using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.WebModels;

namespace Tuscany.Utility
{
    public static class SD
    {
        public const string UserRole = "User";
        public const string AdminRole = "Admin";
        public const int OrderPendingId = 1002;

        public const int OrderEnded = 1;
        public const int OrderUpcoming = 2;
        public const int OrderPending = 1002;


        public static decimal CountPrice(OrderWeb order)
        {
            return (order.AdultCount * order.AdultCount) + (order.ChildCount * order.ChildPrice);
        }
    }
}
