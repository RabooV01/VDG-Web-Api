using VDG_Web_Api.src.DTOs.PostDTOs;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface IPostService
    {

        public Task AddPostAsync(AddPostDTO post);

        public Task DeletePostAsync(int postId);

        public Task UpdatePostAsync(PostDTO post);

        public Task<IEnumerable<PostDTO>> GetAllPostsAsync(int doctorId);

        public Task<PostDTO> GetPostAsync(int postId);
    }
}
