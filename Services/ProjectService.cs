using MariamPortfolio.Data;
using MariamPortfolio.DTOs.Projects;
using MariamPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace MariamPortfolio.Services
{
    public class ProjectService
    {
        // service kan praten met db via EF Core
        private readonly PortfolioDbContext _context;

        // Constructor
        // ASP.NET geeft automatisch PortfolioDbContext mee
        // omdat die in Program.cs met dependency injection geregistreerd is
        public ProjectService(PortfolioDbContext context)
        {
            _context = context;
        }







        // haal alleprojecten op uit db
        public async Task<List<ProjectDto>> GetAllAsync()
        {
            // _context.Projects verwijst naar de Projects DbSet
            // in PortfolioDbContext
            return await _context.Projects

                // Zet elke project db object om naar een ProjectDto
                // Zo stuur ik niet rechtstreeks EF Core entity naar api
                .Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Category = project.Category,
                    GitHubUrl = project.GitHubUrl,
                    Date = project.Date
                })

                // Voer query en maakt een lijst van
                .ToListAsync();
        }









        // haal één project op via id
        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            // zoek in Projects naar het project met hetzelfde id
            return await _context.Projects
                .Where(project => project.Id == id)

                // zet Project entity om naar ProjectDto
                // zodat de api niet rechtstreeks de EF Core entity terugstuurt
                .Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Category = project.Category,
                    GitHubUrl = project.GitHubUrl,
                    Date = project.Date
                })

                // pak het eerste gevonden project
                // als er niks bestaat met dit id komt null terug
                .FirstOrDefaultAsync();
        }







        // maakt een nieuw project aan
        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            // maak van CreateProjectDto een echte Project entity
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                GitHubUrl = dto.GitHubUrl,
                Date = dto.Date
            };

            // vertel EF Core dat project toegevoegd moet worden
            _context.Projects.Add(project);

            // sla verandering echt op in SQL Server
            await _context.SaveChangesAsync();

            // entity weer omzetten naar DTO
            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                GitHubUrl = project.GitHubUrl,
                Date = project.Date
            };
        }








        // wijzigt bestaand project via id
        public async Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto)
        {
            // zoek eerst echte Project entity in database
            var project = await _context.Projects
                .FirstOrDefaultAsync(project => project.Id == id);

            // bestaat project niet? geef null terug
            if (project == null)
            {
                return null;
            }

            // verander de velden van de bestaande entity
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Category = dto.Category;
            project.GitHubUrl = dto.GitHubUrl;
            project.Date = dto.Date;

            // wijzigingen opslaan in SQL Server
            await _context.SaveChangesAsync();

            // gewijzigde entity omzetten naar DTO
            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                GitHubUrl = project.GitHubUrl,
                Date = project.Date
            };
        }









        // verwijdert een project via id
        public async Task<bool> DeleteAsync(int id)
        {
            // zoek project in database
            var project = await _context.Projects
                .FirstOrDefaultAsync(project => project.Id == id);

            // bestaat project niet?
            if (project == null)
            {
                return false;
            }

            // vertel EF Core dat project verwijderd moet worden
            _context.Projects.Remove(project);

            // verandering echt opslaan in SQL Server
            await _context.SaveChangesAsync();

            // verwijderen is gelukt
            return true;
        }
    }
}