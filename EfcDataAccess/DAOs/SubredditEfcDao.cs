using Application.DAOInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class SubredditEfcDao : ISubredditDao

{
    private RedditContext context;

    public SubredditEfcDao(RedditContext context)
    {
        this.context = context;
    }
    public async Task<Subreddit> Create(Subreddit subreddit)
    {
        EntityEntry<Subreddit> newSubreddit = await context.Subreddits.AddAsync(subreddit);
        await context.SaveChangesAsync();
        return newSubreddit.Entity;
    }

    public async Task<Subreddit?> GetByTitle(string title)
    {
        Subreddit? existing = await context.Subreddits.FirstOrDefaultAsync(
            s => s.Title.ToLower().Equals(title.ToLower()));
        return existing;
    }

    public Task<List<string>> GetAllTitles()
    {
        List<string> titles = new List<string>();
        foreach (var s in context.Subreddits)
        {
            titles.Add(s.Title);
        }

        return Task.FromResult(titles);    }

    public Task<IEnumerable<Subreddit>> Get(SingleSearchParameterDto searchParameterDto)
    {
        IEnumerable<Subreddit> subreddits = context.Subreddits.AsEnumerable();
        if (searchParameterDto.SearchParameter != null)
        {
            subreddits = context.Subreddits.Where(s => s.Title.ToLower().Contains(searchParameterDto.SearchParameter.ToLower()));
        }

        return Task.FromResult(subreddits);
    }
}