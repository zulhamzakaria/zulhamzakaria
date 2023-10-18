using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdvert.Search.Worker.Messages
{
    public class AdvertConfirmedMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
