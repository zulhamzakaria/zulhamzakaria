using System;
using System.Collections.Generic;
using System.Text;
using WebAdvert.Search.Worker.Messages;

namespace WebAdvert.Search.Worker.Helpers
{
    public static class MappingHelper
    {
        public static AdvertType Map(AdvertConfirmedMessage message)
        {
            var doc = new AdvertType
            {
                Id = message.Id,
                Title = message.Title,
                CreationDateTime = DateTime.UtcNow
            };

            return doc;
        }
    }
}
