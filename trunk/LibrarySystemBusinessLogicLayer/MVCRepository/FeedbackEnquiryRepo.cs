using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystemBusinessLogicLayer;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class FeedbackEnquiryRepo
    {
        LibrarySystemDBEntities entities = new LibrarySystemDBEntities();
        const Int32 intialTicketNo = 1000;

        public FeedbackEnquiryRepo()
        {
        }

        public int GetTicketNo()
        {
            int TicketNo = intialTicketNo;
            try
            {
               var data = (from fe in entities.Feedbacks
                        select fe).OrderByDescending(x => x.TicketNo);

               if (data != null)
               {
                   TicketNo = data.First().TicketNo+1;
               }
                                
                return TicketNo;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return intialTicketNo;
            }

            
        }

        public void CreateFeedbackEnquiry(Feedback fe)
        {
            try
            {
                entities.AddToFeedbacks(fe);
                entities.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);                
            }
        }

        public IEnumerable<Feedback> GetAllTicketList()
        {
            return entities.Feedbacks.ToList();
        }

        public IEnumerable<Feedback> GetTicketListByNRIC(String NRIC)
        {
            IEnumerable<Feedback> data = entities.Feedbacks.Where(fe => fe.NRIC == NRIC).ToList();
            return data;
        }

        public IEnumerable<Feedback> GetTicketListByType(String Type)
        {
            IEnumerable<Feedback> data = entities.Feedbacks.Where(fe => fe.Type == Type).ToList();
            return data;
        }

        public IEnumerable<Feedback> GetTicketListByStatus(String Status)
        {
            IEnumerable<Feedback> data = entities.Feedbacks.Where(fe => fe.Status == Status).ToList();
            return data;
        }

        public IEnumerable<Feedback> GetTicketListByTicketNumber(String TicketNumber)
        {
            int intTicketNumber = Int32.Parse(TicketNumber);
            IEnumerable<Feedback> data = entities.Feedbacks.Where(fe => fe.TicketNo == intTicketNumber).ToList();
            return data;
        }

        public IEnumerable<Feedback> GetList()
        {
            return entities.Feedbacks.ToList();
        }
        public IEnumerable<Feedback> GetFeekbackByUser(string nric)
        {
            return from fb in entities.Feedbacks
                   where fb.NRIC == nric
                   select fb;
        }

        public Feedback GetFeekbackItem(int TicketNo)
        {
            return (from fb in entities.Feedbacks
                    where fb.TicketNo == TicketNo
                    select fb).FirstOrDefault();
        }

        public void Save()
        {
            entities.SaveChanges();
        }

    }
}
