using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpCommentsService : ICommentsService{
    private HttpClient client;

    public HttpCommentsService(HttpClient client){
        this.client = client;
    }
    
    public IQueryable<CommentDTO> GetManyComments(){
        return GetManyComments("");
    }

    public IQueryable<CommentDTO> GetCommentsFromPost(int postId){
        return GetManyComments($"post={postId}");
    }

    private IQueryable<CommentDTO> GetManyComments(string queries){
        HttpResponseMessage httpResponse = client.GetAsync($"comments?{queries}").Result;
        string response = httpResponse.Content.ReadAsStringAsync().Result;
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<List<CommentDTO>>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!.AsQueryable();
    }

    public Task<CommentDTO> GetSingleCommentAsync(int id){
        throw new NotImplementedException();
    }

    public async Task<CommentDTO> AddCommentAsync(CommentDTO request){
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CommentDTO>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public Task UpdateCommentAsync(int id, CommentDTO request){
        throw new NotImplementedException();
    }

    public Task DeleteCommentAsync(int id){
        throw new NotImplementedException();
    }
}