namespace Entities;

public class User{
    private static int nextId = 0;

    public int Id{ get; set; }
    public string Username{ get; set; }
    public string Password{ get; set; }

    public User(string username, string password){
        Id = nextId++;
        Username = username;
        Password = password;
    }
}