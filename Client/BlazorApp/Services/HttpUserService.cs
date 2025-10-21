using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService : IUserService{
    private HttpClient client;

    public HttpUserService(HttpClient client){
        this.client = client;
    }
    
    public IQueryable<UserDTO> GetManyUser(){
        HttpResponseMessage httpResponse = client.GetAsync("users").Result;
        string response = httpResponse.Content.ReadAsStringAsync().Result;
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<IQueryable<UserDTO>>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task<UserDTO> GetSingleUserAsync(int id){
        HttpResponseMessage httpResponse = await client.GetAsync($"users/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDTO>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task<UserDTO> AddUserAsync(CreateUserDTO request){
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDTO>(
            response, 
            new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task UpdateUserAsync(int id, CreateUserDTO request){
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }
    }

    public async Task DeleteUserAsync(int id){
        HttpResponseMessage httpResponse = await client.DeleteAsync($"users/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }
    }
}