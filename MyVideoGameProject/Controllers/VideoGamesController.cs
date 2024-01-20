using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameModel.Data;
using VideoGameModel.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyVideoGameProject.Controllers
{
    [Authorize(Roles = "Employee")]
    public class VideoGamesController : Controller
    {
        private readonly VideoGameContext _context;

        public VideoGamesController(VideoGameContext context)
        {
            _context = context;
        }

        // GET: VideoGames
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<VideoGame> videoGames = _context.VideoGames
                .Include(b => b.Studio)
                .Include(c => c.Genre)
                .Include(d => d.Platform)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                videoGames = videoGames.Where(s => s.Title.Contains(searchString));
            }

            videoGames = sortOrder switch
            {
                "title_desc" => videoGames.OrderByDescending(b => b.Title),
                "Price" => videoGames.OrderBy(b => b.Price),
                "price_desc" => videoGames.OrderByDescending(b => b.Price),
                _ => videoGames.OrderBy(b => b.Title),
            };

            int pageSize = 2;

            return View(await PaginatedList<VideoGame>.CreateAsync(videoGames, pageNumber ?? 1, pageSize));
        }


        // GET: VideoGames/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .Include(b => b.Studio)
                .Include(c => c.Genre)
                .Include(d => d.Platform)
                .AsNoTracking()
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
            var studioList = _context.Studios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.StudioId = new SelectList(studioList, "Value", "Text");

            var genreList = _context.Genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.GenreId = new SelectList(genreList, "Value", "Text");

            var platformList = _context.Platforms.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.PlatformId = new SelectList(platformList, "Value", "Text");

            return View();
        }

        // POST: VideoGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,StudioId,GenreId,PlatformId,Price")] VideoGame videoGame)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(videoGame);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // Retrieve the studio list as before
                var studioList = _context.Studios.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                ViewBag.StudioId = new SelectList(studioList, "Value", "Text");
            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(videoGame);
        }

        // GET: VideoGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }

            // Retrieve the studio list as before
            var studioList = _context.Studios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.StudioId = new SelectList(studioList, "Value", "Text", videoGame.StudioId);

            // Retrieve the genre list as before
            var genreList = _context.Genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.GenreId = new SelectList(genreList, "Value", "Text", videoGame.GenreId);

            // Retrieve the platform list as before
            var platformList = _context.Platforms.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.PlatformId = new SelectList(platformList, "Value", "Text", videoGame.PlatformId);

            return View(videoGame);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,StudioId,GenreId,PlatformId,Price")] VideoGame videoGame)
        {
            if (id != videoGame.Id)
            {
                return NotFound();
            }

            var videoGameToUpdate = await _context.VideoGames.FirstOrDefaultAsync(s => s.Id == id);

            if (videoGameToUpdate == null)
            {
                return NotFound();
            }

            // Use TryUpdateModelAsync to update properties from the posted form data
            if (await TryUpdateModelAsync(videoGameToUpdate, "", s => s.StudioId, s => s.Title, s => s.GenreId, s => s.PlatformId, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log the error, handle it, or display an error message to the user
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }

            // If TryUpdateModelAsync fails, redisplay the form with validation errors
            // Retrieve the lists as before
            var studioList = _context.Studios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.StudioId = new SelectList(studioList, "Value", "Text", videoGameToUpdate.StudioId);

            var genreList = _context.Genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.GenreId = new SelectList(genreList, "Value", "Text", videoGameToUpdate.GenreId);

            var platformList = _context.Platforms.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.PlatformId = new SelectList(platformList, "Value", "Text", videoGameToUpdate.PlatformId);

            return View(videoGameToUpdate);
        }


        // GET: VideoGames/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .Include(b => b.Studio)
                .Include(c => c.Genre)
                .Include(d => d.Platform)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (videoGame == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(videoGame);
        }

        // POST: VideoGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);

            if (videoGame == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.VideoGames.Remove(videoGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool VideoGameExists(int id)
        {
            return (_context.VideoGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}