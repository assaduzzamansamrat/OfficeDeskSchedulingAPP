using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntityModels
{
    public class OperationModel
    {
        public Team? Team { get; set; }=null;
        public List<User>? User { get; set; } = null;
        public Desk? Desk { get; set; } = null;
        public DeskBooking? DeskBooking { get; set; } = null;
    }
}
