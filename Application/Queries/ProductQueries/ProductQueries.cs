using Application.Dtos.Product;
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Application.Queries.ProductQueries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductReadDto>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductReadDto>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductReadDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return  _mapper.Map<IEnumerable<ProductReadDto>>(await _productService.GetAll());
        }
    }
}