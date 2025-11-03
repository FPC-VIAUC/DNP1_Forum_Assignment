using System.Security.Claims;
using System.Text.Json;
using ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider{
    private HttpClient client;
    private ClaimsPrincipal claimsPrincipal;

    public SimpleAuthProvider(HttpClient client){
        this.client = client;
    }
    
    public override Task<AuthenticationState> GetAuthenticationStateAsync(){
        return Task.FromResult(new AuthenticationState(claimsPrincipal ?? new ClaimsPrincipal()));
    }

    public async Task LoginAsync(string username, string password){
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "auth/login",
            new LoginRequestDTO(){Username = username, Password = password}
        );
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode){
            throw new Exception(content);
        }

        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        })!;

        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString())
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        claimsPrincipal = new ClaimsPrincipal(identity);
        
        NotifyAuthChange();
    }

    public void Logout(){
        claimsPrincipal = new ClaimsPrincipal();
        NotifyAuthChange();
    }

    private void NotifyAuthChange(){
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}