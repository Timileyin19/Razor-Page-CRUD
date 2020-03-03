using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookViewRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookViewRazor
    // NB: this "Upset" Code Behind is use to combine the functionality of both "Update" and "Create"
{
    public class UpsetModel : PageModel
    {
        private ApplicationDbContext _db;   // it is not "readonly" because we have to update things

        public UpsetModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)      // means it is a "create" function
            {
                // create
                return Page();      // go back to the page
            }

            // if the id is not null (meaning we want to Edit), then retrieve the book from the database
            // Update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    // Create 
                    _db.Book.Add(Book);
                }
                else
                {
                    // update 
                    // NB: this "Update" should be used if you want to update every property of the Book 
                    // otherwise you should retrieve the data from the DB and then update the individual property you want to update
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
          

        }
    }
}