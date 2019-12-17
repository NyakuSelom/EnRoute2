using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnRouteTicketing.Models
{
    public class Commuter
    {
        [Key]
        public int CommuterID { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public  string PhoneNumber { get; set; }
    }

    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        public int TicketID { get; set; }

        [Required]
        public string PhoneNumber { get; set; }



        public DateTime TransactionDate { get; set; } = DateTime.Now;


        public bool status { get; set; } = true;

        public Ticket Ticket{get; set;}
    }

    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int TicketID { get; set; }

        [Required]
        public int CommuterID { get; set; }

        public string ReviewMessage { get; set; }

        public int Rating { get; set; }

        public Ticket Ticket { get; set; }

        public Commuter Commuter { get; set; }
               
    }
}