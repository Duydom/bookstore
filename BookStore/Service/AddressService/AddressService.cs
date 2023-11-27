using AutoMapper;
using BookStore.DTOs.Address;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.AddressRepository;
using BookStore.Repositories.AuthorRepository;
using BookStore.Repositories.UserRepository;

namespace BookStore.Service.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public ResponseDTO CreateAddress(CreateAddressDTO createAddressDTO)
        {
            var user = _userRepository.GetUserById(createAddressDTO.UserId);
            if (user == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "User không tồn tại"
            };
            var address = _mapper.Map<Address>(createAddressDTO);
            _addressRepository.CreateAddress(address);
            if (_addressRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Tạo thành công",
                    Data = _mapper.Map<AddressDTO>(_addressRepository.GetAddressById())
                };
            else
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Tạo thất bại"
                };
        }

        public ResponseDTO DeleteAddress(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Địa chỉ không tồn tại"
                };
            }

            address.IsDeleted = true;
            _addressRepository.UpdateAddress(address);
            if (_addressRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Xóa thành công"
                };
            else
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Xóa thất bại"
                };
        }

        public ResponseDTO GetAddressById(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Địa chỉ không tồn tại"
                };
            }
            return new ResponseDTO()
            {
                Data = _mapper.Map<AddressDTO>(address)
            };
        }

        public ResponseDTO GetAddressByUser(int userId)
        {
            var addresses = _addressRepository.GetAddressByUser(userId);
            return new ResponseDTO()
            {
                Data = _mapper.Map<List<AddressDTO>>(addresses)
            };
        }

        public ResponseDTO GetAddresses(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var addresses = _addressRepository.GetAddresses(page, pageSize, key, sortBy);
            return new ResponseDTO()
            {
                Data = _mapper.Map<List<AddressDTO>>(addresses),
                Total = _addressRepository.GetAddressCount()
            };
        }

        public ResponseDTO UpdateAddress(int id, UpdateAddressDTO updateAddressDTO)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Địa chỉ không tồn tại"
                };
            }

            address.Update = DateTime.Now;
            address.Name = updateAddressDTO.Name;
            address.Street = updateAddressDTO.Street;
            address.City = updateAddressDTO.City;
            address.State = updateAddressDTO.State;
            address.Phone = updateAddressDTO.Phone;

            _addressRepository.UpdateAddress(address);
            if (_addressRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Cập nhật thành công"
                };
            else
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Cập nhật thất bại"
                };
        }
    }
}
