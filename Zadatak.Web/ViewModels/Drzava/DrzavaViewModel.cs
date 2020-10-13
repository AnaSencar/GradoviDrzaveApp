using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Web.ViewModels
{
    public class DrzavaViewModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }

        public static DrzavaViewModel DataToViewModel(Drzava drzava)
        {
            return new DrzavaViewModel
            {
                Id = drzava.Id,
                Naziv = drzava.Naziv,
                Sifra = drzava.Sifra
            };
        }

    }
}
