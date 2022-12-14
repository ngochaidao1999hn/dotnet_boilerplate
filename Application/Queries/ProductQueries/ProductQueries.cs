using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Queries.ProductQueries
{
    public class GetProductsQuery : IRequest<IQueryable<Product>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IQueryable<Product>>
    {
        private IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<Product>().Get();
        }
    }
}