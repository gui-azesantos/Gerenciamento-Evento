using System;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoEvento.Data;
using GerenciamentoEvento.DTO;
using GerenciamentoEvento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEvento.Controllers {
    [Authorize]
    public class EventosController : Controller {
        private readonly ApplicationDbContext database;

        private readonly UserManager<IdentityUser> _userManager;

        public EventosController (ApplicationDbContext database, UserManager<IdentityUser> userManager) {
            this.database = database;
            _userManager = userManager;
        }

        public IActionResult Home () {
            var eventos = database.Evento.Include (e => e.CasaDeShow).ToList ();
            return View (eventos);
        }

        public async Task<IActionResult> Index () {
            var user = await _userManager.GetUserAsync (User);
            var eventos = database.Evento.Include (e => e.CasaDeShow).ToList ();
            //Condição para validar Admin
            if (user.Email == "admin@gmail.com") {
                return View (eventos);
            } else {
                return Unauthorized();
            }
        }

        public async Task<IActionResult> Criar () {
            ViewBag.CasaDeShow = database.Local.ToList ();
            var user = await _userManager.GetUserAsync (User);
            //Condição para validar Admin
            if (user.Email == "admin@gmail.com") {
                return View ();
            } else {
                return Unauthorized();
            }
        }

        public async Task<IActionResult> Editar (int id) {
            var user = await _userManager.GetUserAsync (User);
            //Condição para validar Admin
            if (user.Email == "admin@gmail.com") {
                if (ModelState.IsValid) {
                    var evento = database.Evento.Include (e => e.CasaDeShow).First (e => e.Id == id);
                    EventoDTO eventoview = new EventoDTO ();
                    eventoview.Id = evento.Id;
                    eventoview.Nome = evento.Nome;
                    eventoview.Capacidade = evento.Capacidade;
                    eventoview.Data = evento.Data;
                    eventoview.Preco = evento.Preco;
                    eventoview.CasaDeShowID = evento.CasaDeShow.Id;
                    eventoview.Estilo = evento.Estilo;
                    eventoview.Imagem = evento.Imagem;
                    ViewBag.CasaDeShow = database.Local.ToList ();
                    return View (eventoview);
                } else {
                    ViewBag.CasaDeShow = database.Local.ToList ();
                    return View ("Editar");
                }
            } else {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Atualizar (EventoDTO _evento) {

            if (ModelState.IsValid) {
                var evento = database.Evento.First (e => e.Id == _evento.Id);
                evento.Nome = _evento.Nome;
                evento.Capacidade = _evento.Capacidade;
                evento.Data = _evento.Data;
                evento.Preco = _evento.Preco;
                evento.CasaDeShow = database.Local.First (casadeshow => casadeshow.Id == _evento.CasaDeShowID);
                evento.Estilo = _evento.Estilo;
                evento.Imagem = _evento.Imagem;
                database.SaveChanges ();
                return RedirectToAction ("Index", "Eventos");
            } else {
                ViewBag.CasaDeShow = database.Local.ToList ();
                return View ("Editar");
            }

        }

        [HttpPost]
        public IActionResult Salvar (EventoDTO _evento) {

            if (ModelState.IsValid) {

                Evento evento = new Evento ();
                evento.Nome = _evento.Nome;
                evento.Capacidade = _evento.Capacidade;
                evento.Data = _evento.Data;
                evento.Preco = _evento.Preco;
                evento.CasaDeShow = database.Local.First (casadeshow => casadeshow.Id == _evento.CasaDeShowID);
                evento.Estilo = _evento.Estilo;
                evento.Imagem = _evento.Imagem;

                database.Evento.Add (evento);
                database.SaveChanges ();
                return RedirectToAction ("Index", "Eventos");
            } else {
                ViewBag.CasaDeShow = database.Local.ToList ();
                return View ("Criar");
            }

        }

        public async Task<IActionResult> Deletar (int id) {
            var user = await _userManager.GetUserAsync (User);
            //Condição para validar Admin
            if (user.Email == "admin@gmail.com") {
                Evento evento = database.Evento.First (registro => registro.Id == id);
                database.Evento.Remove (evento);
                database.SaveChanges ();
                return RedirectToAction ("Index");
            } else {
                return Unauthorized();
            }
        }

    }
}