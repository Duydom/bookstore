using BookStore.DTOs.Response;

namespace BookStore.Service.PublisherService
{
    public interface IPublisherService
    {
        ResponseDTO GetPublishers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetPublisherById(int id);
        ResponseDTO UpdatePublisher(int id, string name);
        ResponseDTO DeletePublisher(int id);
        ResponseDTO CreatePublisher(string name);
    }
}
