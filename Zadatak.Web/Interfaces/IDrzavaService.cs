using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Web.ViewModels;

namespace Zadatak.Web.Interfaces
{
    public interface IDrzavaService
    {
        List<DrzavaViewModel> GetDrzave(string searchQuery);
    }
}
