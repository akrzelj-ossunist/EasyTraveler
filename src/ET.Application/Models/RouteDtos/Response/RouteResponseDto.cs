using ET.Core.Entities;
using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.RouteDtos.Response
{
    public class RouteResponseDto
    {
        public Guid Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public Bus Bus { get; set; }
        public int CurrentReservations { get; set; }
        public RouteStatus Status { get; set; }
    }
}
