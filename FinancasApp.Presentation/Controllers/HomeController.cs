using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Abrir pagina /Home/Index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
