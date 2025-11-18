using CustomExceptions;
using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class CommentEfcRepository : ICommentRepository{
    private readonly ForumContext ctx;

    public CommentEfcRepository(ForumContext ctx){
        this.ctx = ctx;
    }
    
    public async Task<Comment> GetSingleAsync(int id){
        Comment? comment = await ctx.Comments.FindAsync(id);
        if (comment == null){
            throw new NotFoundException($"Comment with id {id} not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetMany(){
        return ctx.Comments.AsQueryable();
    }
    
    public async Task<Comment> AddAsync(Comment comment){
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment){
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        Comment existingComment = await GetSingleAsync(id);
        ctx.Comments.Remove(existingComment);
        await ctx.SaveChangesAsync();
    }
}