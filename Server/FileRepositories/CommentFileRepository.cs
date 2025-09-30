using System.Text.Json;
using CustomExceptions;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository{
    private readonly string filePath = "comments.json";

    public CommentFileRepository(){
        if (!File.Exists(filePath)){
            File.WriteAllText(filePath, "[]");
            AddDummyDataAsync();
        }
    }
    
    public async Task<Comment> AddAsync(Comment comment){
        List<Comment> comments = await getCommentsAsync();
        
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);

        await saveCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment){
        List<Comment> comments = await getCommentsAsync();
        
        Comment existingComment = getComment(comments, comment.Id);
        comments.Remove(existingComment);
        comments.Add(comment);

        await saveCommentsAsync(comments);
    }

    public async Task DeleteAsync(int id){
        List<Comment> comments = await getCommentsAsync();

        Comment commentToRemove = getComment(comments, id);
        comments.Remove(commentToRemove);

        await saveCommentsAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id){
        List<Comment> comments = await getCommentsAsync();
        Comment comment = getComment(comments, id);
        return comment;
    }

    public IQueryable<Comment> GetMany(){
        List<Comment> comments = getCommentsAsync().Result;
        return comments.AsQueryable();
    }

    private async Task<List<Comment>> getCommentsAsync(){
        string commentsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsJson)!;
        return comments;
    }
    
    private async Task saveCommentsAsync(List<Comment> comments){
        string commentsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsJson);
    }

    private Comment getComment(List<Comment> comments, int id){
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment is null){
            throw new NotFoundException(
                $"Comment with ID '{id}' not found");
        }
        return comment;
    }

    public async Task AddDummyDataAsync(){
        await AddAsync(new Comment("Hello to you too!", 1, 3));
        await AddAsync(new Comment("Thank you!", 2, 3));
    }
}