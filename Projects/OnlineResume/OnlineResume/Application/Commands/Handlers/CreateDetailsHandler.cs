using Application.Abstractions.Interfaces;
using Application.Commands.Details;
using Domain.Models;
using MediatR;

namespace Application.Commands.Handlers;
public class CreateDetailsHandler : IRequestHandler<CreateDetails, Detail>
{
    private readonly IDetailRepository _detailRepository;
    public CreateDetailsHandler(IDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }
    public async Task<Detail> Handle(CreateDetails request, CancellationToken cancellationToken)
    {
        Detail newDetails = new()
        {
           Name = request.Name,
           Role = request.Role,
           Email = request.Email,
           PhoneNo  = request.PhoneNo,
           Qualification = request.Qualification,
           Photo = request.Photo,
        };

        return await _detailRepository.CreateDetail(newDetails);
    }
}
