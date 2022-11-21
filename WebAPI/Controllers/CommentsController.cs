using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;
    
    public CommentsController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync(CommentCreationDto dto)
    {
        try
        {
            Comment comment = await commentLogic.CreateAsync(dto);
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    [Route("upvote")] 
    public async Task<ActionResult<Comment>> UpvoteComment(CommentVoteDto dto)
    {
        try
        {
            Comment comment = await commentLogic.UpvoteComment(dto);
            return Accepted($"/subreddits/{comment.Subreddit}/{comment.Post.Id}/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    [Route("downvote")] 
    public async Task<ActionResult<Comment>> DownvoteComment(CommentVoteDto dto)
    {
        try
        {
            Comment comment = await commentLogic.DownvoteComment(dto);
            return Accepted($"/subreddits/{comment.Subreddit}/{comment.Post.Id}/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}