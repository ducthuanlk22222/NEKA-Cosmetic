//using eShopSolution.Utilities.Constants;
//using eShopSolution.ViewModels.Catalog.Carts;
//using eShopSolution.ViewModels.Catalog.Orders;
//using eShopSolution.ViewModels.Catalog.Products;
//using eShopSolution.ViewModels.Common;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace eShopSolution.ApiIntegration
//{
//    public class CartApiClient : BaseApiClient, ICartApiClient
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IHttpClientFactory _httpClientFactory;
//        private readonly IConfiguration _configuration;

//        public CartApiClient(IHttpClientFactory httpClientFactory,
//                   IHttpContextAccessor httpContextAccessor,
//                    IConfiguration configuration)
//            : base(httpClientFactory, httpContextAccessor, configuration)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _configuration = configuration;
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<bool> CreateCart(CartCreateRequest request)
//        {
//            var sessions = _httpContextAccessor
//                .HttpContext
//                .Session
//                .GetString(SystemConstants.AppSettings.Token);

//            var client = _httpClientFactory.CreateClient();
//            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
//            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

//            var requestContent = new MultipartFormDataContent();

//            requestContent.Add(new StringContent(request.Price.ToString()), "Price");
//            requestContent.Add(new StringContent(request.Quantity.ToString()), "Quantity");
//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AppUser) ? "" : request.SeoTitle.ToString()), "seoTitle");

//            var response = await client.PostAsync($"/api/products/", requestContent);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> UpdateCart(CartUpdateRequest request)
//        {
//            var sessions = _httpContextAccessor
//                .HttpContext
//                .Session
//                .GetString(SystemConstants.AppSettings.Token);

//            var client = _httpClientFactory.CreateClient();
//            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
//            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

//            var requestContent = new MultipartFormDataContent();

//            if (request.ThumbnailImage != null)
//            {
//                byte[] data;
//                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
//                {
//                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
//                }
//                ByteArrayContent bytes = new ByteArrayContent(data);
//                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
//            }

//            //requestContent.Add(new StringContent(request.Id.ToString()), "id");

//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");

//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Details) ? "" : request.Details.ToString()), "details");
//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
//            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
//            requestContent.Add(new StringContent(languageId), "languageId");

//            var response = await client.PutAsync($"/api/products/" + request.Id, requestContent);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<PagedResult<CartVm>> GetPagings(GetCartPagingRequest request)
//        {
//            var temp = $"/api/products/paging?pageIndex={request.PageIndex}" +
//                $"&pageSize={request.PageSize}" +
//                $"&keyword={request.Keyword}&languageId={request.LanguageId}&categoryId={request.CategoryId}";
//            var data = await GetAsync<PagedResult<ProductVm>>(temp);

//            return data;
//        }

//        public async Task<bool> DeleteProduct(int id)
//        {
//            return await Delete($"/api/products/" + id);
//        }
//    }
//}
//}
