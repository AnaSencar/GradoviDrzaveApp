using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Web.Interfaces;
using Zadatak.Web.ViewModels;

namespace Zadatak.Web.Services
{
    public class DrzavaService : IDrzavaService
    {
        private readonly ZadatakContext _context;

        public DrzavaService(ZadatakContext context)
        {
            _context = context;
        }

        public List<DrzavaViewModel> GetDrzave(string searchQuery)
        {
            if (String.IsNullOrEmpty(searchQuery))
            {
                return _context.Drzave.OrderBy(x => x.Naziv).Take(5).Select(x => DrzavaViewModel.DataToViewModel(x)).ToList();
            }
            return _context.Drzave.Where(x => x.Sifra.StartsWith(searchQuery)).Take(5).Select(x => DrzavaViewModel.DataToViewModel(x)).ToList();
        }
    }
}
