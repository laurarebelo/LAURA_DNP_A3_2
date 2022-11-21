using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SubredditHttpClient : ISubredditService
{
    private HttpClient client;

    public SubredditHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    
    public async Task<Subreddit> Create(SubredditCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Subreddits", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Subreddit subreddit = JsonSerializer.Deserialize<Subreddit>(result)!;
        return subreddit;
    }

    public async Task<IEnumerable<String>> GetSubreddits()
    {
        HttpResponseMessage response = await client.GetAsync("/Subreddits/all");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<String> subreddits = JsonSerializer.Deserialize<IEnumerable<String>>(result)!;
        return subreddits;
    }
}