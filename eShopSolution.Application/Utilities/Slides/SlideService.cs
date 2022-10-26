using eShopSolution.Application.Common;
using eShopSolution.Application.System.Roles;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;
using eShopSolution.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Utilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public SlideService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }


        public async Task<List<SlideVm>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                .Select(x => new SlideVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image
                }).ToListAsync();

            return slides;
        }

        public async Task<int> Create(SlideCreateRequest request)
        {
            var slides = new Slide()
            {
                Name = request.Name,
                Description = request.Description,
                Image = await this.SaveFile(request.ThumbnailImage),
                Status = request.Status
            };

            _context.Slides.Add(slides);
            await _context.SaveChangesAsync();
            return slides.Id;
        }

        public async Task<int> Delete(int request)
        {
            var slides = await _context.Slides.FindAsync(request);
            if (slides == null) throw new EShopException($"Cannot find a slides: {request}");

            _context.Slides.Remove(slides);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(SlideUpdateRequest request)
        {
            var slides = await _context.Slides.FindAsync(request.Id);

            if (slides == null) throw new EShopException($"Cannot find a product with id: {request.Id}");

            slides.Name = request.Name;
            slides.Description = request.Description;
            slides.Url = request.Url;
            slides.SortOrder = request.SortOrder;
            slides.Status = request.Status;

            //Save image
            if (request.Image != null)
            {
                var thumbnailImage = await _context.Slides.FirstOrDefaultAsync(i => i.Image == request.Image);
                _context.Slides.Update(thumbnailImage);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<SlideVm>> GetAllPaging(GetSlidePagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Slides
                        select new { p };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new SlideVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    Url = x.p.Url,
                    Image = x.p.Image,
                    SortOrder = x.p.SortOrder,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<SlideVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
        public async Task<SlideVm> GetById(int id)
        {
            var query = from c in _context.Slides
                        where  c.Id == id
                        select new { c };
            return await query.Select(x => new SlideVm()
            {
                Id = x.c.Id,
                Name = x.c.Name
            }).FirstOrDefaultAsync();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}