using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface IDetailRepository
{
    Task<Detail> GetDetail();
    Task<Detail> CreateDetail(Detail detail);
    Task<Detail> UpdateDetail(Detail detail, int detailId);
}
