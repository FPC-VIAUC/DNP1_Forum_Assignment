using CustomExceptions;
using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class PostEfcRepository : IPostRepository{
    private readonly ForumContext ctx;

    public PostEfcRepository(ForumContext ctx){
        this.ctx = ctx;
    }
    
    public async Task<Post> GetSingleAsync(int id){
        Post? post = await ctx.Posts.FindAsync(id);
        if (post == null){
            throw new NotFoundException($"Post with id {id} not found");
        }
        return post;
    }

    public IQueryable<Post> GetMany(){
        return ctx.Posts.AsQueryable();
    }
    
    public async Task<Post> AddAsync(Post post){
        EntityEntry<Post> entityEntry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post){
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        Post existingPost = await GetSingleAsync(id);
        ctx.Posts.Remove(existingPost);
        await ctx.SaveChangesAsync();
    }
}