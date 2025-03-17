using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using EventosFranciscoLuna.Data;

namespace EventosFranciscoLuna.Controllers
{
    public class BasicController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BasicController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Basic
        public async Task<ActionResult> Index()
        {
            return View(await _context.SalonesSet.ToListAsync());
        }

        // GET: Salon/Details/5
        public async Task<ActionResult> Details(int id = 0)
        {
            if (id == 0)
            {
                Models.Salon _salon = new Models.Salon();
                return View(_salon);
            }

            var salon = await _context.SalonesSet.FindAsync(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // GET: Salon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nombre,Capacidad,Ubicacion")] Models.Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return View(salon);
            }

            _context.SalonesSet.Add(salon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Salon/Edit/5
        public async Task<ActionResult> Edit(int id=0)
        {
            if (id == 0)
            {
                return View(new Models.Salon());
            }

            var salon = await _context.SalonesSet.FindAsync(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: Salon/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdSalon,Nombre,Capacidad,Ubicacion")] Models.Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(salon).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(salon);
        }

        // GET: Salon/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salon = await _context.SalonesSet.FindAsync(id);
            if (salon == null)
            {
                return HttpNotFound();
            }

            return View(salon);
        }

        // POST: Salon/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.SalonesSet.FindAsync(id);
            _context.SalonesSet.Remove(salon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}