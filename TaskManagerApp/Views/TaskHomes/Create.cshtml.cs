using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Add the MVC usings
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TaskManagerApp.Views.TaskHomes
{
    //public class List
    //TODO correct the model name and inherit PageModel
    public class Create : PageModel
    {
        public void OnGet()
    {
    }

    public void onSubmit() {
            ViewData["SubmitFailMsg"] = "Create Failed! Retry!";
        }
}
}
