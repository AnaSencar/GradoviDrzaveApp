using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Web.Interfaces;
using Zadatak.Web.ViewModels;

namespace Zadatak.Web.Services
{
    public class NaseljaService : INaseljaService
    {
        private readonly ZadatakContext _context;

        public NaseljaService(ZadatakContext context)
        {
            _context = context;
        }

        public List<NaseljeViewModel> GetFilteredNaselja(PaginationFilterViewModel filters)
        {
            IQueryable<Naselje> naselja = _context.Naselja.Include(x => x.Drzava);
            int pages = (int)Math.Ceiling((decimal)naselja.Count() / filters.PageSize);
            if(filters.PageNumber > pages)
            {
                filters.PageNumber = 1;
            }
            
            naselja = naselja.Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize);
            return naselja.Select(x => NaseljeViewModel.DataToViewModel(x)).ToList();
        }

        public int CountNaselja()
        {
            return _context.Naselja.Count();
        }

        public StatusCodeViewModel NewNaselje(NaseljeViewModel model)
        {
            if (String.IsNullOrEmpty(model.Naziv) || String.IsNullOrEmpty(model.PostanskiBroj) || model.DrzavaId == 0)
            {
                return new StatusCodeViewModel
                {
                    StatusCode = 400,
                    StatusMessage = "Neispravan unos"
                };
            }
            else
            {
                _context.Naselja.Add(model.ViewModelToData());
                _context.SaveChanges();
                return new StatusCodeViewModel
                {
                    StatusCode = 201,
                    StatusMessage = "Naselje kreirano"
                };
            }
        }

        public StatusCodeViewModel UpdateNaselje(NaseljeViewModel model)
        {
            if (String.IsNullOrEmpty(model.Naziv) || String.IsNullOrEmpty(model.PostanskiBroj) || model.DrzavaId == 0)
            {
                return new StatusCodeViewModel
                {
                    StatusCode = 400,
                    StatusMessage = "Neispravan unos"
                };
            }
            else
            {
                Naselje naselje = _context.Naselja.SingleOrDefault(x => x.Id == model.Id);

                if(naselje == null)
                {
                    return new StatusCodeViewModel
                    {
                        StatusCode = 404,
                        StatusMessage = "Naselje nije nadjeno"
                    };
                }

                naselje.Naziv = model.Naziv;
                naselje.PostanskiBroj = model.PostanskiBroj;
                naselje.DrzavaId = model.DrzavaId;

                _context.SaveChanges();
                return new StatusCodeViewModel
                {
                    StatusCode = 200,
                    StatusMessage = "Naselje azurirano"
                };
            }
        }

        public StatusCodeViewModel DeleteNaselje(int id)
        {
            Naselje naselje = _context.Naselja.SingleOrDefault(x => x.Id == id);
            if(naselje == null)
            {
                return new StatusCodeViewModel
                {
                    StatusCode = 404,
                    StatusMessage = "Naselje nije nadjeno"
                };
            }
            _context.Naselja.Remove(naselje);
            _context.SaveChanges();
            return new StatusCodeViewModel
            {
                StatusCode = 200,
                StatusMessage = "Naselje obrisano"
            };
        }

        
    }
}
