using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Entities;
using ApiContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase{
    private readonly CommentsService commentsService;
    private readonly UsersService usersService;

    public CommentsController(CommentsService commentsService, UsersService usersService){
        this.commentsService = commentsService;
        this.usersService = usersService;
    }
    
    [HttpGet(Name = "GetComments")]
    public ActionResult<IEnumerable<CommentDTO>> GetComments([FromQuery] string? user, [FromQuery] int? post){
        IQueryable<Comment> comments = commentsService.GetMany().ToList().AsQueryable(); // The query must be evaluated otherwise it raises an exception
        if (post != null)
            comments = comments.Where(c => c.PostId == post);
            
        if (user != null){
            int id = 0;
            if (int.TryParse(user, out id)){
                comments = comments.Where(c => c.UserId == id);
            } else {
                comments = comments.Where(c => usersService.GetSingleAsync(c.UserId).Result.
                    Username.ToLower().Contains(user.ToLower())) ;
            }
        }
        IQueryable<CommentDTO> commentDTOs = comments.Select(c => CreateCommentDTOFromComment(c));
        return Ok(commentDTOs);
    }

    [HttpGet("{id:int}", Name = "GetComment")]
    public async Task<IActionResult> GetComment([FromRoute] int id){
        Comment comment = await commentsService.GetSingleAsync(id);
        return Ok(CreateCommentDTOFromComment(comment));
    }

    [HttpPost(Name = "CreateComment")]
    public async Task<IActionResult> CreateComment([FromBody] CommentDTO newComment){
        Comment comment = new Comment(newComment.Body, newComment.PostId, newComment.UserId);
        Comment createdComment = await commentsService.AddAsync(comment);

        return Created(
            Url.Link("GetComment", new{ id = createdComment.Id }),
            CreateCommentDTOFromComment(createdComment)
        );
    }

    [HttpPut("{id:int}", Name = "UpdateComment")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentDTO updatedComment){
        Comment comment = new Comment(updatedComment.Body, updatedComment.PostId, updatedComment.UserId){ Id = id };
        await commentsService.UpdateAsync(comment);

        return Ok(CreateCommentDTOFromComment(comment));
    }

    [HttpDelete("{id:int}", Name = "DeleteComment")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id){
        await commentsService.DeleteAsync(id);
        return NoContent();
    }

    private CommentDTO CreateCommentDTOFromComment(Comment comment){
        return new CommentDTO(){ Id = comment.Id, Body = comment.Body, PostId = comment.PostId, UserId = comment.UserId};
    }
}