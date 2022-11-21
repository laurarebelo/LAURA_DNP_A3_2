

namespace Domain.DTOs
{
    public class PostBrowseDto
    {
        public string Title { get; set; }
        public int PostId { get; set; }

        public PostBrowseDto(string title, int postId)
        {
            Title = title;
            PostId = postId;
        }
    }
}