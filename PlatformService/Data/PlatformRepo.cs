using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if(platform != null)
            _context.Platforms.Add(platform);
            else
            throw new ArgumentNullException(nameof(platform));
        }

        public IEnumerable<Platform> GetAllPlatfrom()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(string id)
        {
            return _context.Platforms.Where(x=>x.IdInstance == Guid.Parse(id)).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}