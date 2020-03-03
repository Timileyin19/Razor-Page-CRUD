using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookViewRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookViewRazor
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]   // this make the Book Object to be added automatically
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        // NB: the Task is an IActionResult bcoz we will be redirecting to a new page after the action has being performeds
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Book.AddAsync(Book);      // queue the Book up for addition
                await _db.SaveChangesAsync();       // add the details to the DB
                return RedirectToPage("Index");     // When successful, load Index page
            }
            else
            {
                return Page();                  // bring back the original page 
            }

        }
    }
}