using Application.Models;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Application.Queries.ProductQueries
{
    public class GetProductsQuery : IRequest<ApiResponse<Product>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ApiResponse<Product>>
    {
        private IUnitOfWork _unitOfWork;
        private ICachingService<Product> _cachingService;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork, ICachingService<Product> cachingService)
        {
            _unitOfWork = unitOfWork;
            _cachingService = cachingService;
        }

        public async Task<ApiResponse<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<Product> res = new ApiResponse<Product>();
            try
            {
                var cachedData = await _cachingService.GetData("products");
                if (cachedData != null)
                {
                    res.responseOk(listData: cachedData);
                }
                else
                {
                    var data = await _unitOfWork.GetRepository<Product>().Get();
                    string cachedDataString = JsonSerializer.Serialize(data);
                    var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                           .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                           .SetSlidingExpiration(TimeSpan.FromMinutes(3));
                    await _cachingService.SetData("products", dataToCache, options);
                    res.responseOk(listData: data.ToList());
                }
            }
            catch (Exception ex)
            {
                res.ResponseError(message: ex.Message);
            }
            return res;
        }
    }
}