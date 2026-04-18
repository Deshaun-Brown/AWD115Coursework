using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmaceuticals.ViewModels
{
    public class EmployeeCreateViewModel
    {
        public Models.Employee Employee { get; set; }
        public IEnumerable<SelectListItem> Managers { get; set; }
    }
}
