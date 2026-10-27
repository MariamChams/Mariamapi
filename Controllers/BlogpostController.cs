using MariamPortfolio.DTOs.Blogposts;
using MariamPortfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace MariamPortfolio.Controllers
{
    // is API controller
    [ApiController]

    // Basisroute /api/blogposts
    [Route("api/blogposts")]
    public class BlogpostsController : ControllerBase
    {
        // controller gebruikt de BlogpostService niet rechtstreeks met db
        // var declareren
        private readonly BlogpostService _blogpostService;

        // ASP.NET geeft automatisch de BlogpostService mee
        // omdat die in Program.cs met dependency injection geregistreerd is
        // constructor
        public BlogpostsController(BlogpostService blogpostService)  
        {
            _blogpostService = blogpostService;
        }








        // GET /api/blogposts
        // haalt alle blogposts op
        [HttpGet]
        public async Task<ActionResult<List<BlogpostDto>>> GetAll()
        {
            // service vragen om blogposts
            var blogposts = await _blogpostService.GetAllAsync();

            // http 200 ok met blogposts terug
            return Ok(blogposts);
        }








        // GET /api/blogposts/1
        // haalt een blogpost op via id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BlogpostDto>> GetById(int id)
        {
            // service vragen om blogpost met dit id
            var blogpost = await _blogpostService.GetByIdAsync(id);

            // als blogpost niet bestaat geef 404 Not Found
            if (blogpost == null)
            {
                return NotFound();
            }

            // als blogpost bestaat geef 200 OK met blogpost terug
            return Ok(blogpost);
        }






        // POST /api/blogposts
        // maakt een nieuwe blogpost aan
        [HttpPost]
        public async Task<ActionResult<BlogpostDto>> Create(
            CreateBlogpostDto dto)
        {
            // service vragen om nieuwe blogpost aan te maken
            var blogpost = await _blogpostService.CreateAsync(dto);

            // 201 Created teruggeven
            // en verwijzen naar GET /api/blogposts/{id}
            return CreatedAtAction(
                nameof(GetById),
                new { id = blogpost.Id },
                blogpost);
        }







        // PUT /api/blogposts/1
        // wijzigt bestaande blogpost via id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BlogpostDto>> Update(
            int id,
            UpdateBlogpostDto dto)
        {
            // service vragen om blogpost te wijzigen
            var blogpost = await _blogpostService.UpdateAsync(id, dto);

            // blogpost met dit id bestaat niet
            if (blogpost == null)
            {
                return NotFound();
            }

            // gewijzigde blogpost teruggeven met 200 OK
            return Ok(blogpost);
        }






        // DELETE /api/blogposts/1
        // verwijdert blogpost via id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // service vragen om blogpost te verwijderen
            var deleted = await _blogpostService.DeleteAsync(id);

            // blogpost bestaat niet
            if (!deleted)
            {
                return NotFound();
            }

            // blogpost verwijderd
            // 204 betekent gelukt maar geen inhoud terugsturen
            return NoContent();
        }
    }
}