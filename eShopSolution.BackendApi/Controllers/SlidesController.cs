using System.Threading.Tasks;
using eShopSolution.Application.Utilities.Slides;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlideService _slideService;

        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var slides = await _slideService.GetAll();
            return Ok(slides);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetSlidePagingRequest request)
        {
            var slides = await _slideService.GetAllPaging(request);
            return Ok(slides);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] SlideCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slideIds = await _slideService.Create(request);
            if (slideIds == 0)
                return BadRequest();

            var slides = await _slideService.GetById(slideIds);

            return CreatedAtAction(nameof(GetById), new { id = slideIds }, slides);
        }

        [HttpGet("{slideID}")]
        public async Task<IActionResult> GetById(int slideID)
        {
            var slide = await _slideService.GetById(slideID);
            if (slide == null)
                return BadRequest("Cannot find slides");
            return Ok(slide);
        }

        [HttpPut("{slideID}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int slideId, [FromForm] SlideUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = slideId;
            var affectedResult = await _slideService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{slideID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int slideID)
        {
            var affectedResult = await _slideService.Delete(slideID);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}