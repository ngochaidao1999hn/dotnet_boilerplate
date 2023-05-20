using Application.Dtos.Product;
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Commands.ProductsCommands
{
    public record AddProductRequest(AddProductDto model) : IRequest<Product>;

    public class AddProductRequestHandler : IRequestHandler<AddProductRequest, Product>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public AddProductRequestHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Product> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            var p = _mapper.Map<Product>(request.model);
            return await _productService.Insert(p);            
        }
    }
}