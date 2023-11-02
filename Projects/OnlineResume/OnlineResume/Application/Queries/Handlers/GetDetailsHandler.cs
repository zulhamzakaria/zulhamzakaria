using Application.Abstractions.Interfaces;
using Application.Queries.Details;
using Domain.Models;
using MediatR;

namespace Application.Queries.Handlers;

public class GetDetailsHandler : IRequestHandler<GetDetails, Detail>
{
    private readonly IDetailRepository _detailRepository;
    public GetDetailsHandler(IDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }
    public async Task<Detail> Handle(GetDetails request, CancellationToken cancellationToken)
    {
        return await _detailRepository.GetDetail();
    }
}
