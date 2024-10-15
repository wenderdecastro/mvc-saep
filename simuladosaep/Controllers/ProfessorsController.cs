using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simuladosaep.Contexts;
using simuladosaep.Models;

namespace simuladosaep.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly DbDevContext _context;

        public ProfessorsController(DbDevContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var professorId = HttpContext.Session.GetInt32("ProfessorId");

            if (professorId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var professor = _context.Professors.Find(professorId);

            var turmas = _context.Turmas.Where(x => x.ProfessorId == professorId).ToList();

            ViewBag.NomeProfessor = professor.Nome;
            return View(turmas);
        }

        [HttpPost]
        public IActionResult CadastrarTurma(string nomeTurma)
        {
            int? professorId = HttpContext.Session.GetInt32("ProfessorId");

            var turma = new Turma()
            {
                Nome = nomeTurma,
                ProfessorId = professorId,

            };

            _context.Turmas.Add(turma);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult DeletarTurma(int idTurma)
        {
            var turma = _context.Turmas.Find(idTurma);

            if (turma == null)
            {
                return NotFound();
            }

            var hasActivities = turma.Atividades.Any();

            if (hasActivities)
            {
                _context.Atividades.RemoveRange(turma.Atividades);
            }

            _context.Remove(turma);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,Email,Senha")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,Email,Senha")] Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ProfessorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            if (professor != null)
            {
                _context.Professors.Remove(professor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professors.Any(e => e.ProfessorId == id);
        }
    }
}
