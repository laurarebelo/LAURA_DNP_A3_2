using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubredditsController : ControllerBase
{
    private readonly ISubredditLogic subredditLogic;

    public SubredditsController(ISubredditLogic subredditLogic)
    {
        this.subredditLogic = subredditLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Subreddit>> CreateAsync(SubredditCreationDto dto)
    {
        try
        {
            Subreddit subreddit = await subredditLogic.CreateAsync(dto);
            return Created($"/subreddits/{subreddit.Title}", subreddit);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<Subreddit>> Get(String? titleContains)
    {
        try
        {
            var subreddit = await subredditLogic.GetAsync(new SingleSearchParameterDto(titleContains));
            return Ok(subreddit);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<string>>> GetAllTitles()
    {
        try
        {
            var subreddits = await subredditLogic.GetAllTitles();
            return Ok(subreddits);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}