using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntityModels
{
    public class TeamDetails
    {

       public long Id { get; set; }
        public long ManagerId { get; set; }
        public string? TeamName { get; set; }
        public int TeamSize { get; set; }
        public string? EquipmentDetails { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public long EditedBy { get; set; }
        public string ManagerName { get; set; }
    }
}
