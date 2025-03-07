using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventosFranciscoLuna.Data;
using Microsoft.Ajax.Utilities;

namespace EventosFranciscoLuna.Controllers
{
    public class EventoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EventoController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Evento
        public async Task<ActionResult> Index()
        {
            return View(await _context.EventosSet.ToListAsync());
        }

        // GET: Evento/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            ViewBag.IdSalon = new SelectList(_context.SalonesSet, "IdSalon", "Nombre");
            if (id == 0)
            {
                Models.Evento _evento = new Models.Evento();
                return View(_evento);
            }

            var evento = await _context.EventosSet.FindAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdEvento, Nombre, Fecha, HoraInicio, HoraFin, IdSalon ")] Models.Evento evento)
        {

            if (!ModelState.IsValid)
            {
                return View(evento);
            }

            _context.EventosSet.Add(evento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdEvento, Nombre, Fecha, HoraInicio, HoraFin, IdSalon ")] Models.Evento evento)
        {

            if (evento.IdEvento.ToString().Trim().IsNullOrWhiteSpace())
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var _evento = await _context.EventosSet.FindAsync(evento.IdEvento);

            if (_evento == null)
            {
                //_context.Entry(evento).State = EntityState.Modified;
                _context.EventosSet.Add(evento);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                _evento.IdEvento = evento.IdEvento;
                _evento.Nombre = evento.Nombre;
                _evento.Fecha = evento.Fecha;
                _evento.HoraInicio = evento.HoraInicio;
                _evento.HoraFin = evento.HoraFin;
                _evento.IdSalon = evento.IdSalon;

                _context.EventosSet.AddOrUpdate(evento);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

        }


        // GET: Evento/Delete?code=5
        public async Task<ActionResult> Delete(string code)
        {

            if (code == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var evento = await _context.EventosSet.FindAsync(code);
            if (evento == null)
            {
                return HttpNotFound();
            }

            _context.EventosSet.Remove(evento);
            _context.SaveChanges();
            return View();

        }
    }
}