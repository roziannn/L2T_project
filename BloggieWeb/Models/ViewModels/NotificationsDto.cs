namespace BloggieWeb.Models.ViewModels
{
    public class NotificationsDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
    }
}
