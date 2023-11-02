using Domain.Models;
using MediatR;

namespace Application.Commands.Details;
public class CreateDetails : IRequest<Detail>
{
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public string? Qualification { get; set; }
    public Uri? Photo { get; set; }
}
 