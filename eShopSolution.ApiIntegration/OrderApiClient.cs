using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using eShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace eShopSolution.ApiIntegration
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public OrderApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<List<OrderVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<OrderVm>>>("/api/orders");
        }
        public async Task<bool> CreateOrder(OrderCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ShipName) ? "" : request.ShipName.ToString()), "Tên người đặt");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ShipAddress) ? "" : request.ShipAddress.ToString()), "Địa chỉ");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ShipEmail) ? "" : request.ShipEmail.ToString()), "Email");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ShipPhoneNumber) ? "" : request.ShipPhoneNumber.ToString()), "Số điện thoại");
            requestContent.Add(new StringContent(languageId), "languageId");

            
            var response = await client.PostAsync($"/api/orders/", requestContent);
            return response.IsSuccessStatusCode;
        }



    }
}
