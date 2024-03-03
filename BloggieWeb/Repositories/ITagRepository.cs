using BloggieWeb.Models.Domain;

namespace BloggieWeb.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag); // enable return type
        Task<Tag?> DeleteAsync(Guid id);
    }
}
