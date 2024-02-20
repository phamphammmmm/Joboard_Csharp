using Joboard.DTO.Tag;
using Joboard.Service.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Controllers
{
    [Route("/api/tags")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetTagById(int? id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return Ok(tag);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTag(TagCreate_DTO tagCreate_DTO)
        {
            var result = await _tagService.CreateTagAsync(tagCreate_DTO);
            if (result)
            {
                return Ok(new { Message = "Tag created successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to create tag" });
            }
        }


        [HttpPost("update/{TagId}")]
        public async Task<IActionResult> UpdateTag(int? TagId, TagEdit_DTO tagEdit_DTO)
        {
            var result = await _tagService.UpdateTagAsync(TagId, tagEdit_DTO);
            if (result)
            {
                return Ok(new { Message = "Tag updated successfully" });
            }
            else
            {
                return NotFound(new { Message = "Tag not found" });
            }
        }

        [HttpDelete("delete/{TagId}")]
        public async Task<IActionResult> DeleteTag(int? TagId)
        {
            var result = await _tagService.DeleteTagAsync(TagId);
            if (result)
            {
                return Ok(new { Message = "Tag deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Tag not found" });
            }
        }
    }
}
