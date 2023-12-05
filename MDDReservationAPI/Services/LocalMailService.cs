using System.Net;
using System.Net.Mail;
using Serilog;

namespace MDDReservationAPI.Services;

public class LocalMailService: IMailService
{
     private readonly string? _mailFrom = string.Empty;
        private readonly string? _mailFromPassword = string.Empty;
        private readonly string? _mailSMTP = string.Empty;
        private readonly int _mailPort = 0;
        private readonly List<string>? _mailTo = new List<string>() { };

        public LocalMailService( IConfiguration configuration)
        {
            _mailFrom = configuration["MailService:mailFromAddress"];
            _mailFromPassword = configuration["MailService:mailFromPassword"];
            _mailSMTP = configuration["MailService:mailSMTP"];
            #if DEBUG
            _mailPort = Int32.Parse(configuration["MailService:mailNonSSLPort"] ?? string.Empty);
            #else
            _mailPort = Int32.Parse(configuration["MailService:mailSSLPort"]);
            #endif
            _mailTo = configuration.GetSection("MailService:mailsToAddress").Get<List<string>>();
        }




        public void Email(string subject, string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailFrom);
                for (int i = 0; i < _mailTo.Count; i++)
                {
                    message.To.Add(new MailAddress(_mailTo[i]));
                }

                //message.To.Add(new MailAddress(_mailTo[0]));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = _mailPort;
                smtp.Host = _mailSMTP;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailFrom, _mailFromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

}