using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.Models;
using Tuscany.WebModels;

namespace Tuscany.DataAccess.Repository.IRepository
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        void Update(Gallery gallery);
        
    }
}
