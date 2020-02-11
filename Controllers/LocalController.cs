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
  public class LocalController : Controller {
    private readonly ApplicationDbContext database;
    private readonly UserManager<IdentityUser> _userManager;

    public LocalController (ApplicationDbContext database, UserManager<IdentityUser> userManager) {
      this.database = database;
      _userManager = userManager;
    }

    public async Task<IActionResult> Index () {
      var user = await _userManager.GetUserAsync (User);
      var local = database.Local.ToList ();
      //Condição para validar Admin
      if (user.Email == "admin@gmail.com") {
        return View (local);
      } else {
        return Unauthorized();
      }
    }

    public async Task<IActionResult> Criar () {
      var user = await _userManager.GetUserAsync (User);
      //Condição para validar Admin
      if (user.Email == "admin@gmail.com") {
        return View ();
      } else {
        return Unauthorized();
      }
    }

    public async Task<IActionResult>  Editar (int id) {
      var user = await _userManager.GetUserAsync (User);
      //Condição para validar Admin
      if (user.Email == "admin@gmail.com"){
        if (ModelState.IsValid) {
          var local = database.Local.First (loc => loc.Id == id);
          LocalDTO localview = new LocalDTO ();
          localview.Id = local.Id;
          localview.Nome = local.Nome;
          localview.Endereco = local.Endereco;
          return View (localview);
        } else {
          return View ("Editar");
        }
      }else{
        return Unauthorized();
      }


    }

    [HttpPost]
    public IActionResult Atualizar (LocalDTO _local) {

      if (ModelState.IsValid) {
        var local = database.Local.First (loc => loc.Id == _local.Id);
        local.Nome = _local.Nome;
        local.Endereco = _local.Endereco;
        database.SaveChanges ();
        return RedirectToAction ("Index", "Local");
      } else {
        return View ("Editar");
      }
    }

    [HttpPost]
    public IActionResult Salvar (LocalDTO _local) {

      if (ModelState.IsValid) {
        Local local = new Local ();
        local.Nome = _local.Nome;
        local.Endereco = _local.Endereco;
        database.Add (local);
        database.SaveChanges ();
        return RedirectToAction ("Index", "Local");
      } else {
        return View ("Criar");
      }

    }

  public async Task<IActionResult> Deletar (int id) {
    
    var user = await _userManager.GetUserAsync (User);
    if (user.Email == "admin@gmail.com") {
      Local local = database.Local.First (registro => registro.Id == id);
      database.Local.Remove (local);
      database.SaveChanges ();
      return RedirectToAction ("Index");}
      else{
        return Unauthorized();
      }

    }
  }
}