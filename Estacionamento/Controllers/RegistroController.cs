﻿using System;
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

        [HttpPost]
        public ActionResult Patio(string data = null)
        {

            DateTime dataBusca;

            if (data == null)
            {
                dataBusca = DateTime.Today;
            }
            else
            {
                dataBusca = Convert.ToDateTime(data);
            }

            //testear passando uma DATA COMO PARAMETRO
            var registros = db.Registros.Include(r => r.Veiculo)
                .Where(r => r.Entrada.Day == dataBusca.Day)
                .Where(r => r.Entrada.Month == dataBusca.Month)
                .Where(r => r.Entrada.Year == dataBusca.Year);

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
        public ActionResult Create([Bind(Include = "Id,Entrada,Saida,ValorDevido,ValorPago,Placa,VeiculoId")] Registro registro)
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
        public ActionResult Edit([Bind(Include = "Id,Entrada,Saida,ValorDevido,ValorPago,Placa,VeiculoId")] Registro registro)
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



        public ActionResult RegistraEntrada(string placa)
        {
            bool rotativo = true;
            placa = placa.ToUpper();
            Veiculo v = db.Veiculos.Where(x => x.Placa.Equals(placa)).FirstOrDefault();


            using (db)
            {
                if (v == null)
                {   //cria um novo veiculo sem cliente
                    v = new Veiculo();
                    v.Placa = placa;
                    db.Veiculos.Add(v);

                }
                else
                {   //verifica se o veiculo já está no pátio
                    Registro registro = db.Registros
                        .Where(x => x.VeiculoId == v.Id)
                        .Where(d => d.Saida == null).FirstOrDefault();

                    if (registro != null)
                    {
                        TempData["erro"] = "veiculo ja no patio";
                        return RedirectToAction("Entrada", "Movimentacao");
                    }

                    else if (v.ClienteId != null)
                    {

                        //verificar se o ciente cadastrado possui um contrato ativo
                        Contrato contrato = db.Contratos
                            .Where(c => c.ClienteId == v.ClienteId)
                            .Where(d => d.Datafim <= DateTime.Now).FirstOrDefault();

                        if (contrato != null)
                        {
                            rotativo = false;
                        }
                    }


                }


                Registro reg = new Registro();
                reg.Entrada = DateTime.Now;
                reg.Placa = v.Placa;
                reg.Veiculo = v;
                reg.Rotativo = rotativo;
                db.Registros.Add(reg);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult RegistraSaida(string placa)
        {
            Registro reg;
            using (db)
            {
                reg = db.Registros.Where(p => p.Veiculo.Placa.Equals(placa)).Where(x => x.Saida == null).FirstOrDefault();

                if (reg == null)
                {
                    TempData["erro"] = "veiculo NAO está no patio";
                    return RedirectToAction("Saida", "Movimentacao");
                }
                else
                {
                    int turnos = 0;
                    float preco = 3;
                    DateTime meioDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 00);
                    DateTime fimTarde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 00, 00);

                    reg.Saida = DateTime.Now;
                    int turnoEntrada1 = DateTime.Compare((DateTime)reg.Entrada, meioDia);
                    int turnoEntrada2 = DateTime.Compare((DateTime)reg.Entrada, fimTarde);
                    int turnoSaida1 = DateTime.Compare((DateTime)reg.Saida, meioDia);
                    int turnoSaida2 = DateTime.Compare((DateTime)reg.Saida, fimTarde);

                    if (turnoEntrada1 < 0 && turnoSaida1 <= 0) // entrou pela manha e saiu antes do meio dia
                    {
                        turnos = 1;
                    }
                    else if (turnoEntrada1 < 0 && turnoSaida2 <= 0)// entrou pela manha e saiu a tarde
                    {
                        turnos = 2;
                    }
                    else if (turnoEntrada1 < 0 && turnoSaida2 > 0) // entrou pela manha e saiu a noite
                    {
                        turnos = 3;
                    }
                    else if (turnoEntrada1 >= 0 && turnoEntrada2 < 0 && turnoSaida2 <= 0) //entrou a tarde e saiu a tarde
                    {
                        turnos = 1;
                    }
                    else if (turnoEntrada1 >= 0 && turnoEntrada2 < 0 && turnoSaida2 > 0) //entrou a tarde e saiu a NOITE
                    {
                        turnos = 2;
                    }
                    else
                    {
                        turnos = 1;
                    }


                    reg.ValorDevido = turnos * preco;
                    db.Entry(reg).State = EntityState.Modified;
                    db.SaveChanges();
                }

                // TimeSpan ts = (TimeSpan) (reg.Saida - reg.Entrada);

                //  var dayTs = ts.Days;
                //  var horasTs = ts.Hours;
                //  var minTs = ts.Minutes;
                //  var segTs = ts.Seconds;

            }
            parei as modificações aqui
            return rediRedirectToAction("ConfirmaPagamento", reg.Id);
        }

        public ActionResult ConfirmaPagamento(Registro reg)
        {

            return View(reg);
        }
        [HttpPost]
        public ActionResult ConfirmaPagamento([Bind(Include = "Id,Saida,ValorDevido,ValorPago")]Registro reg)
        {
            using (db)
            {
                Registro registro = db.Registros.Find(reg.Id);
                registro.Saida = reg.Saida;
                registro.ValorDevido = reg.ValorDevido;
                registro.ValorPago = reg.ValorPago;
                db.Entry(registro).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("index");
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
