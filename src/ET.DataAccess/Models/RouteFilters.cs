using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Models
{
    public class RouteFilters
    {
        public string CompanyId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public string StartDate { get; set; }
        public string Price { get; set; }
        public string Company {  get; set; }
        public string People {  get; set; }
        public string Status { get; set; }
        public string SortBy { get; set; }
        public int Page {  get; set; }
        public int Size { get; set; }
        public string Bus {  get; set; }
    }
}
