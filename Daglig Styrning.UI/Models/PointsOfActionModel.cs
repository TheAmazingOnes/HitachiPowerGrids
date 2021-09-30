using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daglig_Styrning.UI.Models
{
    public class PointsOfActionModel
    {
        public int PointOfActionId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Accountable { get; set; }
        public string Project { get; set; }
        public string Status { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
