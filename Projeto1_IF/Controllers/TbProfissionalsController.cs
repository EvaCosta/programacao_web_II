﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto1_IF.Models;

//Eva Costa de Melo
namespace Projeto1_IF.Controllers
{
    public class TbProfissionalsController : Controller
    {
        private readonly db_IFContext _context;

        public TbProfissionalsController(db_IFContext context)
        {
            _context = context;
        }

        // GET: TbProfissionals
        public async Task<IActionResult> Index()
        {
            var db_IFContext = _context.TbProfissional.Include(t => t.IdCidadeNavigation).Include(t => t.IdContratoNavigation).Include(t => t.IdTipoAcessoNavigation);
            return View(await db_IFContext.ToListAsync());
        }

        // GET: TbProfissionals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .Include(t => t.IdContratoNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

            return View(tbProfissional);
        }

        [Authorize]
        // GET: TbProfissionals/Create
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            ViewData["IdPlano"] = new SelectList(_context.TbPlano, "IdPlano", "Nome");
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome");
            return View();
        }

        // POST: TbProfissionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoProfissional, IdTipoAcesso,IdCidade,IdUser,Nome,Cpf,CrmCrn,Especialidade,Logradouro,Numero,Bairro,Cep,Ddd1,Ddd2,Telefone1,Telefone2,Salario")] TbProfissional tbProfissional, [Bind("IdPlano")] TbContrato IdContratoNavigation)
        {
            try
            {
                ModelState.Remove("IdUser");
                ModelState.Remove("IdContrato");


                if (ModelState.IsValid)
                {
                    IdContratoNavigation.DataInicio = DateTime.UtcNow;
                    IdContratoNavigation.DataFim = IdContratoNavigation.DataInicio.Value.AddMonths(1);
                    _context.Add(IdContratoNavigation);
                    await _context.SaveChangesAsync();

                    var userManager = HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();
                    if(userManager != null)
                    {
                        var email = User.Identity?.Name;
                        if(email != null)
                        {
                            var user = await userManager.FindByEmailAsync(email);
                            if(user != null)
                            {
                                tbProfissional.IdUser = user.Id;
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    

                    }
                    else
                    {
                        return NotFound();
                    }
               
                    tbProfissional.IdContrato = IdContratoNavigation.IdContrato;
                    _context.Add(tbProfissional);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException dex)
            {
                ModelState.AddModelError("", "Não foi possível salvar." + dex.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro geral." + ex.ToString());
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdPlano"] = new SelectList(_context.TbPlano, "IdPlano", "Nome", IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tbProfissional = await _context.TbProfissional.Include(t => t.IdContratoNavigation).FirstOrDefaultAsync(s => s.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbPlano, "IdPlano", "Nome", tbProfissional.IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // POST: TbProfissionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional.Include(s => s.IdContratoNavigation).FirstOrDefaultAsync(s => s.IdProfissional == id);
            
            if(tbProfissional == null) { return NotFound(); }

            if (await TryUpdateModelAsync<TbProfissional>(
                tbProfissional,
                "",
                s => s.IdProfissional, s => s.IdTipoAcesso, s => s.IdCidade, s => s.Nome, s => s.Cpf, s => s.CrmCrn,
                s => s.Especialidade, s => s.Logradouro, s => s.Numero, s => s.Bairro, s => s.Cep,
                s => s.Ddd1, s => s.Ddd2, s => s.Telefone1, s => s.Telefone2, s => s.Salario))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdContrato"] = new SelectList(_context.TbPlano, "IdPlano", "Nome", tbProfissional.IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProfissional = await _context.TbProfissional
                .Include(t => t.IdCidadeNavigation)
                .ThenInclude(d => d.IdEstadoNavigation)
                .Include(t => t.IdContratoNavigation)
                .ThenInclude(s => s.IdPlanoNavigation)
                .Include(t => t.IdTipoAcessoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }


            return View(tbProfissional);
        }

        // POST: TbProfissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProfissional = await _context.TbProfissional.FindAsync(id);
            if (tbProfissional == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.TbProfissional.Remove(tbProfissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
