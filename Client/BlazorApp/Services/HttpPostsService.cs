using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpPostsService : IPostsService{
    private HttpClient client;

    public HttpPostsService(HttpClient client){
        this.client = client;
    }
    
    public IQueryable<PostDTO> GetManyPosts(){
        HttpResponseMessage httpResponse = client.GetAsync("posts").Result;
        string response = httpResponse.Content.ReadAsStringAsync().Result;
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<List<PostDTO>>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!.AsQueryable();
    }

    public async Task<PostDTO> GetSinglePostAsync(int id){
        HttpResponseMessage httpResponse = await client.GetAsync($"posts/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<PostDTO>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task<PostDTO> AddPostAsync(PostDTO request){
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<PostDTO>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public Task UpdatePostAsync(int id, PostDTO request){
        throw new NotImplementedException();
    }

    public Task DeletePostAsync(int id){
        throw new NotImplementedException();
    }
}