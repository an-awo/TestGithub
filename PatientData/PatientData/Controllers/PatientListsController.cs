using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientData.Models;

namespace PatientData.Controllers
{
    public class PatientListsController : Controller
    {
        private PatientDBContext db = new PatientDBContext();

        // GET: PatientLists
        /*public ActionResult Index()
        {
            return View(db.Patient.ToList());
        }*/

        public ActionResult Index(string diseaseType, string searchName)
        {
            var diseaseTypeList = new List<string>();  //以現有資料用下拉式選單來查找

            var diseaseTypeQry = from d in db.Patient
                                 orderby d.DiseaseType
                                 select d.DiseaseType;

            diseaseTypeList.AddRange(diseaseTypeQry.Distinct());
            ViewBag.diseaseType = new SelectList(diseaseTypeList);

            var patients = from m in db.Patient    //下面為用空白格以打字的方式查找
                           select m;
            if (!String.IsNullOrEmpty(searchName))
            {
                patients = patients.Where(s => s.Name.Contains(searchName));
            }

            if (!String.IsNullOrEmpty(diseaseType))
            {
                patients = patients.Where(x => x.DiseaseType == diseaseType);
            }
            return View(patients);
        }

        // GET: PatientLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientList patientList = db.Patient.Find(id);
            if (patientList == null)
            {
                return HttpNotFound();
            }
            return View(patientList);
        }

        // GET: PatientLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientLists/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PatientID,phone,Name,DiseaseType,VisitDate,Gender")] PatientList patientList)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(patientList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientList);
        }

        // GET: PatientLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientList patientList = db.Patient.Find(id);
            if (patientList == null)
            {
                return HttpNotFound();
            }
            return View(patientList);
        }

        // POST: PatientLists/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientID,phone,Name,DiseaseType,VisitDate,Gender")] PatientList patientList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientList);
        }

        // GET: PatientLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientList patientList = db.Patient.Find(id);
            if (patientList == null)
            {
                return HttpNotFound();
            }
            return View(patientList);
        }

        // POST: PatientLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientList patientList = db.Patient.Find(id);
            db.Patient.Remove(patientList);
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