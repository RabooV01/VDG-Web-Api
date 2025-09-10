using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class PostService : IPostService
    {
        public readonly IPostRepository _postrepository;

        public PostService(IPostRepository postrepository)
        {
            _postrepository = postrepository;
        }

        public async Task AddPostAsync(AddPostDTO post)
        {
            try
            {
                Post p = post.AddPostToEntity();
                await _postrepository.AddPostAsync(p);
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid adding post, Error{ex.Message}", ex);
            }
        }

        public async Task DeletePostAsync(int postId)
        {
            try
            {
                await _postrepository.DeletePostAsync(postId);
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid deleting post, Error{ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync(int doctorId)
        {
            try
            {
                var posts = await _postrepository.GetAllPostsAsync(doctorId);
                return posts.Select(p => p.ToDto()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid retieriving posts, Error{ex.Message}", ex);
            }
        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {
            try
            {
                var post = await _postrepository.GetPostAsync(postId);
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid retieriving post, Error{ex.Message}", ex);
            }
        }
        public async Task UpdatePostAsync(PostDTO post)
        {
            try
            {
                await _postrepository.UpdatePostAsync(post.PostToEntity());
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid updating post, Error{ex.Message}", ex);
            }
        }
    }
}
