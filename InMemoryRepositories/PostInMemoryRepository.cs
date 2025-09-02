using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository{
    private List<Post> posts;

    public Task<Post> AddAsync(Post post){
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post){
        Post existingPost = getPost(post.Id);
        
        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id){
        Post postToRemove = getPost(id);

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id){
        Post post = getPost(id);
        
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany(){
        return posts.AsQueryable();
    }

    private Post getPost(int id){
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null){
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return post;
    }
}