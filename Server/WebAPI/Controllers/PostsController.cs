using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Entities;
using ApiContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase{
    private readonly PostsService postsService;
    private readonly UsersService usersService;

    public PostsController(PostsService postsService, UsersService usersService){
        this.postsService = postsService;
        this.usersService = usersService;
    }

    [HttpGet(Name = "GetPosts")]
    public ActionResult<IEnumerable<PostDTO>> GetPosts([FromQuery] string? title, [FromQuery] string? user){
        IQueryable<Post> posts = postsService.GetMany();
        if (title != null)
            posts = posts.Where(p => p.Title.ToLower().Contains(title.ToLower()));
        
        if (user != null){
            int id = 0;
            if (int.TryParse(user, out id)){
                posts = posts.Where(p => p.UserId == id);
            } else {
                posts = posts.Where(p => usersService.GetSingleAsync(p.UserId).Result.
                        Username.ToLower().Contains(user.ToLower())) ;
            }
        }
        IQueryable<PostDTO> postDTOs = posts.Select(p => CreatePostDTOFromPost(p));
        return Ok(postDTOs);
    }

    [HttpGet("{id:int}", Name = "GetPost")]
    public async Task<IActionResult> GetPost([FromRoute] int id){
        Post post = await postsService.GetSingleAsync(id);
        return Ok(CreatePostDTOFromPost(post));
    }

    [HttpPost(Name = "CreatePost")]
    public async Task<IActionResult> CreatePost([FromBody] PostDTO newPost){
        Post post = new Post(newPost.Title, newPost.Body, newPost.UserId);
        Post createdPost = await postsService.AddAsync(post);

        return Created(
            Url.Link("GetPost", new{ id = createdPost.Id }),
            CreatePostDTOFromPost(createdPost)
        );
    }

    [HttpPut("{id:int}", Name = "UpdatePost")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] PostDTO updatedPost){
        Post post = new Post(updatedPost.Title, updatedPost.Body, updatedPost.UserId){ Id = id };
        await postsService.UpdateAsync(post);

        return Ok(CreatePostDTOFromPost(post));
    }

    [HttpDelete("{id:int}", Name = "DeletePost")]
    public async Task<IActionResult> DeletePost([FromRoute] int id){
        await postsService.DeleteAsync(id);
        return NoContent();
    }

    private PostDTO CreatePostDTOFromPost(Post post){
        return new PostDTO(){ Id = post.Id, Title = post.Title, Body = post.Body, UserId = post.UserId};
    }
}