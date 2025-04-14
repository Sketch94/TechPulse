using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using TechPulse.Models;
using TechPulse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TechPulse.Controllers
{
    public class PublishingController : Controller
    {
        private readonly TechPulseDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PublishingController(TechPulseDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /Publishing
        [AllowAnonymous]  // Allow anyone to view the index
        public IActionResult Index()
        {
            var articles = _context.Articles.OrderByDescending(a => a.CreatedAt).ToList();
            return View(articles);
        }

        // GET: /Publishing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Publishing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article, [FromForm] IFormFile? imageFile)
        {
            Console.WriteLine(">>> POST: Create called");
            
            // Remove imageFile from ModelState if not present
            if (imageFile == null)
                ModelState.Remove("imageFile");

            if (!ModelState.IsValid)
            {
                Console.WriteLine(">>> ModelState is invalid:");
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($">>> Error: {error.ErrorMessage}");
                    }
                }
                return View(article);
            }

            try
            {
                // Only process image if one was uploaded
                if (imageFile != null && imageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("imageFile", "Endast bildformat (jpg, jpeg, png, gif) är tillåtna.");
                        return View(article);
                    }

                    if (imageFile.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("imageFile", "Filen är för stor. Max 2MB tillåts.");
                        return View(article);
                    }

                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    article.FeaturedImage = "/uploads/" + fileName;
                }

                article.AuthorId = "TechPulse Team";
                article.CreatedAt = DateTime.UtcNow;
                article.IsPublished = true;

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                Console.WriteLine(">>> Article saved successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> Error saving article: {ex.Message}");
                ModelState.AddModelError("", "Ett fel uppstod när artikeln skulle sparas.");
                return View(article);
            }
        }

        // GET: /Publishing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return NotFound();

            return View(article);
        }

        // POST: /Publishing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article, IFormFile imageFile)
        {
            if (id != article.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingArticle = await _context.Articles.FindAsync(id);
                if (existingArticle == null)
                    return NotFound();

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Validate file extension and size
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("imageFile", "Endast bildformat (jpg, jpeg, png, gif) är tillåtna.");
                        return View(article);
                    }

                    if (imageFile.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("imageFile", "Filen är för stor. Max 2MB tillåts.");
                        return View(article);
                    }

                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existingArticle.FeaturedImage = "/uploads/" + fileName;
                }

                existingArticle.Title = article.Title;
                existingArticle.Content = article.Content;
                existingArticle.Category = article.Category;
                existingArticle.UpdatedAt = DateTime.UtcNow;

                _context.Update(existingArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: /Publishing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: /Publishing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 