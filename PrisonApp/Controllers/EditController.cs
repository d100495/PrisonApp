using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrisonApplication.Models;
using PrisonApplication.Models.ViewModels;

namespace PrisonApplication.Controllers
{
    [Authorize(Roles = "Worker")]
    public class EditController : Controller
    {

        private PrisonDatabase _db = new PrisonDatabase();

          [HttpGet]
        public ActionResult ReallocatePrisoner(int? id)
        {
            if (id != null)
            {
                Prisoners prisoner = _db.Prisoners.FirstOrDefault(x => x.Prisoner_Id == id.Value);
                Judgements judgement = _db.Judgements.FirstOrDefault(x => x.FK_Judgements_Prisoners_Id == id);

                var model = new AllocationViewModel()
                {
                    Prisoner_Id = id.Value,
                    CategoryOfCrime_Id = judgement.FK_Judgements_CategoriesOfCrimes_Id,
                    Sex = prisoner.PrisonerSurname
                };

                
               
                model.Cells = from c in _db.Cells
                    join branch in _db.Branches
                    on c.FK_Cells_Branch equals branch.Branch_id
                    join branchSex in _db.BranchesSex
                    on branch.Branch_id equals branchSex.FK_BranchesSex_Branches_Id
                    where branchSex.FK_BranchesSex_CategoriesOfCrimes_Id == judgement.FK_Judgements_CategoriesOfCrimes_Id && c.IsEmpty &&
                          branchSex.Sex == prisoner.Sex && c.Cell_Id != ReleasePrisonerClass.Placeholder
                    select c;

                if (model.Cells.Any() == false)
                {
                    TempData["alertMessage"] = "Brak dostępnych cel";
                    return RedirectToAction("GetPrisoners","Prisoners");
                }
                ViewBag.CelList = new SelectList(model.Cells, "Cell_Id", "Cell_Id");
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ReallocatePrisoner(AllocationViewModel model)
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
           

            var cell = _db.Cells.FirstOrDefault(x => x.Cell_Id == model.Cell_Id);
            if (cell == null)
                return HttpNotFound();
            var count = _db.Allocation.Count(x => x.FK_Allocation_Cells_Id == cell.Cell_Id);
            if (cell.IsEmpty && count < cell.Size)
            {
             

                var allocation = _db.Allocation.FirstOrDefault(s => s.FK_Allocation_Prisoners_Id == model.Prisoner_Id);

                if (allocation != null)
                {
                    allocation.FK_Allocation_Cells_Id = cell.Cell_Id;
                    _db.Entry(allocation).State = EntityState.Modified;
                }
                var history = new ChangesHistory()
                {
                    Worker_Id = id,
                    Date = DateTime.Now.Date
                };

                var allocationHistory = new AllocationChangesHistory()
                {
                    Cell_Id = cell.Cell_Id,
                    FK_AllocationChangesHistory_ChangesHistory_Id = history.Record_Id,
                    FK_AllocationChangesHistory_Allocation_Id = allocation.FK_Allocation_Prisoners_Id
                };
                if (count + 1 == cell.Size)
                {
                    cell.IsEmpty = false;
                    _db.Entry(cell).State = EntityState.Modified;
                }
                if (count - 1 < cell.Size)
                {
                    cell.IsEmpty = true;
                    _db.Entry(cell).State = EntityState.Modified;
                }


                _db.ChangesHistory.Add(history);
                _db.AllocationChangesHistory.Add(allocationHistory);
                _db.SaveChanges();
                return RedirectToAction("GetPrisoners", "Prisoners");
            }
            return View(model);
        }

          [HttpGet]
        public ActionResult EditPrisoner(int? id)
        {
            var prisoner = _db.Prisoners.FirstOrDefault(x => x.Prisoner_Id == id);
            return View(prisoner);
        }

        [HttpPost]
        public ActionResult EditPrisoner(Prisoners prisoner)
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var history = new ChangesHistory()
                {
                    Worker_Id = id,
                    Date = DateTime.Now.Date
                };
                _db.Entry(prisoner).State = EntityState.Modified;

                var prisonerChangesHistory = new PrisonerChangesHistory()
                {
                    PrisonerName = prisoner.PrisonerName,
                    PrisonerSurname = prisoner.PrisonerSurname,
                    Pesel = prisoner.Pesel,
                    FK_PrisonerChangesHistory_Prisoner_Id = prisoner.Prisoner_Id,
                    Sex = prisoner.Sex,
                    FK_PrisonerChangesHistory_ChangesHistory_Id = history.Record_Id
                };

                _db.ChangesHistory.Add(history);
                _db.PrisonerChangesHistory.Add(prisonerChangesHistory);
                _db.SaveChanges();
                return RedirectToAction("GetPrisoners", "Prisoners");
            }
            return View(prisoner);
        }

        [HttpPost]
        public ActionResult EditJudgement(EditJudgementViewModel model)
        {
            var list = _db.CategoriesOfCrimes.ToList();
            ViewBag.KatList = new SelectList(list, "CategoryOfCrime_Id", "NameOfCategory");
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                
                var history = new ChangesHistory
                {
                    Worker_Id = id,
                    Date = DateTime.Now.Date
                };
                _db.Entry(model.Judgement).State = EntityState.Modified;

                var judgementChangesHistory = new JudgementsChangesHistory
                {
                    Prisoner_id = model.Prisoner_Id,
                    Time = model.Judgement.TimeOfJudgement,
                    Date = model.Judgement.StartDate,
                    FK_JudgementsChangesHistory_ChangesHistory_Id = history.Record_Id,
                    FK_JudgementsChangesHistory_Judgements = model.Judgement.Judgement_Id,
                    CategoryOfCrimes_Id = model.Judgement.FK_Judgements_CategoriesOfCrimes_Id
                };

                _db.ChangesHistory.Add(history);
                _db.JudgementsChangesHistory.Add(judgementChangesHistory);
                _db.SaveChanges();
                var idTemp = judgementChangesHistory.Prisoner_id;
                return RedirectToAction("ReallocatePrisoner", new {id = idTemp});
            }
            return View(model);
        }
    }
}