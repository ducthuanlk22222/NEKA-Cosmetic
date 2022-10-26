using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Products;
using eShopSolution.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopSolution.AdminApp.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideApiClient _slideApiClient;
        private readonly IConfiguration _configuration;

        public SlideController(ISlideApiClient slideApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _slideApiClient = slideApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {

            var request = new GetSlidePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var data = await _slideApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;


            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] SlideCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _slideApiClient.CreateSlides(request);
            if (result)
            {
                TempData["result"] = "Thêm tin mới thành công";
                return RedirectToAction("Index");
            }
                
            ModelState.AddModelError("", "Thêm tin mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var slide = await _slideApiClient.GetById(id);
            var editVm = new SlideUpdateRequest()
            {
                Id = slide.Id,
                Description = slide.Description,
                SortOrder = slide.SortOrder,
                Status = slide.Status
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] SlideUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _slideApiClient.UpdateSlides(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new SlideDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SlideDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _slideApiClient.DeleteSlides(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa tin thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}
