namespace Entities;

public class Post{
    private static int nextId = 0;

    public int Id{ get; set; }
    public string Title{ get; set; }
    public string Body{ get; set; }
    public int UserId{ get; set; }

    public Post(string title, string body, int userId){
        Id = nextId++;
        Title = title;
        Body = body;
        UserId = userId;
    }
}