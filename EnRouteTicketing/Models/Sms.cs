using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace EnRouteTicketing.Models
{
    public class Sms
    {

        public string SendSms(string Phone, string message)
        {
            string sender_id = "EnRoute";
            //$key='jqUisbehcWTPpMwjKyc6phC3I';
            string key = "1R4EIzmWR4ITapF3ISSLgxpvv";
            string url = $"http://bulk.becsms.com/smsapi?key={key}&to={Phone}&msg={message}&sender_id={sender_id}";

            WebRequest request = WebRequest.Create(url);

            //var result = System.Diagnostics.Process.Start(url);
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                return result;
            }

            // return 
            //        switch (response){                                           
            //    case "1000":
            //    $outcome = "Message sent";
            //            break;
            //    case "1002":
            //    $outcome = "Message not sent";
            //            break;
            //    case "1003":
            //    $outcome = "You don't have enough balance";
            //            break;
            //    case "1004":
            //    $outcome = "Invalid API Key";
            //            break;
            //    case "1005":
            //    $outcome = "Phone number not valid";
            //            break;
            //    case "1006":
            //    $outcome = "Invalid Sender ID";
            //            break;
            //    case "1008":
            //        $outcome = "Empty message";
            //            break;
            //        }

            //        if ($result == 1000)
            //{
            //            echo $outcome." to ".$Phone;

            //        }
            //else
            //{
            //            echo $outcome;
            //        }

            //    }




        }
    }
}