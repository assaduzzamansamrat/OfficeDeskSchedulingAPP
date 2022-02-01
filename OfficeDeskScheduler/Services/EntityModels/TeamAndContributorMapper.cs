using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntityModels
{
    public class TeamAndContributorMapper
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long TeamId { get; set; }
        public long ContributorId { get; set; }
        public long ManagerId { get; set; }
        public string? ContributorName { get; set; }
        public bool IsInvaitationAccept { get; set; }
        public long ChoosedDeskId { get; set; }
        public string? ManagerName { get; set; }
        public string? TeamName { get; set; }

        

    }
}
