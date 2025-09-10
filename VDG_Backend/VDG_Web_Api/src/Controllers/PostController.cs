using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Doctor)}")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]

        public async Task<ActionResult> AddPost([FromBody] AddPostDTO postDTO)
        {

            if (postDTO == null)
            {
                throw new ArgumentNullException(nameof(postDTO));
            }
            try
            {
                await _postService.AddPostAsync(postDTO);
                return Created();

            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpDelete]

        public async Task<ActionResult> DeletePost(PostDTO postDTO)
        {
            if (postDTO == null || postDTO.Id < 0)
            {
                throw new ArgumentNullException(nameof(postDTO));
            }
            try
            {
                await _postService.DeletePostAsync(postDTO.Id);
                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost([FromBody] UpdatePostDTO postDTO)
        {
            if (postDTO == null)
            {
                throw new ArgumentNullException(nameof(postDTO));
            }
            try
            {
                await _postService.UpdatePostAsync(postDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PostDTO>> GetAllPosts(int? doctorId)
        {
            if (doctorId is not null && (doctorId < 0))
            {
                throw new ArgumentNullException(nameof(doctorId));
            }

            try
            {
                return await _postService.GetAllPostsAsync(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpGet]
        public async Task<PostDTO> GetPost(int postId)
        {
            if (postId < 0)
            {
                throw new ArgumentNullException(nameof(postId));
            }
            try
            {
                return await _postService.GetPostAsync(postId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }
    }
}
