using Microsoft.AspNetCore.Mvc;
using tarea_ANGEL_SEBASTIAN_MACA_SANCHEZ.Models;
using tarea_ANGEL_SEBASTIAN_MACA_SANCHEZ.repositorio;

namespace tarea_ANGEL_SEBASTIAN_MACA_SANCHEZ.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repository;

        public UsuarioController(IConfiguration configuration)
        {
            _repository = new UsuarioRepository(configuration);
        }

        public IActionResult Index()
        {
            var lista = _repository.ObtenerTodos();
            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _repository.Insertar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Edit(int id)
        {
            var usuario = _repository.ObtenerTodos().FirstOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Actualizar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Delete(int id)
        {
            var usuario = _repository.ObtenerTodos().FirstOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
