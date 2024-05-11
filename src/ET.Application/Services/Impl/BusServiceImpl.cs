using ET.Application.Exceptions;
using ET.Application.Mappers;
using ET.Application.Models;
using ET.Application.Models.BusDtos;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Utilities;
using ET.DataAccess.Repositories;

namespace ET.Application.Services.Impl
{
    public class BusServiceImpl : BusService
    {
        private readonly BusRepository _busRepository;
        private readonly AuthenticateUser _authenticateUser;
        private readonly BusMapper _busMapper;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public BusServiceImpl(BusRepository busRepository, BusMapper busMapper, AuthenticateUser authenticateUser)
        {
            _busRepository = busRepository;
            _busMapper = busMapper;
            _authenticateUser = authenticateUser;
        }
        public BusResponseDto Create(BusDto busDto)
        {
            if (busDto == null) throw new InvalidArgumentsException("Sent bus create data cannot be null!");

            var newBus = _busRepository.Save(_busMapper.BusDtoToBus(busDto));

            return _busMapper.BusToBusDto(newBus);
        }

        public bool Delete(Guid id)
        {
            var bus = _busRepository.FindById(id);
            if (bus == null) throw new NotFoundException("Bus with sent id doesnt exist!");

            _busRepository.Delete(bus);

            return true;
        }

        public BusResponseDto GetById(Guid id)
        {
            var bus = _busRepository.FindById(id);

            if (bus == null) throw new NotFoundException("Bus with sent id doesnt exist!");

            return _busMapper.BusToBusDto(bus);
        }

        public BusResponseDto Update(Guid id, BusDto busDto)
        {
            if (busDto == null) throw new InvalidArgumentsException("Sent bus edit data cannot be null!");

            var bus = _busRepository.FindById(id);
            if (bus == null) throw new NotFoundException("Bus with sent id doesnt exist!");

            bus.Name = busDto.Name;
            //Later check if bus have ride in that case he cannot edit number of seats
            bus.Seats = busDto.Seats;

            var editedBus = _busRepository.Update(bus);

            return _busMapper.BusToBusDto(editedBus);
        }

        public List<BusResponseDto> Filter(Dictionary<string, string> searchParams)
        {
            var validator = new ValidatePageableParams();
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            Console.WriteLine(searchParams["page"] + " SHOULD BE BEFORE:!");
            var companyId = AuthenticatedDto.Id.ToString();
            var name = searchParams.GetValueOrDefault("name", "");
            var isAvailable = searchParams.GetValueOrDefault("isAvailable", "");
            var seats = searchParams.GetValueOrDefault("seats", "");
            var company = searchParams.GetValueOrDefault("company", "");
            var pageParam = validator.Validate(searchParams.GetValueOrDefault("page", "0"), 0);
            var sizeParam = validator.Validate(searchParams.GetValueOrDefault("size", "10"), 10);
            var sortByParam = searchParams.TryGetValue("sortBy", out var value) ? value : "Id";
            Console.WriteLine("PAGE PARAM IS: " + pageParam);
            if (AuthenticatedDto.Role == Core.Enums.UserRole.Admin)
            {
                company = "";
                companyId = "";
            }

            var buses = _busRepository.FilterByParams(companyId, name, seats, isAvailable, company, pageParam, sizeParam, sortByParam);

            return buses.Select(_busMapper.BusToBusDto).ToList();
        }


        public List<BusResponseDto> GetAll(Dictionary<string, string> searchParams)
        {
            var validator = new ValidatePageableParams();
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            var page = validator.Validate(searchParams.GetValueOrDefault("page", "0"), 0);
            var size = validator.Validate(searchParams.GetValueOrDefault("size", "10"), 10);
            var sortBy = searchParams.TryGetValue("sortBy", out var value) ? value : "Id";
            var companyId = "";

            if(AuthenticatedDto.Role == Core.Enums.UserRole.Company) companyId = AuthenticatedDto.Id.ToString();

            var buses = _busRepository.FindAll(companyId, page, size, sortBy);

            return buses.Select(_busMapper.BusToBusDto).ToList();
        }

        public List<BusResponseDto> List(Dictionary<string, string> searchParams)
        {
            if (searchParams == null || searchParams.All(kv => string.IsNullOrEmpty(kv.Value)))
            {
                return GetAll(searchParams);
            }
            return Filter(searchParams);
        }
    }
}
