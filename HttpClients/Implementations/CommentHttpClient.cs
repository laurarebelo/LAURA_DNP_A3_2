using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private HttpClient client;

    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Comment> Upvote(CommentVoteDto dto)
    {
        var json = JsonContent.Create(dto);
        HttpResponseMessage response = await client.PatchAsync("/Comments/upvote", json);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Comment upvotedComment = JsonSerializer.Deserialize<Comment>(result)!;
        return upvotedComment;
    }

    public async Task<Comment> Downvote(CommentVoteDto dto)
    {
        var json = JsonContent.Create(dto);
        HttpResponseMessage response = await client.PatchAsync("/Comments/downvote", json);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Comment downvotedComment = JsonSerializer.Deserialize<Comment>(result)!;
        return downvotedComment;
    }
}