using BookStore.Model;

namespace BookStore.Repositories.AddressRepository
{
    public interface IAddressRepository
    {
        List<Address> GetAddresses(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        List<Address> GetAddressByUser(int userId);
        Address GetAddressById(int? id = 0);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);
        void CreateAddress(Address address);
        int GetAddressCount();
        bool IsSaveChanges();
    }
}
