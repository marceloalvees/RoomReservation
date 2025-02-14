namespace Domain.Entities
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string SenderEmail { get; set; } = "roomreservation100@gmail.com";
        public string SenderName { get; set; } = "roomreservation100@gmail.com";
        public string Password { get; set; } = "inec ijwr vvbg cwhm";
        public bool UseSsl { get; set; } = true;
    }
}
