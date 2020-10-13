using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Web.ViewModels;

namespace Zadatak.Web.Interfaces
{
    public interface INaseljaService
    {
        List<NaseljeViewModel> GetFilteredNaselja(PaginationFilterViewModel filters);
        int CountNaselja();
        StatusCodeViewModel NewNaselje(NaseljeViewModel model);
        StatusCodeViewModel UpdateNaselje(NaseljeViewModel model);
        StatusCodeViewModel DeleteNaselje(int id);
    }
}
