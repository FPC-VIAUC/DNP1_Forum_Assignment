using ApiContracts;

namespace BlazorApp.Services;

public interface ICommentsService{
    IQueryable<CommentDTO> GetManyComments();
    IQueryable<CommentDTO> GetCommentsFromPost(int postId);
    Task<CommentDTO> GetSingleCommentAsync(int id);
    Task<CommentDTO> AddCommentAsync(CommentDTO request);
    Task UpdateCommentAsync(int id, CommentDTO request);
    Task DeleteCommentAsync(int id);
}