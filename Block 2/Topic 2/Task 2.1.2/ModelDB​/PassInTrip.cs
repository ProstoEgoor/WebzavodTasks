using System;
using System.Collections.Generic;

#nullable disable

namespace Task_2._1._2.ModelDB​
{
    public partial class PassInTrip
    {
        public int TripNo { get; set; }
        public DateTime Date { get; set; }
        public int IdPsg { get; set; }
        public string Place { get; set; }

        public virtual Passenger IdPsgNavigation { get; set; }
        public virtual Trip TripNoNavigation { get; set; }
    }
}
