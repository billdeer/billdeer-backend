using Billdeer.Core.Entities.Concrete;
using Castle.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Utilities.Mail
{
    public class EmailSender
    {
        //private readonly IConfiguration _configuration;
        //public EmailSender(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public static void Send(User user, IMailService mailService)
        {
            EmailAddress emailAddressTo = new EmailAddress();
            emailAddressTo.Name = $"{user.FirstName} {user.LastName}";
            emailAddressTo.Address = $"{user.Email}";

            var toList = new List<EmailAddress>();
            toList.Add(emailAddressTo);
            //..
            EmailAddress emailAddressFrom = new EmailAddress();
            emailAddressFrom.Name = "billdeer";
            emailAddressFrom.Address = "billdeer@gmail.com";

            var fromList = new List<EmailAddress>();
            fromList.Add(emailAddressFrom);
            //...
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.ToAddresses = toList;
            emailMessage.FromAddresses = fromList;
            emailMessage.Subject = "Deneme";
            emailMessage.Content = "Deneme 1";

            mailService.Send(emailMessage);
        }
    }
}
