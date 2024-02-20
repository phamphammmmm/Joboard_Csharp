using Joboard.DTO.Tag;

namespace Joboard.Service.Tag
{
    public interface ITagService    
    {
        Task<List<Entities.Tag>> GetAllTagsAsync();
        Task<Entities.Tag> GetTagByIdAsync(int? id);
        Task<bool> CreateTagAsync(TagCreate_DTO tagCreate_DTO);
        Task<bool> UpdateTagAsync(int? id, TagEdit_DTO tagEdit_DTO);
        Task<bool> DeleteTagAsync(int? tagId);
    }
}
