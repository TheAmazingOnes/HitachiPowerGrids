using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Common.Enum;
using Dapper;
using System.Configuration;

namespace Common.Classes
{
    public class DayEventService : IDayEventService
    {
        DayEvent _oDayEvent = new DayEvent();
        List<DayEvent> _oDayEvents = new List<DayEvent>();
        public IConfiguration Configuration { get; }
        public DayEventService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Delete(int id)
        {
            string message = "";
            try
            {
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DayEventId", id);

                using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DagligStyrningDB")))
                {
                    con.Open();
                    string q = "";


                    q = @"DELETE FROM   DayEvent
                            WHERE DayEventId = @DayEventId";
                    con.Execute(q, parameters);
                    message = "Deleted";

                }
            }

            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


        public DayEvent GetEvent(DateTime eventDate)
        {
            _oDayEvent = new DayEvent();
            using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DagligStyrningDB")))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate = '{0}'", eventDate.ToString("dd-MMM-yyyy"));

                var oDayEvents = con.Query<DayEvent>(sql).ToList();

                if (oDayEvents != null && oDayEvents.Count() > 0)
                {
                    _oDayEvent = oDayEvents.SingleOrDefault();
                }

                else
                {
                    _oDayEvent.EventDate = eventDate;
                    _oDayEvent.FromDate = eventDate;
                    _oDayEvent.ToDate = eventDate;
                }
            }

            return _oDayEvent;

        }


        public List<DayEvent> GetEvents(DateTime fromDate, DateTime toDate)
        {
            _oDayEvents = new List<DayEvent>();
            using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DagligStyrningDB")))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate BETWEEN '{0}' AND '{1}'", fromDate.ToString("dd-MMM-yyyy"), toDate.ToString("dd-MMM-yyyy"));

                var oDayEvents = con.Query<DayEvent>(sql).ToList();

                if (oDayEvents != null && oDayEvents.Count() > 0)
                {
                    _oDayEvents = oDayEvents;
                }
            }

            return _oDayEvents;
        }

        public DayEvent SaveOrUpdate(DayEvent oDayEvent)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Note", oDayEvent.Note);
                parameters.Add("@EventDate", oDayEvent.EventDate);
                parameters.Add("@DayEventId", oDayEvent.DayEventId);
                parameters.Add("@Safety", oDayEvent.Safety);
                parameters.Add("@Quality", oDayEvent.Quality);
                parameters.Add("@Delivery", oDayEvent.Delivery);
                parameters.Add("@Material", oDayEvent.Material);
                parameters.Add("@Tidy", oDayEvent.Tidy);
                parameters.Add("@Actions_Id", oDayEvent.ActionsOfTheDay);

                using (IDbConnection con = new SqlConnection(Configuration.GetConnectionString("DagligStyrningDB")))
                {
                    con.Open();
                    string q = "";

                    if (oDayEvent.DayEventId == 0)
                    {
                        q = @"
                        INSERT 
                        INTO   DayEvent(Note, EventDate, Safety, Quality, Delivery, Material, Tidy)
                        VALUES (@Note,@EventDate,@Safety, @Quality, @Delivery, @Material, @Tidy); SELECT SCOPE_IDENTITY() ";
                    }
                    else
                    {
                        q = @"UPDATE   DayEvent
                            SET   Note = @Note, EventDate = @EventDate, Safety = @Safety, Quality = @Quality, Delivery = @Delivery, Material = @Material, Tidy = @Tidy
                            WHERE DayEventId = @DayEventId";
                    }

                    if (oDayEvent.DayEventId == 0)
                    {
                        oDayEvent.DayEventId = con.ExecuteScalar<int>(q, parameters);
                    }
                    else
                    {
                        con.Execute(q, parameters);
                    }

                    //var oDayEvents = con.Query<DayEvent>("SP_DayEvent",
                    //    this.SetParameters(oDayEvent, operationType),
                    //    commandType: CommandType.StoredProcedure);

                    //if (oDayEvents != null && oDayEvents.Count() > 0)
                    //{
                    //    _oDayEvent = oDayEvents.FirstOrDefault();
                    //}
                }
            }
            catch (Exception ex)
            {
                oDayEvent.Message = ex.Message;
            }
            return oDayEvent;
        }

        private DynamicParameters SetParameters(DayEvent oDayEvent, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@DayEventId", oDayEvent.DayEventId);
            parameters.Add("@Note", oDayEvent.Note);
            parameters.Add("@EventDate", oDayEvent.EventDate);
            parameters.Add("@OperationType", operationType);
            parameters.Add("@Safety", oDayEvent.Safety);
            parameters.Add("@Quality", oDayEvent.Quality);
            parameters.Add("@Delivery", oDayEvent.Delivery);
            parameters.Add("@Material", oDayEvent.Material);
            parameters.Add("@Tidy", oDayEvent.Tidy);
            parameters.Add("@Actions_Id", oDayEvent.ActionsOfTheDay);

            return parameters;
        }
    }
}
