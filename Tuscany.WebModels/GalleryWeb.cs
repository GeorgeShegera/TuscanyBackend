using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.Models;

namespace Tuscany.WebModels
{
    public class GalleryWeb
    {
        public int Id { get; set; }
        public string? Img { get; set; } = "";
        public int TourId { get; set; } = 0;

        public GalleryWeb() { }

        public GalleryWeb(Gallery gallery)            
        {
            Id = gallery.Id;
            Img = gallery.Img;
            TourId = gallery.TourId;
        }
    }
}
