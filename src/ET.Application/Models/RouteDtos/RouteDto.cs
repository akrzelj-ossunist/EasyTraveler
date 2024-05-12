using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.RouteDtos
{
    public class RouteDto
    {
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateOnly StartDate { get; set; }
        public decimal Price { get; set; }
        public Guid BusId { get; set; }
    }
}
