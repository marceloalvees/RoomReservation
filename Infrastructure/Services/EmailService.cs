
//senha roomreservation123 inec ijwr vvbg cwhm
using System;
using System.Net;
using System.Net.Mail;
using Domain.Abstractions;
using Domain.Entities;


namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings = new EmailSettings();
      

        public async Task SendEmailAsync(string toEmail, string subject, string status)
        {
            try
            {
                string body = $"<h1>Hello, Reservation: {status}</h1>";
                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    Port= _emailSettings.Port,
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Define se o corpo da mensagem aceita HTML
                };

                mailMessage.To.Add(toEmail); // Adiciona destinatário

                await client.SendMailAsync(mailMessage);
                Console.WriteLine("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }
}
