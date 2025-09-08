using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Adding")]

        public async Task<ActionResult> AddPost(PostDTO postDTO)
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

        [HttpDelete("delete")]

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

        [HttpPut("update")]
        public async Task<ActionResult> UpdatePost(PostDTO postDTO)
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

        [HttpGet("GetAllPosts")]
        public async Task<IEnumerable<PostDTO>> GetAllPosts(int doctorId)
        {
            if (doctorId < 0)
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

        [HttpGet("GetPost")]
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
