using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYPrject
{
    class MyReservationsModel
    {
        public string Name { get; set; }
        public decimal MyBalance { get; set; }
        public IEnumerable<ReservationsModel> MyBookings { get; set; }
    }
}
