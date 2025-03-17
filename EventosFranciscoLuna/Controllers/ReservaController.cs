using System.Collections.Generic;
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
            List<Models.Reserva> listReservas = await _context.ReservasSet.ToListAsync();
            return View(listReservas);
        }

        // GET: Reserva/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            ViewBag.Eventos = new SelectList(await _context.EventosSet.ToListAsync(), "IdEvento", "Nombre");
            if (id == 0)
            {
                Models.Reserva _reserva = new Models.Reserva();
                return View(_reserva);
            }

            var reserva = await _context.ReservasSet.FindAsync(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }


        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdReserva,Nombre,Fecha,HoraInicio,HoraFin,IdReserva")] Models.Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.ReservasSet.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdReserva = new SelectList(_context.SalonesSet, "IdReserva", "Nombre", reserva.IdReserva);
            return View(reserva);
        }

        // POST: Reserva/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdReserva,Nombre,Fecha,HoraInicio,HoraFin,IdReserva")] Models.Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reserva).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdReserva = new SelectList(_context.SalonesSet, "IdReserva", "Nombre", reserva.IdReserva);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = await _context.ReservasSet
                .Include(r => r.IdReserva)  // Incluimos la relación con Salón
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return HttpNotFound();
            }

            return View(reserva);
        }

    }
}