namespace MariamPortfolio.Models
{

    // model / object van een project
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string GitHubUrl { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}