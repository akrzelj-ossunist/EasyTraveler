using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateOnly StartDate { get; set; }
        public int MaxPassengers { get; set; }
        public int CurrentReservations { get; set; } = 0;

    }
}
