namespace PlatformService.Dtos
{
    public class PlatformReadDTO
    {
        public Guid IdInstance { get; set; }

        public string? Name { get; set; }

        public string? Publisher { get; set; }
        
        public string? Cost { get; set; }
    }
}