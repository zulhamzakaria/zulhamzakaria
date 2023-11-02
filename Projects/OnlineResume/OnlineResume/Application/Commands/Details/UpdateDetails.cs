using Domain.Models;
using MediatR;

namespace Application.Commands.Details;

public class UpdateDetails : IRequest<Detail>
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public string? Qualification { get; set; }
    public Uri? Photo { get; set; }
}
