using AutoMapper;
using BookStore.DTOs.Response;
using BookStore.DTOs.ShippingMode;
using BookStore.Model;
using BookStore.Repositories.ShippingModeRepository;

namespace BookStore.Service.ShippingModeService
{
    public class ShippingModeService : IShippingModeService
    {
        private readonly IShippingModeRepository _shippingModeRepository;
        private readonly IMapper _mapper;
        public ShippingModeService(IShippingModeRepository shippingModeRepository, IMapper mapper)
        {
            _shippingModeRepository = shippingModeRepository;
            _mapper = mapper;
        }

        public ResponseDTO CreateShippingMode(string name)
        {
            var shippingMode = new ShippingMode { Name = name };
            _shippingModeRepository.CreateShippingMode(shippingMode);
            if (_shippingModeRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = 400, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteShippingMode(int id)
        {
            var shippingMode = _shippingModeRepository.GetShippingModeById(id);
            if (shippingMode == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "ShippingMode không tồn tại"
                };
            shippingMode.IsDeleted = true;
            _shippingModeRepository.UpdateShippingMode(shippingMode);
            if (_shippingModeRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Cập nhật thành công" };
            else return new ResponseDTO() { Code = 400, Message = "Cập nhật thất bại" };
        }

        public ResponseDTO GetShippingModeById(int id)
        {
            var shippingMode = _shippingModeRepository.GetShippingModeById(id);
            if (shippingMode == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "ShippingMode không tồn tại"
                };
            return new ResponseDTO()
            {
                Data = _mapper.Map<ShippingModeDTO>(shippingMode)
            };
        }

        public ResponseDTO GetShippingModes(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var shippingModes = _shippingModeRepository.GetShippingModes(page, pageSize, key, sortBy);
            return new ResponseDTO()
            {
                Data = _mapper.Map<List<ShippingModeDTO>>(shippingModes),
                Total = _shippingModeRepository.GetShippingModeCount()
            };
        }

        public ResponseDTO UpdateShippingMode(int id, string name)
        {
            var shippingMode = _shippingModeRepository.GetShippingModeById(id);
            if (shippingMode == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "ShippingMode không tồn tại"
                };
            shippingMode.Update = DateTime.Now;
            shippingMode.Name = name;
            _shippingModeRepository.UpdateShippingMode(shippingMode);
            if (_shippingModeRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Cập nhật thành công" };
            else return new ResponseDTO() { Code = 400, Message = "Cập nhật thất bại" };
        }
    }
}
