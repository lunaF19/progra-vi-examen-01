using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using EventosFranciscoLuna.Data;
using EventosFranciscoLuna.Models;

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
            

            // Va a crear un nuevo registro
            if (id == 0)
            {
                ViewBag.ListSalones = new SelectList(_context.SalonesSet, "IdSalon", "Nombre", 0);
                Models.Evento _evento = new Models.Evento();
                return View(_evento);
            }

            // Busca el registro por PK
            var evento = await _context.EventosSet.FindAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListSalones = new SelectList(_context.SalonesSet, "IdSalon", "Nombre", evento.IdSalon);
            return View(evento);
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdEvento, Nombre, Fecha, HoraInicio, HoraFin, IdSalon ")] Models.Evento evento)
        {

            // Verifica si el modelo es válido
            if (!ModelState.IsValid)
            {
                return View(evento);
            }

            // Agrega el nuevo registro
            _context.EventosSet.Add(evento);
            // Salva cambios
            _context.SaveChanges();
            // Redirige a Index
            return RedirectToAction("Index");
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdEvento, Nombre, Fecha, HoraInicio, HoraFin, IdSalon ")] Models.Evento evento)
        {

            // Valida Modelo
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            // Valida la PK
            if (evento.IdEvento.ToString().Trim().IsNullOrWhiteSpace() || evento.IdEvento == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            // Busca datos del registro
            var _evento = await _context.EventosSet.FindAsync(evento.IdEvento);

            if (_evento == null)
            {
                //_context.Entry(evento).State = EntityState.Modified;
                _context.EventosSet.Add(evento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            // Setea los nuevos datos en el registro
            _evento.IdEvento = evento.IdEvento;
            _evento.Nombre = evento.Nombre;
            _evento.Fecha = evento.Fecha;
            _evento.HoraInicio = evento.HoraInicio;
            _evento.HoraFin = evento.HoraFin;
            _evento.IdSalon = evento.IdSalon;

            // Actualiza
            _context.EventosSet.AddOrUpdate(evento);

            // Aplica cambios
            _context.SaveChanges();

            return RedirectToAction("Index");
        } 

        // GET: Evento/Delete?code=5
        public async Task<ActionResult> Delete(int id=0)
        {

            // Valida la PK
            if (id.ToString().Trim().IsNullOrWhiteSpace() || id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            // Busca el registro
            var evento = await _context.EventosSet.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }

            // Remueve el registro
            _context.EventosSet.Remove(evento);

            // Aplica los cambios
            _context.SaveChanges();
            return View();

        }
    }
}