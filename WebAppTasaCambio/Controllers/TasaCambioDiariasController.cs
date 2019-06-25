using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTasaCambio.Context;
using WebAppTasaCambio.Models;

namespace WebAppTasaCambio.Controllers
{
    public class TasaCambioDiariasController : Controller
    {
        private SqlServerContext db = new SqlServerContext();

        // GET: TasaCambioDiarias
        public ActionResult Index()
        {
            List<TasaCambioDiaria> lista = new List<TasaCambioDiaria>();
            lista = db.TasaCambioDiaria.OrderByDescending(x=>x.Id).Take(7).ToList();
            return View(lista);
        }

        // GET: TasaCambioDiarias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDiaria tasaCambioDiaria = db.TasaCambioDiaria.Find(id);
            if (tasaCambioDiaria == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambioDiaria);
        }

        // GET: TasaCambioDiarias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TasaCambioDiarias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Compra,Venta,Proceso,Actualizado")] TasaCambioDiaria tasaCambioDiaria)
        {
            if (ModelState.IsValid)
            {
                db.TasaCambioDiaria.Add(tasaCambioDiaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasaCambioDiaria);
        }

        // GET: TasaCambioDiarias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDiaria tasaCambioDiaria = db.TasaCambioDiaria.Find(id);
            if (tasaCambioDiaria == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambioDiaria);
        }

        // POST: TasaCambioDiarias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Compra,Venta,Proceso,Actualizado")] TasaCambioDiaria tasaCambioDiaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasaCambioDiaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasaCambioDiaria);
        }

        // GET: TasaCambioDiarias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaCambioDiaria tasaCambioDiaria = db.TasaCambioDiaria.Find(id);
            if (tasaCambioDiaria == null)
            {
                return HttpNotFound();
            }
            return View(tasaCambioDiaria);
        }

        // POST: TasaCambioDiarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TasaCambioDiaria tasaCambioDiaria = db.TasaCambioDiaria.Find(id);
            db.TasaCambioDiaria.Remove(tasaCambioDiaria);
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
