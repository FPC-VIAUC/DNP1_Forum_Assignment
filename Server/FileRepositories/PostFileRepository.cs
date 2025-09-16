using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository{
    private readonly string filePath = "posts.json";

    public PostFileRepository(){
        if (!File.Exists(filePath)){
            File.WriteAllText(filePath, "[]");
            AddDummyDataAsync();
        }
    }
    
    public async Task<Post> AddAsync(Post post){
        List<Post> posts = await getPostsAsync();
        
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);

        await savePostsAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post){
        List<Post> posts = await getPostsAsync();
        
        Post existingPost = getPost(posts, post.Id);
        posts.Remove(existingPost);
        posts.Add(post);

        await savePostsAsync(posts);
    }

    public async Task DeleteAsync(int id){
        List<Post> posts = await getPostsAsync();

        Post postToRemove = getPost(posts, id);
        posts.Remove(postToRemove);

        await savePostsAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id){
        List<Post> posts = await getPostsAsync();
        Post post = getPost(posts, id);
        return post;
    }

    public IQueryable<Post> GetMany(){
        List<Post> posts = getPostsAsync().Result;
        return posts.AsQueryable();
    }

    private async Task<List<Post>> getPostsAsync(){
        string postsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsJson)!;
        return posts;
    }
    
    private async Task savePostsAsync(List<Post> posts){
        string postsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsJson);
    }

    private Post getPost(List<Post> posts, int id){
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null){
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        return post;
    } 

    public async Task AddDummyDataAsync(){
        await AddAsync(new Post("Hello!", "Hello World!", 1));
        await AddAsync(new Post("Wow!", "What an interesting application!", 2));
    }
}