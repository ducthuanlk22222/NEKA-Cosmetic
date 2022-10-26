using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Utilities.Slides
{
    public class SlideCreateRequest
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public int SortOrder { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public Status Status { get; set; }
    }
}
