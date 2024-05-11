using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Route
    {
        public Guid Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateOnly StartDate { get; set; }
        public decimal Price { get; set; }
        public Bus Bus { get; set; }
        public int CurrentReservations { get; set; }
        public RouteStatus status { get; set; }
    }
}
