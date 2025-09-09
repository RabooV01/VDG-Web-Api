using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.DTOs.PostDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly VdgDbDemoContext _context;
        public PostRepository(VdgDbDemoContext context)
        {
            _context = context;
        }

        public async Task<int> AddPostAsync(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return post.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to add Post ,error: [{ex.Message}]", ex);
            }
        }

        public async Task DeletePostAsync(int postId)
        {
            Post? post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new ArgumentNullException("actually, this postId is not found");
            }
            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to remove this post, Error:[{ex.Message}]", ex);
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int doctorId)
        {
            var posts = await _context.Posts.Where(p => p.DoctorId == doctorId).ToListAsync();

            if (posts == null)
            {
                throw new ArgumentException($"this doctor has not any Post ");
            }
            return posts;

        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {

            var post = await _context.Posts.Select(pm => new PostDTO()
            {
                DoctorId = pm.DoctorId,
                Content = pm.Content,
                DoctorName = pm.Doctor.User.Person.FirstName + " " + pm.Doctor.User.Person.LastName ?? "",
                DoctorSpeciality = pm.Doctor.Speciality.Name,
                Id = pm.Id,
                ImageUrl = pm.ImageUrl
            }).FirstOrDefaultAsync();
            if (post == null)
                throw new KeyNotFoundException("the post has not found ");

            return post;

        }

        public async Task UpdatePostAsync(Post post)
        {
            var postToUpdate = await _context.Posts.FindAsync(post.Id);

            if (postToUpdate == null)
                throw new KeyNotFoundException("the post has not found for update");

            try
            {
                await _context.Posts.Where(p => p.Id == post.Id)
                    .ExecuteUpdateAsync(po => po.SetProperty(p => p.ImageUrl, post.ImageUrl).SetProperty(p => p.Content, post.Content));
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Unable to update the post ,error: {ex.Message}", ex);
            }

        }
    }
}
