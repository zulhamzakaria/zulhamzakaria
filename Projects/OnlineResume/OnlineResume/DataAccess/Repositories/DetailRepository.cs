using Application.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class DetailRepository : IDetailRepository
{
    private readonly OnlineResumeDbContext _context;

    public DetailRepository(OnlineResumeDbContext context)
    {
        _context = context;
    }
    public async Task<Detail> GetDetail()
    {
        var result = await _context!.Details!.FirstOrDefaultAsync();
        return result;
    }


    public async Task<Detail> CreateDetail(Detail detail)
    {
        _context!.Details!.Add(detail);
        await _context.SaveChangesAsync();
        return detail;
    }

    public async Task<Detail> UpdateDetail(Detail detail, int detailId)
    {
        var result = _context.Details!.FirstOrDefault(d => d.Id == detailId);

        result!.Role = string.IsNullOrEmpty(detail.Role) ? result.Role : detail.Role;
        result.Name = string.IsNullOrEmpty(detail.Name) ? result.Name : detail.Name;
        result.Email = string.IsNullOrEmpty(detail.Email) ? result.Email : detail.Email;
        result.PhoneNo = string.IsNullOrEmpty(detail.PhoneNo) ? result.PhoneNo : detail.PhoneNo;
        result.Qualification = string.IsNullOrEmpty(detail.Qualification) ? result.Qualification : detail.Qualification;
        result.Photo = string.IsNullOrEmpty(detail.Photo.OriginalString) ? result.Photo : detail.Photo;

        _context.Entry(result).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return result;
    }
}
