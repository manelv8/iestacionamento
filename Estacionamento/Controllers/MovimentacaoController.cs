using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estacionamento.Controllers
{
    public class MovimentacaoController : Controller
    {
        //
        // GET: /Movimentacao/
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

            //    }


            return RedirectToAction("RegistraEntrada", "Registro", new { placa = placa });
        }
	}
}