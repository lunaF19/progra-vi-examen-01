using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventosFranciscoLuna.Data;

namespace EventosFranciscoLuna.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ReservaController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Reservas
        public async Task<ActionResult> Index()
        {
            var reservas = await _context.SalonesSet
                .Include(r => r.IdSalon)  // Incluimos la relación con Salón
                .ToListAsync();
            return View(reservas);
        }

        // GET: Reservas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = await _context.ReservasSet
                .Include(r => r.Salon)  // Incluimos la relación con Salón
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.IdSalon = new SelectList(_context.SalonesSet, "IdSalon", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdReserva,Nombre,Fecha,HoraInicio,HoraFin,IdSalon")] Models.Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.ReservasSet.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdSalon = new SelectList(_context.SalonesSet, "IdSalon", "Nombre", reserva.IdSalon);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = await _context.ReservasSet.FindAsync(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdSalon = new SelectList(_context.SalonesSet, "IdSalon", "Nombre", reserva.IdSalon);
            return View(reserva);
        }

        // POST: Reservas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdReserva,Nombre,Fecha,HoraInicio,HoraFin,IdSalon")] Models.Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reserva).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdSalon = new SelectList(_context.SalonesSet, "IdSalon", "Nombre", reserva.IdSalon);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = await _context.ReservasSet
                .Include(r => r.Salon)  // Incluimos la relación con Salón
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return HttpNotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.ReservasSet.FindAsync(id);
            _context.ReservasSet.Remove(reserva);
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