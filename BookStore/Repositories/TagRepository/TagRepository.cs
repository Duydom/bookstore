using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _dataContext;
        public TagRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateTag(Tag tag)
        {
            _dataContext.Tags.Add(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _dataContext.Tags.Remove(tag);
        }

        public Tag GetTagById(int id)
        {
            return _dataContext.Tags.FirstOrDefault(t => t.Id == id);
        }

        public int GetTagCount()
        {
            return _dataContext.Tags.Count();
        }

        public List<Tag> GetTags(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.Tags.AsQueryable();

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

        public void UpdateTag(Tag tag)
        {
            _dataContext.Entry(tag).State = EntityState.Modified;
        }
    }
}
