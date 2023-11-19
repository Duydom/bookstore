using BookStore.DTOs.Response;

namespace BookStore.Service.ShippingModeService
{
    public interface IShippingModeService
    {
        ResponseDTO GetShippingModes(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetShippingModeById(int id);
        ResponseDTO UpdateShippingMode(int id, string name);
        ResponseDTO DeleteShippingMode(int id);
        ResponseDTO CreateShippingMode(string name);
    }
}
