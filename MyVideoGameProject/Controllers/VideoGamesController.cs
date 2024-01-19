using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameModel.Data;
using VideoGameModel.Models;

namespace MyVideoGameProject

{
    public class VideoGamesController : Controller
    {
        private readonly VideoGameContext _context;

        public VideoGamesController(VideoGameContext context)
        {
            _context = context;
        }

        // GET: VideoGames
        public async Task<IActionResult> Index()
        {
            return _context.VideoGames != null ?
                View(await _context.VideoGames.ToListAsync()) :
                Problem("Entity set 'VideoGameContext.VideoGames'  is null.");
        }

        // GET: VideoGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VideoGames == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null)
            {
                return NotFound();
            }

            return View(videoGame);
        }

        // GET: VideoGames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] VideoGame videoGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoGame);
        }

        // GET: VideoGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VideoGames == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }
            return View(videoGame);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Studio videoGame)
        {
            if (id != videoGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioExists(videoGame.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(videoGame);
        }

        // GET: VideoGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VideoGames == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null)
            {
                return NotFound();
            }

            return View(videoGame);
        }

        // POST: VideoGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VideoGames == null)
            {
                return Problem("Entity set 'VideoGameContext.VideoGames' is null.");
            }

            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame != null)
            {
                _context.VideoGames.Remove(videoGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool StudioExists(int id)
        {
            return (_context.VideoGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using VideoGameModel.Data;
//using VideoGameModel.Models;
//using Microsoft.AspNetCore.Authorization;

//namespace MyVideoGameProject
//{
//    [Authorize(Roles = "Employee")]
//    public class VideoGamesController : Controller
//    {
//        private readonly VideoGameContext _context;

//        public VideoGamesController(VideoGameContext context)
//        {
//            _context = context;
//        }

//        // GET: VideoGames
//        [AllowAnonymous]
//        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
//        {
//            ViewData["CurrentSort"] = sortOrder;
//            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
//            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

//            if (searchString != null)
//            {
//                pageNumber = 1;
//            }
//            else
//            {
//                searchString = currentFilter;
//            }

//            ViewData["CurrentFilter"] = searchString;
//            var videoGames = _context.VideoGames
//               .Include(b => b.Studio)
//               .Select(b => b);

//            if (!String.IsNullOrEmpty(searchString))
//            {
//                videoGames = videoGames.Where(s => s.Title.Contains(searchString));
//            }

//            switch (sortOrder)
//            {
//                case "title_desc":
//                    videoGames = videoGames.OrderByDescending(b => b.Title);
//                    break;
//                case "Price":
//                    videoGames = videoGames.OrderBy(b => b.Price);
//                    break;
//                case "price_desc":
//                    videoGames = videoGames.OrderByDescending(b => b.Price);
//                    break;
//                default:
//                    videoGames = videoGames.OrderBy(b => b.Title);
//                    break;
//            }
//            int pageSize = 2;

//            return View(await PaginatedList<VideoGame>.CreateAsync(videoGames.AsNoTracking(), pageNumber ?? 1, pageSize));
//        }

//        // GET: VideoGames/Details/5
//        [AllowAnonymous]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var videoGame = await _context.VideoGames
//                .Include(b => b.Studio)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (videoGame == null)
//            {
//                return NotFound();
//            }

//            return View(videoGame);
//        }

//        // GET: VideoGames/Create
//        public IActionResult Create()
//        {
//            var studioList = _context.Studios.Select(x => new SelectListItem
//            {
//                Value = x.Id.ToString(),
//                Text = x.Name
//            }).ToList();

//            ViewBag.StudioId = new SelectList(studioList, "Value", "Text");

//            return View();
//        }

//        // POST: VideoGames/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Title,StudioId,Price")] VideoGame videoGame)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    _context.Add(videoGame);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            catch (DbUpdateException /* ex*/)
//            {
//                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
//            }

//            // Retrieve the videoGame list as before
//            var studioList = _context.Studios.Select(x => new SelectListItem
//            {
//                Value = x.Id.ToString(),
//                Text = x.Name
//            }).ToList();

//            ViewBag.StudioId = new SelectList(studioList, "Value", "Text");

//            return View(videoGame);
//        }

//        // GET: VideoGames/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var videoGame = await _context.VideoGames.FindAsync(id);
//            if (videoGame == null)
//            {
//                return NotFound();
//            }

//            // Retrieve the videoGame list as before
//            var studioList = _context.Studios.Select(x => new SelectListItem
//            {
//                Value = x.Id.ToString(),
//                Text = x.Name
//            }).ToList();

//            ViewBag.StudioId = new SelectList(studioList, "Value", "Text", videoGame.StudioId);

//            return View(videoGame);
//        }

//        // POST: VideoGames/Edit/5
//        [HttpPost, ActionName("Edit")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,StudioId,Price")] VideoGame videoGame)
//        {
//            if (id != videoGame.Id)
//            {
//                return NotFound();
//            }

//            var videoGameToUpdate = await _context.VideoGames.FirstOrDefaultAsync(s => s.Id == id);

//            if (await TryUpdateModelAsync<VideoGame>(videoGameToUpdate, "", s => s.Studio, s => s.Title, s => s.Price))
//            {
//                try
//                {
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (DbUpdateException /* ex */)
//                {
//                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists");
//                }
//            }

//            // Retrieve the videoGame list as before
//            var studioList = _context.Studios.Select(x => new SelectListItem
//            {
//                Value = x.Id.ToString(),
//                Text = x.Name
//            }).ToList();

//            ViewBag.StudioId = new SelectList(studioList, "Value", "Text", videoGame.StudioId);

//            return View(videoGame);
//        }

//        // GET: VideoGames/Delete/5
//        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var videoGame = await _context.VideoGames
//                .AsNoTracking()
//                .Include(b => b.Studio)
//                .FirstOrDefaultAsync(m => m.Id == id);

//            if (videoGame == null)
//            {
//                return NotFound();
//            }
//            if (saveChangesError.GetValueOrDefault())
//            {
//                ViewData["ErrorMessage"] = "Delete failed. Try again";
//            }

//            return View(videoGame);
//        }

//        // POST: VideoGames/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var videoGame = await _context.VideoGames.FindAsync(id);

//            if (videoGame == null)
//            {
//                return RedirectToAction(nameof(Index));
//            }

//            try
//            {
//                _context.VideoGames.Remove(videoGame);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch (DbUpdateException /* ex */)
//            {
//                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
//            }
//        }

//        private bool BookExists(int id)
//        {
//            return (_context.VideoGames?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
