using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public Task<int> AddPostAsync(Post post);

        public Task DeletePostAsync(int postId);

        public Task UpdatePostAsync(Post post);

        public Task<IEnumerable<Post>> GetAllPostsAsync(int doctorId);

        public Task<PostDTO> GetPostAsync(int postId);

    }
}