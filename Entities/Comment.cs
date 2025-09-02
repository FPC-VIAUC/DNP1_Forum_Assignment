namespace Entities;

public class Comment{
    private static int nextId = 0;

    public int Id{ get; set; }
    public string Body{ get; set; }
    public int PostId{ get; set; }
    public int UserId{ get; set; }

    public Comment(string body, int postId, int userId){
        Id = nextId++;
        Body = body;
        PostId = postId;
        UserId = userId;
    }
}