using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnRouteTicketing.Models
{
    public class BusService
    {
               
        [Key]
        public int BusServiceID { get; set; }


        [Required(ErrorMessage = "Pls Enter the name of your Organisation")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Pls Enter the Registration No. of your Organisation")]
        public string ServiceRegistrationNo { get; set; }

        [Required(ErrorMessage = "Pls Enter a Contact no")]
        public string ServiceContactNo { get; set; }

        public string Email { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;



    }
}