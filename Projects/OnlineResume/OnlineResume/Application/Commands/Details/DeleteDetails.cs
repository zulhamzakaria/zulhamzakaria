using MediatR;

namespace Application.Commands.Details;
public class DeleteDetails : IRequest
{
    public int Id { get; set; }
}
