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
    public class ContratoController : Controller
    {
        private EstacionamentoContext db = new EstacionamentoContext();

        // GET: /Contrato/
        public ActionResult Index()
        {
            var contratos = db.Contratos.Include(c => c.Cliente).Include(c => c.Turno);
            return View(contratos.ToList());
        }

        // GET: /Contrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // GET: /Contrato/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Telefone");
            ViewBag.TurnoId = new SelectList(db.Turnos, "Id", "Nome");
            return View();
        }

        // POST: /Contrato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DataInicio,Datafim,Valor,TurnoId,ClienteId")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.Contratos.Add(contrato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Telefone", contrato.ClienteId);
            ViewBag.TurnoId = new SelectList(db.Turnos, "Id", "Nome", contrato.TurnoId);
            return View(contrato);
        }

        // GET: /Contrato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Telefone", contrato.ClienteId);
            ViewBag.TurnoId = new SelectList(db.Turnos, "Id", "Nome", contrato.TurnoId);
            return View(contrato);
        }

        // POST: /Contrato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DataInicio,Datafim,Valor,TurnoId,ClienteId")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contrato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Telefone", contrato.ClienteId);
            ViewBag.TurnoId = new SelectList(db.Turnos, "Id", "Nome", contrato.TurnoId);
            return View(contrato);
        }

        // GET: /Contrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: /Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contrato contrato = db.Contratos.Find(id);
            db.Contratos.Remove(contrato);
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
