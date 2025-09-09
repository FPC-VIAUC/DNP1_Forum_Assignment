using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository{
    private List<Comment> comments;

    public Task<Comment> AddAsync(Comment comment){
        comment.Id = comments.Any()
            ? comments.Max(u => u.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment){
        Comment existingComment = getComment(comment.Id);
        
        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id){
        Comment commentToRemove = getComment(id);

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id){
        Comment comment = getComment(id);
        
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany(){
        return comments.AsQueryable();
    }

    private Comment getComment(int id){
        Comment? comment = comments.SingleOrDefault(u => u.Id == id);
        if (comment is null){
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        return comment;
    }
}