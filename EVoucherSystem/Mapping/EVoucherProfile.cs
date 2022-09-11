using AutoMapper;
using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using System.Text;

namespace EVoucherSystem.Mapping
{
    public class EVoucherProfile : Profile
    {
        public EVoucherProfile()
        {
            CreateMap<EVoucherDto, EVoucher>();

            CreateMap<EVoucher, EVoucherDto>();

        }
    }
}
