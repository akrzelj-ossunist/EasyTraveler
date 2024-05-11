using ET.Application.Models.CompanyDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.BusDtos.Response
{
    public class BusResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public bool IsAvailable { get; set; }
        public CompanyResponseDto Company { get; set; }
    }
}
