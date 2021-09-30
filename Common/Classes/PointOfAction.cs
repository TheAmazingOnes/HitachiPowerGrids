using Common.Enum;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    public class PointOfAction : IPointOfAction
    {
        public int Id { get; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Accountable { get; set; }
        public string Project { get; set; }
        public ActionPointStatuses Status { get; set; }
        public DateTime ActionDate { get; set; }

        public PointOfAction(int id, string description, string category, string accountable, string project, ActionPointStatuses status, DateTime actionDate)
        {
            Id = id;
            Description = description;
            Category = category;
            Accountable = accountable;
            Project = project;
            Status = status;
            ActionDate = actionDate;
        }
    }
}
