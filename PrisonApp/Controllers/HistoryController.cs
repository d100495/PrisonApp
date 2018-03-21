using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrisonApplication.Models;
using PrisonApplication.Models.ViewModels.HistoryViewModels;

namespace PrisonApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HistoryController : Controller
    {
        private PrisonDatabase _db = new PrisonDatabase();

        // GET: History
        public ActionResult GetPrisonerHistory()
        {
            var list = from x in _db.ChangesHistory
                join prisonerHistory in _db.PrisonerChangesHistory on x.Record_Id equals prisonerHistory.FK_PrisonerChangesHistory_Prisoner_Id
                select new PrisonerHistoryViewModel
                {
                    Prisoner_Id = prisonerHistory.FK_PrisonerChangesHistory_Prisoner_Id,
                    PrisonerName = prisonerHistory.PrisonerName,
                    PrisonerSurname = prisonerHistory.PrisonerSurname,
                    Pesel = prisonerHistory.Pesel,
                    Sex = prisonerHistory.Sex,
                    Worker = (from o in _db.Users
                        where o.Id == x.Worker_Id
                        select o.UserName).FirstOrDefault(),
                    Date = x.Date
                };

            return View(list);
        }

        public ActionResult GetJudgementsHistory()
        {
            var list = from x in _db.ChangesHistory
                join historyChangeJudgements in _db.JudgementsChangesHistory on x.Record_Id equals historyChangeJudgements.FK_JudgementsChangesHistory_ChangesHistory_Id
                select new JudgementsHistoryViewModel
                {
                    Prisoner_Id = historyChangeJudgements.Prisoner_id,
                    TimeOfJudgement = historyChangeJudgements.Time,
                    Date = x.Date,
                    StartDate = historyChangeJudgements.Date,
                    NameOfCategory = (from o in _db.CategoriesOfCrimes
                        where historyChangeJudgements.CategoryOfCrimes_Id == o.CategoryOfCrime_Id
                        select o.NameOfCategory).FirstOrDefault(),
                    Worker = (from o in _db.Users
                        where o.Id == x.Worker_Id
                        select o.UserName).FirstOrDefault()
                };

            return View(list);
        }

        public ActionResult GetAllocationHistory()
        {
            var list = from x in _db.ChangesHistory
                join y in _db.AllocationChangesHistory on x.Record_Id equals y.FK_AllocationChangesHistory_Allocation_Id
                select new AllocationHistoryViewModel()
                {
                    Prisoner_Id = y.FK_AllocationChangesHistory_Allocation_Id,
                    Cell_Id = y.Cell_Id,
                    Worker = (from o in _db.Users
                        where o.Id == x.Worker_Id
                        select o.UserName).FirstOrDefault(),
                    Date = x.Date
                };
            return View(list);
        }
    }
}