using ApiContracts;

namespace BlazorApp.Services;

public interface IPostsService{
    IQueryable<PostDTO> GetManyPosts();
    Task<PostDTO> GetSinglePostAsync(int id);
    Task<PostDTO> AddPostAsync(PostDTO request);
    Task UpdatePostAsync(int id, PostDTO request);
    Task DeletePostAsync(int id);
}