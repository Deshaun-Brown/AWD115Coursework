using System.Collections.Generic;
using Quaterly_Sales_app.Models;

namespace Quaterly_Sales_app.ViewModels
{
    public class SalesListViewModel
    {
        // Holds the list of sales to iterate over in the view
        public IEnumerable<Sales> Sales { get; set; } = new List<Sales>();
        
        // Holds options for the drop-down (previously managed via ViewBag)
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        
        // The currently selected employee to filter by
        public int EmployeeId { get; set; }
    }
}