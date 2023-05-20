using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ICachingService<Product> _cachingService;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(ICachingService<Product> cachingService, IUnitOfWork unitOfWork)
        {
            _cachingService = cachingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            _unitOfWork.GetRepository<Product>().Delete(id);
            return await _unitOfWork.CommitTransactionAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {

            var data = await _unitOfWork.GetRepository<Product>().GetAsync();
            return data;
        }

        public async Task<Product> Insert(Product product)
        {
            var entity = await _unitOfWork.GetRepository<Product>().CreateAsync(product);
            await _unitOfWork.CommitTransactionAsync();
            return entity;
        }

        public async Task<bool> Update(Product product)
        {
            _unitOfWork.GetRepository<Product>().Update(product);
            return await _unitOfWork.CommitTransactionAsync();
        }
    }
}
