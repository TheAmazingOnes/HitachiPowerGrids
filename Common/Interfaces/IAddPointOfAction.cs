using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface IAddPointOfAction
    {
        int Id { get; }
        int PointOfActionId { get; }
        
        string Description { get; }
        string Category { get; }
        string Accountable { get; }
        string Project { get; }
        ActionPointStatuses Status { get; }
        DateTime ActionDate { get; }

        //För att kunna spara när handlingspunkten är avklarad
        DateTime ActionDateDone { get; }

        void FinishedPointOfAction(IPointOfAction pointOfAction);
    }
}
