﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository
{
    public class ToursLanguageRepository : Repository<ToursLanguage>, IToursLanguageRepository
    {
        public ToursLanguageRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(ToursLanguage model)
        {
            dbSet.Update(model);
        }
    }
}
