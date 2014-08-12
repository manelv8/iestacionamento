using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estacionamento.Models;

namespace Estacionamento.Controllers
{
    public class RegistroController : Controller
    {
        private EstacionamentoContext db = new EstacionamentoContext();

        // GET: /Registro/
        public ActionResult Index()
        {
            var registros = db.Registros.Include(r => r.Veiculo);
            return View(registros.ToList());
        }

        // GET: /Registro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // GET: /Registro/Create

        public ActionResult RegistraEntrada(string placa)
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.VeiculoId = new SelectList(db.Veiculos, "Id", "Placa");
            return View();
        }

        // POST: /Registro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Entrada,Saida,ValorDevido,ValorPago,VeiculoId")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                db.Registros.Add(registro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VeiculoId = new SelectList(db.Veiculos, "Id", "Placa", registro.VeiculoId);
            return View(registro);
        }

        // GET: /Registro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            ViewBag.VeiculoId = new SelectList(db.Veiculos, "Id", "Placa", registro.VeiculoId);
            return View(registro);
        }

        // POST: /Registro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Entrada,Saida,ValorDevido,ValorPago,VeiculoId")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VeiculoId = new SelectList(db.Veiculos, "Id", "Placa", registro.VeiculoId);
            return View(registro);
        }

        // GET: /Registro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // POST: /Registro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registro registro = db.Registros.Find(id);
            db.Registros.Remove(registro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
