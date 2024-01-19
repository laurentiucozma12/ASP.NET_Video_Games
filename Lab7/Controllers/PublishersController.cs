using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Data;
using LibraryModel.Models;
using LibraryModel.Models.LibraryViewModels;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Cozma_Laurentiu_Lab2.Controllers
{
    [Authorize(Policy = "OnlySales")]
    public class PublishersController : Controller
    {
        private readonly LibraryContext _context;

        public PublishersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(int? id, int? bookId)
        {
            var viewModel = new PublisherIndexData();
            viewModel.Publishers = await _context.Publishers
                .Include(p => p.PublishedBooks)
                    .ThenInclude(pb => pb.Book).ThenInclude(b => b.Author)
                .Include(p => p.PublishedBooks)
                    .ThenInclude(pb => pb.Book)
                    .ThenInclude(i => i.Orders)
                    .ThenInclude(i => i.Customer)

                .AsNoTracking()
                .OrderBy(p => p.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["PublisherId"] = id.Value;
                Publisher publisher = viewModel.Publishers.Where(i => i.Id == id.Value).Single();
                viewModel.Books = publisher.PublishedBooks.Select(s => s.Book).ToList();
            }

            if (bookId != null)
            {
                ViewData["BookId"] = bookId.Value;
                viewModel.Orders = viewModel.Books.Where(x => x.Id == bookId).Single().Orders;
            }
            return View(viewModel);
        }


        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PublisherName,Address")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            //var publisher = await _context.Publishers
            //.FindAsync(id);

            var publisher = await _context.Publishers
                    .Include(i => i.PublishedBooks).ThenInclude(i => i.Book)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            PopulatePublishedBookData(publisher);
            return View(publisher);
        }
        private void PopulatePublishedBookData(Publisher publisher)
        {
            var allBooks = _context.Books;
            var publisherBooks = new HashSet<int>(publisher.PublishedBooks.Select(c => c.BookId));
            var viewModel = new List<PublishedBookData>();

            foreach (var book in allBooks)
            {
                viewModel.Add(new PublishedBookData
                {
                    BookId = book.Id,
                    Title = book.Title,
                    IsPublished = publisherBooks.Contains(book.Id)
                });
            }

            ViewData["Books"] = viewModel;
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedBooks)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherToUpdate = await _context.Publishers
                .Include(i => i.PublishedBooks)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (await TryUpdateModelAsync<Publisher>(publisherToUpdate, "", i => i.PublisherName, i => i.Address))
            {
                UpdatePublishedBooks(selectedBooks, publisherToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }

                return RedirectToAction(nameof(Index));
            }

            UpdatePublishedBooks(selectedBooks, publisherToUpdate);
            PopulatePublishedBookData(publisherToUpdate);

            return View(publisherToUpdate);
        }
        private void UpdatePublishedBooks(string[] selectedBooks, Publisher publisherToUpdate)
        {
            if (selectedBooks == null)
            {
                publisherToUpdate.PublishedBooks = new List<PublishedBook>();
                return;
            }
            var selectedBooksHS = new HashSet<string>(selectedBooks);
            var publishedBooks = new HashSet<int>(publisherToUpdate.PublishedBooks.Select(c => c.Book.Id));

            foreach (var book in _context.Books)
            {
                if (selectedBooksHS.Contains(book.Id.ToString()))
                {
                    if (!publishedBooks.Contains(book.Id))
                    {
                        publisherToUpdate.PublishedBooks.Add(new PublishedBook
                        {
                            PublisherId =
                            publisherToUpdate.Id,
                            BookId = book.Id
                        });
                    }
                }
                else
                {
                    if (publishedBooks.Contains(book.Id))
                    {
                        PublishedBook bookToRemove = publisherToUpdate.PublishedBooks.FirstOrDefault(i => i.BookId == book.Id);
                        _context.Remove(bookToRemove);
                    }
                }
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,PublisherName,Address")] Publisher publisher)
        //{
        //    if (id != publisher.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(publisher);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PublisherExists(publisher.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(publisher);
        //}

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publishers == null)
            {
                return Problem("Entity set 'LibraryContext.Publishers'  is null.");
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
