using Application.Abstractions.Interfaces;
using Application.Commands.Details;
using Domain.Models;
using MediatR;

namespace Application.Commands.Handlers;
public class UpdateDetailsHandler : IRequestHandler<UpdateDetails, Detail>
{
    private readonly IDetailRepository _detailRepository;
    public UpdateDetailsHandler(IDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }
    public async Task<Detail> Handle(UpdateDetails request, CancellationToken cancellationToken)
    {
        var details = new Detail()
        {
            Role = request.Role,
            Name = request.Name,
            Email = request.Email,
            PhoneNo = request.PhoneNo,
            Qualification = request.Qualification,
            Photo = request.Photo
        };

        var result = await _detailRepository.UpdateDetail(details, request.Id);
        return result;

    }
}
