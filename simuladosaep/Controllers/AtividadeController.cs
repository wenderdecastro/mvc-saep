using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simuladosaep.Contexts;

namespace simuladosaep.Controllers
{
    public class AtividadeController : Controller
    {

        public readonly DbDevContext _context;

        public AtividadeController(DbDevContext context)
        {
            _context = context;
        }
        public IActionResult Index(int turmaId)
        {
            var atividades = _context.Atividades
                .Include(x => x.Turma)
                .Where(x => x.TurmaId == turmaId)
                .ToList();


            var turma = _context.Turmas.Find(turmaId);
            ViewBag.TurmaId = turmaId;
            ViewBag.NomeTurma = turma.Nome;
            ViewBag.NomeProfessor = HttpContext.Session.GetString("NomeProfessor");

            return View(atividades);
        }
    }
}
