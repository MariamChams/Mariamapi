using MariamPortfolio.Data;
using MariamPortfolio.DTOs.Blogposts;
using MariamPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace MariamPortfolio.Services
{
    public class BlogpostService
    {
        // service kan praten met db via EF Core
        private readonly PortfolioDbContext _context;

        // Constructor
        // ASP.NET geeft automatisch PortfolioDbContext mee
        // omdat die in Program.cs met dependency injection geregistreerd is
        public BlogpostService(PortfolioDbContext context)
        {
            _context = context;
        }





        // haal alle blogposts op uit db
        public async Task<List<BlogpostDto>> GetAllAsync()
        {
            // _context.Blogposts verwijst naar de Blogposts DbSet
            // in PortfolioDbContext
            return await _context.Blogposts

                // zet elke blogpost db object om naar een BlogpostDto
                // zo stuur ik niet rechtstreeks EF Core entity naar api
                .Select(blogpost => new BlogpostDto
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    Content = blogpost.Content,
                    PublicationDate = blogpost.PublicationDate
                })

                // voer query uit en maak een lijst
                .ToListAsync();
        }








        // haal een blogpost op via id
        public async Task<BlogpostDto?> GetByIdAsync(int id)
        {
            // zoek in Blogposts naar blogpost met hetzelfde id
            return await _context.Blogposts
                .Where(blogpost => blogpost.Id == id)

                // zet Blogpost entity om naar BlogpostDto
                // zodat api niet rechtstreeks EF Core entity terugstuurt
                .Select(blogpost => new BlogpostDto
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    Content = blogpost.Content,
                    PublicationDate = blogpost.PublicationDate
                })

                // pak het eerste gevonden blogpost
                // als niks bestaat met dit id komt null terug
                .FirstOrDefaultAsync();
        }








        // maakt een nieuwe blogpost aan
        public async Task<BlogpostDto> CreateAsync(CreateBlogpostDto dto)
        {
            // maak van CreateBlogpostDto een echte Blogpost entity
            var blogpost = new Blogpost
            {
                Title = dto.Title,
                Content = dto.Content,
                PublicationDate = dto.PublicationDate
            };

            // vertel EF Core dat deze blogpost toegevoegd moet worden
            _context.Blogposts.Add(blogpost);

            // schrijf verandering echt naar SQL server
            await _context.SaveChangesAsync();

            // zet opgeslagen Blogpost entity om naar BlogpostDto
            return new BlogpostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                Content = blogpost.Content,
                PublicationDate = blogpost.PublicationDate
            };
        }







        // wijzigt bestaande blogpost via id
        public async Task<BlogpostDto?> UpdateAsync(
            int id,
            UpdateBlogpostDto dto)
        {
            // zoek eerst echte Blogpost entity in database
            var blogpost = await _context.Blogposts
                .FirstOrDefaultAsync(blogpost => blogpost.Id == id);

            // bestaat blogpost niet? geef null terug
            if (blogpost == null)
            {
                return null;
            }

            // verander de velden van de bestaande entity
            blogpost.Title = dto.Title;
            blogpost.Content = dto.Content;
            blogpost.PublicationDate = dto.PublicationDate;

            // wijzigingen opslaan in SQL server
            await _context.SaveChangesAsync();

            // gewijzigde entity omzetten naar DTO
            return new BlogpostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                Content = blogpost.Content,
                PublicationDate = blogpost.PublicationDate
            };
        }







        // verwijdert blogpost via id
        public async Task<bool> DeleteAsync(int id)
        {
            // zoek blogpost in database
            var blogpost = await _context.Blogposts
                .FirstOrDefaultAsync(blogpost => blogpost.Id == id);

            // bestaat blogpost niet?
            if (blogpost == null)
            {
                return false;
            }

            // vertel EF Core dat blogpost verwijderd moet worden
            _context.Blogposts.Remove(blogpost);

            // verandering echt opslaan in SQL server
            await _context.SaveChangesAsync();

            // verwijderen is gelukt
            return true;
        }
    }
}