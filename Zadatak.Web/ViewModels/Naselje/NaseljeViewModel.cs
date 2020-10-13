using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Web.ViewModels
{
    public class NaseljeViewModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }
        public int DrzavaId { get; set; }
        public DrzavaViewModel Drzava { get; set; }


        public static NaseljeViewModel DataToViewModel(Naselje naselje)
        {
            return new NaseljeViewModel
            {
                Id = naselje.Id,
                Naziv = naselje.Naziv,
                PostanskiBroj = naselje.PostanskiBroj,
                DrzavaId = naselje.DrzavaId,
                Drzava = DrzavaViewModel.DataToViewModel(naselje.Drzava)
            };
        }

        public Naselje ViewModelToData()
        {
            Naselje naselje = new Naselje();
            naselje.Naziv = Naziv;
            naselje.PostanskiBroj = PostanskiBroj;
            naselje.DrzavaId = DrzavaId;
            return naselje;
        }

    }
}
