using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto1_IF.Models;
using System.Security.Claims;

namespace Projeto1_IF.Controllers
{
    [Authorize]
    public class TbPacientesController : Controller
    {
        private readonly db_IFContext _context;

        public TbPacientesController(db_IFContext context)
        {
            _context = context;
        }

        // GET: TbPacientes
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> Index()
        {
            // Carregar id de usuário logado
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            var profissionalLogado = await _context.TbProfissional.FirstOrDefaultAsync(p => p.IdUser == userId);

            if (profissionalLogado == null)
            {
                return RedirectToAction("Erro", "Home");
            }

            // Buscar os registros na tabela TbMedicoPaciente associados ao profissional logado
            var registrosMedicoPaciente = await _context.TbMedicoPaciente
                .Where(mp => mp.IdProfissional == profissionalLogado.IdProfissional)
                .ToListAsync();

            // Extrair os IDs dos pacientes associados
            var idsPacientesAssociados = registrosMedicoPaciente.Select(mp => mp.IdPaciente).ToList();

            // Buscar os pacientes associados ao profissional logado
            var pacientesPorProfissional = await _context.TbPaciente
                .Where(p => idsPacientesAssociados.Contains(p.IdPaciente))
                .Include(t => t.IdCidadeNavigation)
                .ToListAsync();

            
            return View(pacientesPorProfissional);
        }

        // GET: TbPacientes/Details/5
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return NotFound();
            }

            return View(tbPaciente);
        }

        [Authorize]
        // GET: TbPacientes/Create
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public IActionResult Create()
        {
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome");
            return View();
        }

        // POST: TbPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> Create([Bind("Nome,Rg,Cpf,DataNascimento,NomeResponsavel,Sexo,Etnia,Endereco,Bairro,IdCidade,TelResidencial,TelComercial,TelCelular,Profissao,FlgAtleta,FlgGestante")] TbPaciente tbPaciente, TbMedicoPaciente tbMedicoPaciente)
        {
            try
            {
                ModelState.Remove("IdPaciente");

                if (ModelState.IsValid)
                {
                    // Carrega o ID do usuário logado
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Busca o profissional associado ao usuário logado
                    var isLogado = await _context.TbProfissional.FirstOrDefaultAsync(p => p.IdUser == userId);

                    if (isLogado == null)
                    {
                        // Se o profissional não for encontrado, redirecionar para a página de erro
                        return RedirectToAction("Erro", "Home");
                    }

                    // Adiciona o paciente ao conte
                    _context.Add(tbPaciente);
                    await _context.SaveChangesAsync();

                    // Cria uma nova instância de TbMedicoPaciente com o ID do profissional e do paciente
                    tbMedicoPaciente.IdProfissional = isLogado.IdProfissional;
                    tbMedicoPaciente.IdPaciente = tbPaciente.IdPaciente;

                    // Adiciona o registro de TbMedicoPaciente ao contexto e salva as mudanças
                    _context.Add(tbMedicoPaciente);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                ModelState.AddModelError("", "Não foi possível salvar." + dex.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro geral." + ex.ToString());
            }

            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            return View(tbPaciente);
        }

        // GET: TbPacientes/Edit/5
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var tbPaciente = await _context.TbPaciente.FindAsync(id);
            if (tbPaciente == null)
            {
                return NotFound();
            }
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            return View(tbPaciente);
        }

        // POST: TbPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tbPaciente = await _context.TbPaciente.FirstOrDefaultAsync(s => s.IdPaciente == id);

            if (tbPaciente == null) { return NotFound(); }

            if (await TryUpdateModelAsync<TbPaciente>(
                tbPaciente,
                "",
                s => s.Nome, s => s.Rg, s => s.Cpf, s => s.DataNascimento, s => s.NomeResponsavel,
                s => s.Sexo, s => s.Etnia, s => s.Endereco, s => s.Bairro, s => s.IdCidade,
                s => s.TelResidencial, s => s.TelComercial, s => s.TelCelular, s => s.Profissao, s => s.FlgAtleta, s => s.FlgGestante))
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
            ViewData["IdCidade"] = new SelectList(_context.TbCidade, "IdCidade", "Nome", tbPaciente.IdCidade);
            return View(tbPaciente);
        }

        // GET: TbPacientes/Delete/5
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPaciente = await _context.TbPaciente
                .Include(t => t.IdCidadeNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (tbPaciente == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tbPaciente);
        }

        // POST: TbPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GerenteGeral, Medico, Nutricionista")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPaciente = await _context.TbPaciente.FindAsync(id);
            if (tbPaciente == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Encontra todas as associações do paciente com os profissionais
            var associacoes = await _context.TbMedicoPaciente
                .Where(mp => mp.IdPaciente == id)
                .ToListAsync();

            // Remova essas associações
            _context.TbMedicoPaciente.RemoveRange(associacoes);


            try
            {
                _context.TbPaciente.Remove(tbPaciente);
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
