using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace trip_chill_view.Pages
{
    public class ordersModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";
        public void OnGet()
        {
           
        }
    }
}
