using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EnRouteTicketing.Models
{
    public class Terminal
    {
        [Key]
        public int TerminalID { get; set; }

        [Required(ErrorMessage = "Please Enter the location of Terminal")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please Enter a Name for the Terminal")]
        public string TerminalName { get; set; }


        public int BusServiceID { get; set; }

        public virtual BusService BusService { get; set; }

        public virtual ICollection<Ticket> DepartTerminals { get; set; }

        public virtual ICollection<Ticket> ArriveTerminals { get; set; }

    }



    public class Bus
    {
        [Key]
        public int BusID { get; set; }

        [Required(ErrorMessage = "Please Enter the license Number for your Bus")]
        public string LicenseNo { get; set; }

        [Required(ErrorMessage = "Please Enter the model for your Bus")]
        public string Busmodel { get; set; }

        [Required(ErrorMessage = "Please Enter the no of seats the Bus")]
        public int NoofSeats { get; set; }


        public int BusServiceID { get; set; }

        public int TerminalID { get; set; }

        public virtual Terminal Terminal { get; set; }

        public virtual BusService BusService { get; set; }

    }

    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        public string TicketNumber { get; set; }

        [Required]
        public int SeatNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime TravelDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm:ss}")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }

        public Double Price { get; set; }

        public bool Status { get; set; }


        [Required]
        public int BusServiceID { get; set; }

        [Required]
        public int BusID { get; set; }


        public int DepartTerminalID { get; set; }


        public int ArriveTerminalID { get; set; }


        [ForeignKey("DepartTerminalID")]
        [InverseProperty("DepartTerminals")]
        public virtual Terminal DepartTerminal { get; set; }


        [ForeignKey("ArriveTerminalID")]
        [InverseProperty("ArriveTerminals")]
        public virtual Terminal ArriveTerminal { get; set; }



        public virtual BusService BusService { get; set; }


        public virtual Bus Bus { get; set; }


    }
}