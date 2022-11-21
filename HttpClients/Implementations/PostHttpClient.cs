using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<Post>> Get(string subreddit, int? postId)
    {
        Console.WriteLine("Entered Get method in Post Service");
        String uri = "/Posts";
        uri += "?subreddit=" + subreddit;
        
        if (postId != null)
        {
            uri += "&postId=" + postId;
        }
        
        Console.WriteLine($"uri: {uri}");

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> Create(PostCreationDto creationDto)
    {
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/Posts", creationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result)!;
        return post;
    }

    public async Task<Post> Upvote(PostSearchParameters post)
    {
        var json = JsonContent.Create(post);
        HttpResponseMessage response = await client.PatchAsync("/Posts/upvote", json);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        Post upvotedPost = JsonSerializer.Deserialize<Post>(result)!;
        return upvotedPost;
    }

    public async Task<Post> Downvote(PostSearchParameters post)
    {
        var json = JsonContent.Create(post);
        HttpResponseMessage response = await client.PatchAsync("/Posts/downvote", json);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        Post upvotedPost = JsonSerializer.Deserialize<Post>(result)!;
        return upvotedPost;
    }

    public async Task<Comment> Comment(CommentCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Comments", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Comment comment = JsonSerializer.Deserialize<Comment>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comment;
    }
}