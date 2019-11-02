using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor.Pages
{
    public class EditModel : PageModel
    {
        private readonly Data.RazorDbContext _dbContext;
        public EditModel(Data.RazorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Models.Book Book { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = _dbContext.Book
                             .AsNoTracking()
                             .FirstOrDefault(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = _dbContext.Book.FirstOrDefault(m => m.Id == Book.Id);

            if (model == null)
            {
                return NotFound();
            }

            model.Name = Book.Name;
            model.PublicationDate = Book.PublicationDate;
            model.UnitPrice = Book.UnitPrice;

            _dbContext.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
