using BookStore.DTOs.Address;
using BookStore.DTOs.Response;
using BookStore.Model;

namespace BookStore.Service.AddressService
{
    public interface IAddressService
    {
        ResponseDTO GetAddresses(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetAddressByUser(int userId);
        ResponseDTO GetAddressById(int id);
        ResponseDTO UpdateAddress(int id, UpdateAddressDTO updateAddressDTO);
        ResponseDTO DeleteAddress(int id);
        ResponseDTO CreateAddress(CreateAddressDTO createAddressDTO);
    }
}
