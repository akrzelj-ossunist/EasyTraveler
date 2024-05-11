using AutoMapper;
using ET.Core.Entities;
using ET.Application.Models.BusDtos;
using ET.Application.Utilities;
using ET.Application.Services;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Models;
using ET.Application.Models.CompanyDtos.Response;
using ET.DataAccess.Repositories;

namespace ET.Application.Mappers
{
    public class BusMapper
    {
        private readonly IMapper _mapper;
        private readonly AuthenticateUser _authenticateUser;
        private readonly CompanyMapper _companyMapper;
        private readonly CompanyRepository _companyRepository; 
        public required AuthenticatedDto AuthenticateDto { get; set; }

        public BusMapper(AuthenticateUser authenticateUser, CompanyRepository companyRepository, CompanyMapper companyMapper)
        {
            _authenticateUser = authenticateUser;
            _companyRepository = companyRepository;
            _companyMapper = companyMapper;

            AuthenticateDto = _authenticateUser.CreateAuthentication();

            var company = _companyRepository.FindById(AuthenticateDto.Id);
            var companyDto = _companyMapper.CompanyToCompanyDto(company);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BusDto, Bus>()
                   .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true))
                   .ForMember(dest => dest.Company, opt => opt.MapFrom(src => company));
                cfg.CreateMap<Bus, BusResponseDto>()
                   .ForMember(dest => dest.Company, opt => opt.MapFrom(src => companyDto));
            });

            _mapper = config.CreateMapper();
        }

        public Bus BusDtoToBus(BusDto busDto)
        {
            return _mapper.Map<Bus>(busDto);
        }

        public BusResponseDto BusToBusDto(Bus bus)
        {
            return _mapper.Map<BusResponseDto>(bus);
        }
    }
}
