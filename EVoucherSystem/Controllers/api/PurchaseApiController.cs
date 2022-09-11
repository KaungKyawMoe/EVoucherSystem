using AutoMapper;
using Microsoft.AspNetCore.Http;
using EVoucherSystem.Models;
using Microsoft.AspNetCore.Mvc;
using EVoucherSystem.Dtos;
using EVoucherSystem.Services;
using EVoucherSystem.Helpers;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EVoucherSystem.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Purchase")]
    [ApiController]
    public class PurchaseApiController : ControllerBase
    {
        private readonly ILogger<PurchaseApiController> _logger;

        private readonly IMapper _mapper;

        private readonly IPurchaseService _PurchaseService;

        private readonly IEvoucherService _EvoucherService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PurchaseApiController(IPurchaseService PurchaseService, 
            IEvoucherService EvoucherService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper, ILogger<PurchaseApiController> logger)
        {
            _PurchaseService = PurchaseService;
            _EvoucherService = EvoucherService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetPurchases")]
        [HttpGet]
        public async Task<IEnumerable<PurchaseDto>> Get()
        {

            List<Purchase> PurchaseList = await _PurchaseService.GetAllPurchases();
            List<PurchaseDto> PurchaseDtoList = new List<PurchaseDto>();
            PurchaseList.ForEach(x => PurchaseDtoList.Add(_mapper.Map<PurchaseDto>(x)));

            return PurchaseDtoList;
        }

        [Route("CreatePurchase")]
        [HttpPost]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseDto PurchaseDto)
        {
            Purchase Purchase = _mapper.Map<Purchase>(PurchaseDto);

            var evoucherDetail = await _EvoucherService.GetEVoucherById(PurchaseDto.EVoucherId);

            if(evoucherDetail != null)
            {
                //check max qty limit for onlyme
                if(PurchaseDto.BuyType == PurchaseType.onlyme)
                {
                    if(PurchaseDto.Qty > evoucherDetail.MaxBuyLimit)
                    {
                        return BadRequest($"Purchasing evoucher for only me allow {evoucherDetail.MaxBuyLimit}");
                    }
                }
                else //check max qty limit for gift
                {
                    if (PurchaseDto.Qty > evoucherDetail.GiftPerUserLimit)
                    {
                        return BadRequest($"Purchasing evoucher for only me allow {evoucherDetail.GiftPerUserLimit}");
                    }
                }

                if(PurchaseDto.PaymentMethod == PaymentMethod.Card)
                {
                    PurchaseDto.Amount = evoucherDetail.Amount * PurchaseDto.Qty;

                    PurchaseDto.Amount = PurchaseDto.Amount + (PurchaseDto.Amount * (decimal) 0.1);
                }
                
            }

            //make payment
            //await MakePayment(PurchaseDto);

            //Generate Promo Code
            Purchase.PromoCode = GeneratePromoCode();

            return Ok(await _PurchaseService.SavePurchase(Purchase));
        }

        [Route("VerifyPromoCode")]
        [HttpGet]
        public async Task<IActionResult> VerifyPromocode([FromQuery] string promoCode)
        {
            var purchaseInfo = await _PurchaseService.GetPurchaseByPromoCode(promoCode);

            EVoucherDto eVoucherDto = new EVoucherDto();

            if(purchaseInfo == null)
            {
                return BadRequest("Promocode is invalid");
            }
            else
            {
                var evoucherInfo = await _EvoucherService.GetEVoucherById(purchaseInfo.EVoucherId);
                if(evoucherInfo.ExpiredDate < DateTime.Now)
                {
                    return BadRequest("Promocode is expired");
                }
                else
                {
                    eVoucherDto = _mapper.Map<EVoucherDto>(evoucherInfo);
                }
            }

            return Ok(eVoucherDto);
        }

        [Route("UpdatePurchase")]
        [HttpPost]
        public async Task<ActionResult> UpdatePurchase([FromBody] PurchaseDto PurchaseDto)
        {
            var result = await _PurchaseService.GetPurchaseById(PurchaseDto.Id);
            if (result == null)
            {
                return NotFound("Purchase Not Found");
            }
            else
            {
                Purchase Purchase = _mapper.Map<Purchase>(PurchaseDto);
                return Ok(await _PurchaseService.UpdatePurchase(Purchase.Id, Purchase));
            }

        }

        private string GeneratePromoCode()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcedfghijklmnopqrstuvwxyz";
            string nums = "0123456789";
            var charstr = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            var numstr = new string(Enumerable.Repeat(nums, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            var result = numstr + charstr;

            return result;
        }


        private async Task MakePayment(PurchaseDto purchaseDto)
        {
            Encoding ascii = Encoding.ASCII;

            //Merchant's account information
            var merchant_id = "JT02";           //Get MerchantID when opening account with 2C2P
            var secret_key = "YDRbw14OtHw3";    //Get SecretKey from 2C2P PGW Dashboard

            //Transaction information
            var payment_description = purchaseDto.Id.ToString();

            var order_id = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var currency = "104"; //Currency Code
            //Request information
            var version = "8.5";

            var invoice_no = purchaseDto.Id.ToString();

            var amount = purchaseDto.Amount.ToString();

            bool useSandBox = true; //use sandbox as default

            var payment_url = useSandBox ?
                "https://demo2.2c2p.com/2C2PFrontEnd/RedirectV3/payment" :
                "https://t.2c2p.com/RedirectV3/payment";

            var result_url_1 = $"http://localhost:5181/swagger";

            //Construct signature string
            var _params = version + merchant_id + payment_description + order_id.ToString() + invoice_no + currency + amount + result_url_1;

            HMACSHA256 hmac = new HMACSHA256(ascii.GetBytes(secret_key));

            var hash_value = BitConverter.ToString(hmac.ComputeHash(ascii.GetBytes(_params))).Replace("-", "");

            RemotePost _remotePost = new RemotePost(_httpContextAccessor)
            {
                Url = payment_url
            };

            _remotePost.Add("version", version);
            _remotePost.Add("merchant_id", merchant_id);
            _remotePost.Add("currency", currency);
            _remotePost.Add("result_url_1", result_url_1);
            _remotePost.Add("hash_value", hash_value);
            _remotePost.Add("payment_description", payment_description);
            _remotePost.Add("order_id", order_id.ToString());
            _remotePost.Add("amount", amount);
            _remotePost.Add("invoice_no", invoice_no);
            _remotePost.Post();
        }
    }
}
