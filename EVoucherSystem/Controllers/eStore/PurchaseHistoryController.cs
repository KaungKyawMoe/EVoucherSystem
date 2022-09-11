using AutoMapper;
using EVoucherSystem.Controllers.api;
using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using EVoucherSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EVoucherSystem.Controllers.eStore
{
    public class PurchaseHistoryController : Controller
    {
        private readonly ILogger<PurchaseHistoryController> _logger;

        private readonly IMapper _mapper;

        private readonly IPurchaseService _purchaseService;

        public PurchaseHistoryController(IPurchaseService purchaseService, IMapper mapper, ILogger<PurchaseHistoryController> logger)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Purchase> purchases = await _purchaseService.GetAllPurchases();
            List<PurchaseDto> purchaseDtos = new List<PurchaseDto>();
            purchases.ForEach(x => purchaseDtos.Add(_mapper.Map<PurchaseDto>(x)));

            return View(purchaseDtos);
        }

        public ActionResult Create(PurchaseEVoucherViewModel purchaseEVoucher)
        {
            return RedirectToAction("Index");
        }
    }
}
