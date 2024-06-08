﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository.IRepository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Update(Ticket ticket);
    }
}
