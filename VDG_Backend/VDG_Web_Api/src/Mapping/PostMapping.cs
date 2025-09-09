using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
    public static class PostMapping
    {
        public static Post AddPostToEntity(this AddPostDTO postDTO)
            => new()
            {
                ImageUrl = postDTO.ImageUrl,
                DoctorId = postDTO.DoctorId,
                Content = postDTO.Content
            };

        public static Post PostToEntity(this PostDTO postDTO)
           => new()
           {
               Id = postDTO.Id,
               DoctorId = postDTO.DoctorId,
               Content = postDTO.Content
           };

        public static PostDTO ToDto(this Post post)
            => new()
            {
                Id = post.Id,
                DoctorId = post.DoctorId,
                Content = post.Content
            };
    }
}
