namespace ET.Application.Models.TicketDtos.Response
{
    public class TicketPageDto
    {
        public List<TicketResponseDto> Tickets { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public int TotalPages { get; set; } = 1;
        public Dictionary<string, string> SearchParams { get; set; }
    }
}
