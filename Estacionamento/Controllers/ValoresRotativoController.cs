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
    public class ValoresRotativoController : Controller
    {
        private EstacionamentoContext db = new EstacionamentoContext();

        // GET: /ValoresRotativo/
        public ActionResult Index()
        {
            return View(db.ValoresRotativo.ToList());
        }

        // GET: /ValoresRotativo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValorRotativo valorrotativo = db.ValoresRotativo.Find(id);
            if (valorrotativo == null)
            {
                return HttpNotFound();
            }
            return View(valorrotativo);
        }

        // GET: /ValoresRotativo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ValoresRotativo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Descricao,Valor")] ValorRotativo valorrotativo)
        {
            if (ModelState.IsValid)
            {
                db.ValoresRotativo.Add(valorrotativo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valorrotativo);
        }

        // GET: /ValoresRotativo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValorRotativo valorrotativo = db.ValoresRotativo.Find(id);
            if (valorrotativo == null)
            {
                return HttpNotFound();
            }
            return View(valorrotativo);
        }

        // POST: /ValoresRotativo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Descricao,Valor")] ValorRotativo valorrotativo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valorrotativo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valorrotativo);
        }

        // GET: /ValoresRotativo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValorRotativo valorrotativo = db.ValoresRotativo.Find(id);
            if (valorrotativo == null)
            {
                return HttpNotFound();
            }
            return View(valorrotativo);
        }

        // POST: /ValoresRotativo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValorRotativo valorrotativo = db.ValoresRotativo.Find(id);
            db.ValoresRotativo.Remove(valorrotativo);
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
