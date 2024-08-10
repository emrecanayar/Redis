namespace RealTimeNotificationApp.Domain.Entities
{
    // Bu sınıf, bildirim modelini temsil eder
    public class Notification
    {
        // Bildirimin benzersiz kimliği
        public int Id { get; set; }

        // Bildirimin gideceği kullanıcı kimliği
        public int UserId { get; set; }

        // Bildirimin içeriği
        public string Message { get; set; } = string.Empty;

        // Bildirimin oluşturulduğu zaman
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
