using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(Language language)
        {
            _db.Update(language);
        }
    }
}
