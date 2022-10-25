using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideApiClient _slideApiClient;

        public SlideController(ISlideApiClient slideApiClient)
        {
            _slideApiClient = slideApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var viewModel = new SlideViewModel()
            {
                Slides = await _slideApiClient.GetAll()
            };
            return View(viewModel);
        }
    }
}
