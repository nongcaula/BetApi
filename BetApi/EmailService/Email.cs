using System.Net.Mail;
using System.Net;
using BetApi.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace BetApi.EmailService
{
    public static class Email
    {

        public static void SendEmail(string email, string subject, string htmlMessage)
        {
            string fromMail = "betsoftwareamanda@gmail.com";
            string fromPassword = "mieanmeuofitqqih";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }



        public static string GenerateEmailBody(OrderDetails order)
        {
            var emailStr = "Dear Customer, <br>";
            emailStr += "I hope you are doing well. <br>";
            emailStr += "This email is to inform you that we have received your purchase. <br>"+ 
            "Order Number : " + order.OrderId +"<br>" +
            "Purchase Date : " + DateTime.Today.ToString("yyyy-MM-dd");
            emailStr += "<ul>";
            int count = 1;
            order.Order.ToList().ForEach(x =>
            {
              emailStr += "<li>" + "Item " + count + ":<br>" + "Product Name :" + x.ProductName + "<br> Quantity :" + x.OderdedQuantity + "<br> Price Per Item :R " + Math.Round(x.ProductPrice,2) + "</li> ";
              count++;
            });
            emailStr += "</ul>";

            emailStr += "<br> Total amount for your purchase :" + "R " + Math.Round(order.SubTotal, 2) + "<br><br>";
            emailStr += "King Regards <br>";
            emailStr += "Bet Software Team.";

            return emailStr;
        }
    }
}
