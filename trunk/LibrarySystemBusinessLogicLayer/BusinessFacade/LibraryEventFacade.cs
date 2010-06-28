using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibrarySystemBusinessLogicLayer.MVCRepository;

namespace LibrarySystemBusinessLogicLayer.BusinessFacade
{
    public class LibraryEventFacade
    {
        LibrarySystemDBEntities LibraryContext;
        public LibraryEventFacade()
        {
            LibraryContext = new LibrarySystemDBEntities();

        }

        //public List<string> GetEventList()
        public  DataTable  GetEventList()
        {
            
            var eventLists = (from evt in LibraryContext.LibraryEvents
                              where evt.EventDate >= DateTime.Now
                              select evt).Take(20);

            //List<string> eventList = new List<string>();
            int Sno =1;

            DataTable dtab = new DataTable();
            DataColumn No = new DataColumn("No", Type.GetType("System.Int32"));
            DataColumn Title = new DataColumn("Title", Type.GetType("System.String"));
            DataColumn EventDate = new DataColumn("EventDate", Type.GetType("System.String"));
            dtab.Columns.Add(No);
            dtab.Columns.Add(Title);
            dtab.Columns.Add(EventDate);

            try
            {

                foreach (LibraryEvent evt in eventLists)
                {
                    DataRow itemRow = dtab.NewRow();
                    itemRow["No"] = Sno++;
                    itemRow["Title"] = evt.Title;
                    //itemRow["EventDate"] = evt.EventDate;
                    itemRow["EventDate"] = String.Format("{0:M/d/yyyy}", evt.EventDate);  
                    dtab.Rows.Add(itemRow);

                                       // eventList.Add(evt.Title + "  " + evt.EventDate.ToString());
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Execption verifying password. " +
                ex.Message);
            }
            return dtab;
        }


    }
}
