using System;
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
    public enum Plano
    {
        MedicoTotal = 1,
        MedicoParcial = 5,
        NutricionalTotal = 6,
        NutricionalParcial = 7

    }

    [Authorize]
    public class TbProfissionalsController : Controller
    {
        private readonly db_IFContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TbProfissionalsController(db_IFContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      
        // GET: TbProfissionals

        //Medico e Nutricionista
        //Medico e Nutricionista
  /*      [Authorize(Roles ="Medico")]
        [Authorize(Roles ="Nutricionista")]*/

        [Authorize(Roles = "Medico,Nutricionista, GerenteMedico, GerenteNutricionista, GerenteGeral")]
        public async Task<IActionResult> Index()
        {
            // Metodo 1
            /*  var db_IFContext = _context.TbProfissional
                  .Include(t => t.IdCidadeNavigation)
                  .Include(t => t.IdContratoNavigation)
                      .ThenInclude(s => s.IdPlanoNavigation)
                  .Include(t => t.IdTipoAcessoNavigation)
                  .Where(t => t.IdContratoNavigation.IdPlano == 1)
              ;*/

            //Metodo 2
            /*  var db_IFContext = (from pro in _context.TbProfissional
                                  where (Plano)pro.IdContratoNavigation.IdPlano == Plano.MedicoTotal
                                  select pro)
                                     .Include(pro => pro.IdContratoNavigation)
                                         .ThenInclude(contrato => contrato.IdPlanoNavigation)
               ;*/

            // Metodo 3
            /* var db_IFContext = from pro in _context.TbProfissional
                                join contrato in _context.TbContrato on pro.IdContrato equals contrato.IdContrato
                                join plano in _context.TbPlano on contrato.IdPlano equals plano.IdPlano
                                where plano.IdPlano == 1
                                select pro       
            ;*/
            if (User.IsInRole("GerenteGeral"))
            {
                var db_IFContext = (from pro in _context.TbProfissional
                                    select new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });



                return View(db_IFContext);
            } 
            else if (User.IsInRole("GerenteMedico"))
            {
                var db_IFContext = (from pro in _context.TbProfissional
                                    where (Plano)pro.IdContratoNavigation.IdPlano == Plano.MedicoTotal || (Plano)pro.IdContratoNavigation.IdPlano == Plano.MedicoParcial
                                    select new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });



                return View(db_IFContext);
            }
            else if (User.IsInRole("GerenteNutricionista"))
            {
                var db_IFContext = (from pro in _context.TbProfissional
                                    where (Plano)pro.IdContratoNavigation.IdPlano == Plano.NutricionalTotal || (Plano)pro.IdContratoNavigation.IdPlano == Plano.NutricionalParcial
                                    select new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });



                return View(db_IFContext);
            }
            else if (User.IsInRole("Medico")|| User.IsInRole("Nutricionista"))
            {
                var userId = _userManager.GetUserId(User);

                var db_IFContext = (from pro in _context.TbProfissional
                                    //where (Plano)pro.IdContratoNavigation.IdPlano == Plano.MedicoTotal
                                    where pro.IdUser == userId
                                    select new ProfissionalResumido
                                    {
                                        Nome = pro.Nome,
                                        NomeCidade = pro.IdCidadeNavigation.Nome,
                                        NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                        IdProfissional = pro.IdProfissional,
                                        Cpf = pro.Cpf,
                                        CrmCrn = pro.CrmCrn,
                                        Especialidade = pro.Especialidade,
                                        Logradouro = pro.Logradouro,
                                        Numero = pro.Numero,
                                        Bairro = pro.Bairro,
                                        Cep = pro.Cep,
                                        Ddd1 = pro.Ddd1,
                                        Ddd2 = pro.Ddd2,
                                        Telefone1 = pro.Telefone1,
                                        Telefone2 = pro.Telefone2,
                                        Salario = pro.Salario,
                                    });



                return View(db_IFContext);

            }
            /*else if (User.IsInRole("Nutricionista"))
            {*/
               /* var db_IFContext2 = (from pro in _context.TbProfissional
                                        where (Plano)pro.IdContratoNavigation.IdPlano == Plano.Nutricional
                                        select new ProfissionalResumido
                                        {
                                            Nome = pro.Nome,
                                            NomeCidade = pro.IdCidadeNavigation.Nome,
                                            NomePlano = pro.IdContratoNavigation.IdPlanoNavigation.Nome,
                                            IdProfissional = pro.IdProfissional,
                                            Cpf = pro.Cpf,
                                            CrmCrn = pro.CrmCrn,
                                            Especialidade = pro.Especialidade,
                                            Logradouro = pro.Logradouro,
                                            Numero = pro.Numero,
                                            Bairro = pro.Bairro,
                                            Cep = pro.Cep,
                                            Ddd1 = pro.Ddd1,
                                            Ddd2 = pro.Ddd2,
                                            Telefone1 = pro.Telefone1,
                                            Telefone2 = pro.Telefone2,
                                            Salario = pro.Salario,
                                        });*/



                /**//*return View(db_IFContext2);*/
            //}


               return View();

            }

        // GET: TbProfissionals/Details/5
        [Authorize(Roles = "Medico, Nutricionista, GerenteGeral, GerenteMedico, GerenteNutricionista")]
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

        [Authorize (Roles ="GerenteGeral")]
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
        [Authorize(Roles = "GerenteGeral")]
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

            var planos = new List<Plano>();

            if (User.IsInRole("Medico"))
            {
                planos.Add(Plano.MedicoTotal);
                planos.Add(Plano.MedicoParcial);
            }
            else if (User.IsInRole("Nutricionista"))
            {
                planos.Add(Plano.NutricionalTotal);
                planos.Add(Plano.NutricionalParcial);
            }

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbProfissional.IdCidade);
            ViewData["IdPlano"] = new SelectList(_context.TbPlano.Where(p => planos.Contains((Plano)p.IdPlano)), "IdPlano", "Nome", tbProfissional.IdContratoNavigation.IdPlano);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TbTipoAcesso, "IdTipoAcesso", "Nome", tbProfissional.IdTipoAcesso);
            return View(tbProfissional);
        }

        // GET: TbProfissionals/Edit/5
        [Authorize(Roles = "Medico, Nutricionista, GerenteGeral, GerenteMedico, GerenteNutricionista")]
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
        [Authorize(Roles = "Medico, Nutricionista, GerenteGeral, GerenteMedico, GerenteNutricionista")]
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
        [Authorize(Roles = "GerenteGeral, GerenteMedico, GerenteNutricionista")]
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
                .Include(t => t.TbMedicoPaciente)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdProfissional == id);
            if (tbProfissional == null)
            {
                return NotFound();
            }

          
            else if (saveChangesError.GetValueOrDefault())
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
        [Authorize(Roles = "GerenteGeral, GerenteMedico, GerenteNutricionista")]
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
