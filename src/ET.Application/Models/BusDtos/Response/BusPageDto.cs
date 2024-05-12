using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.BusDtos.Response
{
    public class BusPageDto
    {
        public List<BusResponseDto> Buses { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public int TotalPages { get; set; } = 1;
        public Dictionary<string, string> SearchParams { get; set; }
    }
}
