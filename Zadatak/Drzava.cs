using System;
using System.Collections.Generic;
using System.Text;

namespace Zadatak
{
    public class Drzava
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public List<Naselje> Naselja { get; set; }
    }
}
