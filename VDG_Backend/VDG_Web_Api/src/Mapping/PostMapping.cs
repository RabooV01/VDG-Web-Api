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

        public static Post PostToEntity(this UpdatePostDTO postDTO)
           => new()
           {
               Id = postDTO.PostId,
               Content = postDTO.Content,
               ImageUrl = postDTO.ImageUrl
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
