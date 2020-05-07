using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "hotels@example.com";
        public string MailFromAddress = "bookingshop@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\storehouse_emails";
    }
    
    public class EmailReservationProcessor : IReservationProcessor
    {
        private EmailSettings emailSettings;
        public EmailReservationProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessReservation(Entities.Cart cart, Entities.BookingDetails bookingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = @"c:\bookingshop_emails";
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Новая заявка обработана")
                .AppendLine("---")
                .AppendLine("Подробности заявки:");

                foreach (var line in cart.Lines)
                {
                    var sum = line.Hotel.PriceForPerson * bookingDetails.PersonAmount * bookingDetails.NightsAmount;
                    body.AppendFormat("Общая стоимость: {0:c}", sum);
                }

                body.AppendFormat(":")
                    .AppendLine("---")
                    .AppendLine(bookingDetails.FullName + "\n")
                    .AppendLine(bookingDetails.ArrivalDate + "\n")
                    .AppendLine(bookingDetails.PersonAmount + "\n")
                    .AppendLine(bookingDetails.NightsAmount + "\n")
                    .AppendLine("---")
                    .AppendLine("Почта для обратной связи: \n" + bookingDetails.Email);

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Новая заявка отправлена!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }

        public void ProcessPositiveResponse(Entities.Reservation order)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = @"c:\response_emails";
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Ваша заявка была одобрена")
                .AppendLine("---");

               string MailToAddress = order.Email;

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Ваша заявка была одобрена! Вскоре вам будет выслан счет для оплаты.",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
        public void ProcessNegativeResponse(Entities.Reservation order)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = @"c:\response_emails";
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Ваша заявка была отклонена")
                .AppendLine("---");

                string MailToAddress = order.Email;

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "К сожалению ваша заявка была отклонена! Попробуйте подать ее еще раз.",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
    }

