using MariamPortfolio.DTOs.Projects;
using MariamPortfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace MariamPortfolio.Controllers
{
    // is API controller
    [ApiController]

    // Basisroute /api/projects
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        // controller gebruikt de ProjectService niet rechtsstreek met db
        private readonly ProjectService _projectService;

        // ASP.NET geeft automatisch de ProjectService mee
        // omdat die in Program.cs met dependency injection geregistreerd is
        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }







        // GET /api/projects
        // Haalt alle projecten op
        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAll()
        {
            // service vragen om projecten
            var projects = await _projectService.GetAllAsync();

            // http 200 ok met projecten terug
            return Ok(projects);
        }







        // GET /api/projects/1
        // haalt een project op via id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            // service vragen om project met dit id
            var project = await _projectService.GetByIdAsync(id);

            // als project niet bestaat geef 404 Not Found
            if (project == null)
            {
                return NotFound();
            }

            // als project bestaat geef 200 OK met project terug
            return Ok(project);
        }





        // POST /api/projects
        // maakt een nieuw project aan
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
        {
            // service vragen om nieuw project aan te maken
            var project = await _projectService.CreateAsync(dto);

            // 201 Created teruggeven
            // en verwijzen naar GET /api/projects/{id}
            return CreatedAtAction(
                nameof(GetById),
                new { id = project.Id },
                project);
        }






        // PUT /api/projects/1
        // wijzigt bestaand project via id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProjectDto>> Update(
            int id,
            UpdateProjectDto dto)
        {
            // service vragen om project te wijzigen
            var project = await _projectService.UpdateAsync(id, dto);

            // project met dit id bestaat niet
            if (project == null)
            {
                return NotFound();
            }

            // gewijzigd project teruggeven met 200 OK
            return Ok(project);
        }





        // DELETE /api/projects/1
        // verwijdert project via id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // service vragen om project te verwijderen
            var deleted = await _projectService.DeleteAsync(id);

            // project bestaat niet
            if (!deleted)
            {
                return NotFound();
            }

            // project verwijderd
            // 204 gelukt maar geen inhoud terugsturen
            return NoContent();
        }
    }
}