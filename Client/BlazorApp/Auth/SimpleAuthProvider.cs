using System.Security.Claims;
using System.Text.Json;
using ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider{
    private HttpClient client;
    private IJSRuntime jsRuntime;

    public SimpleAuthProvider(HttpClient client, IJSRuntime jsRuntime){
        this.client = client;
        this.jsRuntime = jsRuntime;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync(){
        string userAsJson = "";
        try{
            userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        } catch (InvalidOperationException e){ }
        if (string.IsNullOrEmpty(userAsJson)){
            return new AuthenticationState(new ClaimsPrincipal());
        }

        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(userAsJson);
        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString())
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

        return new AuthenticationState(claimsPrincipal);
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
        
        string serialisedData = JsonSerializer.Serialize(userDto);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);

        NotifyAuthChange();
    }

    public async Task Logout(){
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthChange();
    }

    private void NotifyAuthChange(){
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}