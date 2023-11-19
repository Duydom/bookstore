using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.ShippingModeRepository
{
    public class ShippingModeRepository : IShippingModeRepository
    {
        private readonly DataContext _dataContext;
        public ShippingModeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateShippingMode(ShippingMode shippingMode)
        {
            _dataContext.ShippingModes.Add(shippingMode);
        }

        public void DeleteShippingMode(ShippingMode shippingMode)
        {
            _dataContext.ShippingModes.Remove(shippingMode);
        }

        public ShippingMode GetShippingModeById(int id)
        {
            return _dataContext.ShippingModes.FirstOrDefault(s => s.Id == id);
        }

        public int GetShippingModeCount()
        {
            return _dataContext.ShippingModes.Count();
        }

        public List<ShippingMode> GetShippingModes(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.ShippingModes.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Name.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "NAME":
                    query = query.OrderBy(u => u.Name);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenBy(u => u.Id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public void UpdateShippingMode(ShippingMode shippingMode)
        {
            _dataContext.Entry(shippingMode).State = EntityState.Modified;
        }
    }
}
