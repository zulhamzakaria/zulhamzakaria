using Application.Abstractions.Interfaces;
using Application.Commands.Details;
using Domain.Models;
using MediatR;

namespace Application.Commands.Handlers;

public class DeleteDetailsHandler : IRequestHandler<DeleteDetails>
{
    private readonly IDetailRepository _detailRepository;
    public DeleteDetailsHandler(IDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }
    public async Task Handle(DeleteDetails request, CancellationToken cancellationToken)
    {
        // TO DO : no delete function yet

        throw new NotImplementedException();
    }
}
