using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Classes
{
    public class DayEvent
    {
        public int DayEventId { get; set; }
        public string Note { get; set; }
        public DateTime EventDate { get; set; } = new DateTime(2021, 1, 1);

        public DateTime FromDate { get; set; } = new DateTime(2021, 1, 1);
        public DateTime ToDate { get; set; } = new DateTime(2021, 1, 1);
        public string DateValue { get; set; }
        public string DayName { get; set; }
        public string Message { get; set; }
        public string Safety { get; set; } = "";
        public string Quality { get; set; } = "";
        public string Delivery { get; set; } = "";
        public string Material { get; set; } = "";
        public string Tidy { get; set; } = "";

        public ActionsOfTheDay ActionsOfTheDay { get; set; }
        public int Actions_Id { get; set; }
        public string ActionsOfTheDayTypes { get; set; } = "";
    }
}
