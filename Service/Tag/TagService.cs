using Joboard.DTO.Tag;
using Joboard.Repository.Tag;
using Joboard.Service.Tag;

namespace Joboard.Service.Tag
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<bool> EditTagAsync(int? Tagid, TagEdit_DTO tagEdit_DTO)
        {
            var tagDB = await _tagRepository.GetTagByIdAsync(Tagid);
            if (tagDB == null)
            {
                return false;
            }

            tagDB.Name = tagEdit_DTO.Name;
            tagDB.Update_at = tagEdit_DTO.Update_at;

            await _tagRepository.UpdateTagAsync(tagDB);

            return true;
        }

        public async Task<bool> CreateTagAsync(TagCreate_DTO tagCreate_DTO)
        {
            var tag = new Entities.Tag
            {
                Name = tagCreate_DTO.Name,
                Create_at = tagCreate_DTO.Create_at
            };

            return await _tagRepository.CreateTagAsync(tag);
        }

        public async Task<List<Entities.Tag>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllTagsAsync();
        }

        public async Task<Entities.Tag> GetTagByIdAsync(int? id)
        {
            return await _tagRepository.GetTagByIdAsync(id);
        }

        public async Task<bool> UpdateTagAsync(int? id, TagEdit_DTO tagEdit_DTO)
        {
            var tagDB = await _tagRepository.GetTagByIdAsync(id);
            if (tagDB == null)
            {
                return false;
            }

            tagDB.Name = tagEdit_DTO.Name;
            tagDB.Update_at = tagEdit_DTO.Update_at;

            return await _tagRepository.UpdateTagAsync(tagDB);
        }

        public async Task<bool> DeleteTagAsync(int? tagId)
        {
            return await _tagRepository.DeleteTagAsync(tagId);
        }
    }
}
