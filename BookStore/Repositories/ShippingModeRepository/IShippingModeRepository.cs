using BookStore.Model;

namespace BookStore.Repositories.ShippingModeRepository
{
    public interface IShippingModeRepository
    {
        List<ShippingMode> GetShippingModes(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ShippingMode GetShippingModeById(int id);
        void UpdateShippingMode(ShippingMode shippingMode);
        void DeleteShippingMode(ShippingMode shippingMode);
        void CreateShippingMode(ShippingMode shippingMode);
        int GetShippingModeCount();
        bool IsSaveChanges();
    }
}
