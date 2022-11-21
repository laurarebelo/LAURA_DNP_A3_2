using Domain;
using Domain.DTOs;

namespace Application.DAOInterfaces;

public interface ISubredditDao
{
    public Task<Subreddit> Create(Subreddit subreddit);
    public Task<Subreddit?> GetByTitle(String title);
    public Task<List<string>> GetAllTitles();
    public Task<IEnumerable<Subreddit>> Get(SingleSearchParameterDto searchParameterDto);
}