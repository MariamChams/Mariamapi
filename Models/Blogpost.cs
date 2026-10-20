namespace MariamPortfolio.Models
{
    // model / object van een blog post
    public class Blogpost
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }
    }
}
