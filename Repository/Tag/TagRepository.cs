using Joboard.Context;
using Joboard.Repository.Tag;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<bool> CreateTagAsync(Entities.Tag tag)
        {
            try
            {
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTagAsync(Entities.Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTagAsync(int? tagId)
        {
            if (tagId == null)
            {
                throw new ArgumentNullException(nameof(tagId), "TagId is required");
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(r => r.Id == tagId);
            if (tag == null)
            {
                return false;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Entities.Tag> GetTagByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Tag ID is required");
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(r => r.Id == id);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {id} not found");
            }

            return tag;
        }
    }
}
