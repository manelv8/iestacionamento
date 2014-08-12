using Estacionamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estacionamento.Controllers
{
    public class HomeController : Controller
    {

        EstacionamentoContext db = new EstacionamentoContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Entrada()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Entrada(string placa)
        {

         //   if (db.Veiculo.Where(v => v.Placa == placa).Any())
         //   {
        //        return RedirectToAction("RegistraEntrada", "Registro", new { placa = placa });
        //    }
        //    else
        //    {
        //        return RedirectToAction("RegistraEntrada", "Registro", new { placa = placa });

            //}
           

            return RedirectToAction("RegistraEntrada", "Registro", new {placa = placa });
        }
    }
}