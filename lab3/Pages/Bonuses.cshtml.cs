using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using lab3.Models;
using Lab3.Services;

namespace lab3.Pages
{
    public class BonusesModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public List<Bonus> Bonuses { get; set; }

        public BonusesModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void OnGet()
        {
            Bonuses = _cache.Get<List<Bonus>>("Bonuses");

            if (Bonuses == null)
            {
                var dataReader = new DataReader(_cache);
                Bonuses = dataReader.GetBonuses();

                _cache.Set("Bonuses", Bonuses, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) 
                });
            }
        }
    }
}
