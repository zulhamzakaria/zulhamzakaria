using Catalogue.DTO;
using Catalogue.Entities;

namespace   Catalogue
{
    public static class Extensions{
        public static ItemDTO AsDTO(this Item item){
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}