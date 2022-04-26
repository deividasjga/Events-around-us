namespace RenginiaiWebApp.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public int? AA { get; set; }
        public int? BB { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public IFormFile Image { get; set; }
    }
}
