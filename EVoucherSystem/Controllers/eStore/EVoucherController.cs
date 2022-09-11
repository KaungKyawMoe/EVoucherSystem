using AutoMapper;
using EVoucherSystem.Controllers.api;
using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using EVoucherSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EVoucherSystem.Controllers.eStore
{
    public class EVoucherController : Controller
    {
        private readonly ILogger<EVoucherController> _logger;

        private readonly IMapper _mapper;

        private readonly IEvoucherService _evoucherService;

        public EVoucherController(IEvoucherService evoucherService, IMapper mapper, ILogger<EVoucherController> logger)
        {
            _evoucherService = evoucherService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<EVoucher> eVoucherList = await _evoucherService.GetAllEVouchers();
            List<EVoucherDto> eVoucherDtoList = new List<EVoucherDto>();
            eVoucherList.ForEach(x => eVoucherDtoList.Add(_mapper.Map<EVoucherDto>(x)));

            return View("EVoucherList",eVoucherDtoList);
        }
    }
}
