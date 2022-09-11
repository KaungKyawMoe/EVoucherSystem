using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using EVoucherSystem.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EVoucherSystem.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    //[Route("[controller]")]
    [Route("api/Evouchers")]
    public class EvoucherApiController : ControllerBase
    {

        private readonly ILogger<EvoucherApiController> _logger;

        private readonly IMapper _mapper;

        private readonly IEvoucherService _evoucherService;

        public EvoucherApiController(IEvoucherService evoucherService, IMapper mapper, ILogger<EvoucherApiController> logger)
        {
            _evoucherService = evoucherService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("GetEvouchers")]
        [HttpGet]
        public async Task<IEnumerable<EVoucherDto>> Get([FromQuery] bool? showActive = null)
        {
            List<EVoucher> eVoucherList = await _evoucherService.GetAllEVouchers(showActive: showActive);
            List<EVoucherDto> eVoucherDtoList = new List<EVoucherDto>();
            eVoucherList.ForEach(x => eVoucherDtoList.Add(_mapper.Map<EVoucherDto>(x)));

            return eVoucherDtoList;
        }

        [Route("GetEvoucherDetail")]
        [HttpGet]
        public async Task<EVoucherDto> GetEvoucherDetail([FromQuery] int id)
        {
            var evoucher = await _evoucherService.GetEVoucherById(id);
            return _mapper.Map<EVoucherDto>(evoucher);
        }

        [Route("CreateEvoucher")]
        [HttpPost]
        public async Task<ActionResult> CreateEVoucher([FromBody] EVoucherDto eVoucherDto)
        {
            EVoucher eVoucher = _mapper.Map<EVoucher>(eVoucherDto);
            return Ok(await _evoucherService.SaveEvoucher(eVoucher));
        }

        [Route("UpdateEvoucher")]
        [HttpPost]
        public async Task<ActionResult> UpdateEVoucher([FromBody] EVoucherDto eVoucherDto)
        {
            var result = await _evoucherService.GetEVoucherById(eVoucherDto.Id);
            if (result == null)
            {
                return NotFound("EVoucher Not Found");
            }
            else
            {
                EVoucher eVoucher = _mapper.Map<EVoucher>(eVoucherDto);
                return Ok(await _evoucherService.UpdateEvoucher(eVoucher.Id, eVoucher));
            }

        }
    }
}