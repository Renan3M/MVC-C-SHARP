using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using System.Web.Script.Serialization;



namespace MvcMovie.Controllers
{
    public class EstrategiasController : Controller
    {
        private EstrategiaDBContext db = new EstrategiaDBContext();

        // GET: Estrategias
        public ActionResult Index()
        {
            
            return View(db.Estrategias.ToList());
        }

        // GET: Estrategias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estrategia estrategia = db.Estrategias.Find(id);
            if (estrategia == null)
            {
                return HttpNotFound();
            }
            return View(estrategia);
        }

        // GET: Estrategias/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Estrategias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,side,price,quantity,symbol")] Estrategia estrategia)
        {
            if (ModelState.IsValid)
            {
                 
                RpcClient connectRMQ = new RpcClient();

                var json = new JavaScriptSerializer().Serialize(estrategia);
                var jso1 = "{'id': 2,'side':'buy','price':1,'quantity':3,'symbol':'USD'}";
                var response = connectRMQ.Call(json);

                db.Estrategias.Add(estrategia);
                db.SaveChanges();
                var res = response;
                TempData.Add("MyTempData", res);

                
                return RedirectToAction("Response1", "Estrategias");
            }

            return View(estrategia);
        }

        public ActionResult Response1()
        {
            string data = TempData["MyTempData"].ToString();
            ViewBag.Dat = data;
            return View("Index", db.Estrategias.ToList());
            
        }

        // GET: Estrategias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estrategia estrategia = db.Estrategias.Find(id);
            if (estrategia == null)
            {
                return HttpNotFound();
            }
            return View(estrategia);
        }

        // POST: Estrategias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,side,price,quantity,symbol")] Estrategia estrategia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estrategia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estrategia);
        }

        // GET: Estrategias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estrategia estrategia = db.Estrategias.Find(id);
            if (estrategia == null)
            {
                return HttpNotFound();
            }
            return View(estrategia);
        }

        // POST: Estrategias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estrategia estrategia = db.Estrategias.Find(id);
            db.Estrategias.Remove(estrategia);
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

      /*  public String sendData() {
        }*/



    }
}
