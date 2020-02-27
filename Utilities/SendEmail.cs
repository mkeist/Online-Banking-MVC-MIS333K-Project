using System;
using System.Net.Mail;
using System.Net;

namespace Team_4_Project.Utilities
{
    public static class EmailMessaging
    {
        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {
            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                //This is the SENDING email address and password
                Credentials = new NetworkCredential("donotreply.bevosbank@gmail.com", "Pa$$word123"),
                EnableSsl = true
            };
            //Add anything that you need to the body of the message
            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = "Dear Valued Customer, \n\n This is a notice that a dispute you submitted for a transaction was rejected. " +
                "To view the details or any comments left by the manager that reviewed your transaction, you can view" +
                "them on the transaction details. If you feel a mistake has been made, we encourage you to submit another dispute." +
                "\n\n Kind Regards, \n\n  Bevos Bank Management \n\n Please do not reply to this email.";


            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("donotreply.bevosbank@gmail.com", "DoNotReply: Bevo's Bank");
            MailMessage mm = new MailMessage();
            mm.Subject = "Transaction Dispute Rejected";
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }
    }
}
