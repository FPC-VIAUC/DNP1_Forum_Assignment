using CustomExceptions;
using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class UserEfcRepository : IUserRepository{
    private readonly ForumContext ctx;

    public UserEfcRepository(ForumContext ctx){
        this.ctx = ctx;
    }
    
    public async Task<User> GetSingleAsync(int id){
        User? user = await ctx.Users.FindAsync(id);
        if (user == null){
            throw new NotFoundException($"User with id {id} not found");
        }
        return user;
    }

    public IQueryable<User> GetMany(){
        return ctx.Users.AsQueryable();
    }
    
    public async Task<User> AddAsync(User user){
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user){
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        User existingUser = await GetSingleAsync(id);
        ctx.Users.Remove(existingUser);
        await ctx.SaveChangesAsync();
    }
}