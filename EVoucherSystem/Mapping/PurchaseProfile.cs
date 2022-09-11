using AutoMapper;
using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using System.Text;

namespace EVoucherSystem.Mapping
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<PurchaseDto, Purchase>();

            CreateMap<Purchase, PurchaseDto>();

        }
    }
}
