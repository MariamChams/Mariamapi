namespace MariamPortfolio.DTOs.Blogposts
{
    public class CreateBlogpostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
    }
}