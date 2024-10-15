using Microsoft.AspNetCore.Mvc;
using simuladosaep.Contexts;

namespace simuladosaep.Controllers
{
    public class LoginController : Controller
    {

        private readonly DbDevContext _context;
        public LoginController(DbDevContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var professor = _context.Professors.FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (professor != null)
            {
                HttpContext.Session.SetInt32("ProfessorId", professor.ProfessorId);
                HttpContext.Session.SetString("NomeProfessor", professor.Nome);

                return RedirectToAction("Index", "Professors");
            }

            TempData["Mensagem"] = "Email ou senha incorretos, tente novamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Professors");

        }
    }
}
