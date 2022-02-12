using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntityModels
{
    public class DeskBookingDetails
    {
        public long Id { get; set; }
        public long DeskId { get; set; }
        public string? DeskNumber { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long BookedBy { get; set; }
        public long AssignedContributorId { get; set; }
        public string? AssignedContributorName { get; set; }
        public string? Map { get; set; }
        public string? Location { get; set; }
        public long TeamId { get; set; }
    }
}
