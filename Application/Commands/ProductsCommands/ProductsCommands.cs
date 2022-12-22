using Application.Dtos.Product;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Commands.ProductsCommands
{
   public record AddProductRequest(AddProductDto model):IRequest<ApiResponse<Product>>;

    public class AddProductRequestHandler : IRequestHandler<AddProductRequest, ApiResponse<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddProductRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<Product>> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            ApiResponse<Product> res = new ApiResponse<Product>();
            Product p = new Product();
            p.Name = request.model.Name;
            p.Description = request.model.Description;
            p.Quantity= request.model.Quantity;
            try
            {
                await _unitOfWork.GetRepository<Product>().Create(p);
                await _unitOfWork.CommitTransactionAsync();
                res.ResponseOk();
            }
            catch(Exception ex) 
            {
                await _unitOfWork.RollBackTransactionAsync();
                res.ResponseError(message: ex.Message);
            }
            return res;
        }
    }
}
