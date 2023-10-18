/*
    Manages the DB transactions
    For eventual consistency and compensation
*/
using AdvertAPI.Models.Infrastructure;

namespace AdvertAPI.Models.Models
{
    public class ConfirmAdvert
    {
        public string Id { get; set; }
        public AdvertStatus Status { get; set; }
    }
}
