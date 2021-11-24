using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
         bool SaveChanges();
         
         IEnumerable<Platform> GetAllPlatfrom();
         Platform GetPlatformById(string id);
         void CreatePlatform(Platform platform);
    }
}