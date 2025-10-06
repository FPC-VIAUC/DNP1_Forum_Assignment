using Microsoft.AspNetCore.Mvc;
using BusinessLogic;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase{
    private readonly PostsService postsService;

    public PostsController(PostsService postsService){
        this.postsService = postsService;
    }
}