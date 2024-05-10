using AutoMapper;
using ET.Core.Entities;
using ET.Application.Models.CompanyDtos;
using ET.Application.Models.CompanyDtos.Response;

namespace ET.Application.Mappers
{
    public class CompanyMapper
    {
        private readonly IMapper _mapper;

        public CompanyMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyRegisterDto, Company>();
                cfg.CreateMap<Company, CompanyResponseDto>();
            });

            _mapper = config.CreateMapper();
        }

        public Company CompanyDtoToCompany(CompanyRegisterDto companyRegisterDto)
        {
            return _mapper.Map<Company>(companyRegisterDto);
        }

        public CompanyResponseDto CompanyToCompanyDto(Company company)
        {
            return _mapper.Map<CompanyResponseDto>(company);
        }
    }
}
