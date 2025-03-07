using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventosFranciscoLuna.Data;

namespace EventosFranciscoLuna.Controllers
{
    class SalonController : Controller
    {    
        private readonly ApplicationDbContext _context;

        public SalonController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Salones
        public async Task<ActionResult> Index()
        {
            var salones = await _context.SalonesSet.ToListAsync();
            return View(salones);
        }

        // GET: Salones/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Salones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdSalon,Nombre,Capacidad,Ubicacion")] Models.Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.SalonesSet.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(salon);
        }

        // GET: Salones/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Salones/Edit
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

        // GET: SalonesSet/Delete/5
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

        // POST: SalonesSet/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.SalonesSet.FindAsync(id);
            _context.SalonesSet.Remove(salon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}