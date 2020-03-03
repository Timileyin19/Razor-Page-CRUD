using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookViewRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookViewRazor
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // then we want to return a list of IEnumerable of Book
        public IEnumerable<Book> Books { get; set; }

        // NB: async will let us run multiple task at a Time
        // The Method below is call the "Get" handler method
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");         // The Page will be reloaded
        }
    }
}