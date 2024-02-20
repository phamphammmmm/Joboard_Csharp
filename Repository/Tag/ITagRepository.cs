namespace Joboard.Repository.Tag
{
    public interface ITagRepository    
    {
        Task<List<Entities.Tag>> GetAllTagsAsync();
        Task<Entities.Tag> GetTagByIdAsync(int? id);
        Task<bool> CreateTagAsync(Entities.Tag tag);
        Task<bool> UpdateTagAsync(Entities.Tag tag);
        Task<bool> DeleteTagAsync(int? tagId);
    }
}
