using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using PrisonApplication.Models;
using PrisonApplication.Models.ViewModels;

namespace PrisonApplication.Controllers
{
    [Authorize(Roles = "Worker")]
    public class PrisonersController : Controller
    {
        private PrisonDatabase _db = new PrisonDatabase();

        // GET: Prisoners
        public ActionResult GetPrisoners(string searchString, string sortOrder)
        {
            ViewBag.IDSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";


            ViewBag.NazwiskoSortParm = sortOrder == "nazwisko" ? "nazwisko_desc" : "nazwisko";
            ViewBag.ImieSortParm = sortOrder == "imie" ? "imie_desc" : "imie";
            ViewBag.PeselSortParm = sortOrder == "pesel" ? "pesel_desc" : "pesel";
            ViewBag.PlecSortParm = sortOrder == "plec" ? "plec_desc" : "plec";

            var list = from w in _db.Prisoners
                join judgement in _db.Judgements on w.Prisoner_Id equals judgement.FK_Judgements_Prisoners_Id
                join allocation in _db.Allocation on w.Prisoner_Id equals allocation.FK_Allocation_Prisoners_Id
                where allocation.FK_Allocation_Cells_Id != ReleasePrisonerClass.Placeholder
                select new ShowPrisonersViewModel
                {
                    Prisoner = w,
                    Judgement = judgement,
                    Allocation = allocation,
                    Date = DbFunctions.AddMonths(judgement.StartDate, judgement.TimeOfJudgement).Value
                };


            if (TempData["alertMessage"] != null)
                ViewBag.alert = TempData["alertMessage"].ToString();

            if (!string.IsNullOrEmpty(searchString))
                list = list.Where(s => s.Prisoner.Pesel.Contains(searchString) ||
                                       s.Prisoner.PrisonerSurname.Contains(searchString) ||
                                       s.Prisoner.PrisonerName.Contains(searchString));


            switch (sortOrder)
            {
                case "id_desc":
                    list = list.OrderByDescending(s => s.Prisoner.Prisoner_Id);
                    break;


                case "surname":
                    list = list.OrderBy(s => s.Prisoner.PrisonerSurname);
                    break;

                case "surname_desc":
                    list = list.OrderByDescending(s => s.Prisoner.PrisonerSurname);
                    break;


                case "name":
                    list = list.OrderBy(s => s.Prisoner.PrisonerName);
                    break;

                case "name_desc":
                    list = list.OrderByDescending(s => s.Prisoner.PrisonerName);
                    break;


                case "sex":
                    list = list.OrderBy(s => s.Prisoner.Sex);
                    break;

                case "sex_desc":
                    list = list.OrderByDescending(s => s.Prisoner.Sex);
                    break;


                case "pesel":
                    list = list.OrderBy(s => s.Prisoner.Pesel);
                    break;

                case "pesel_desc":
                    list = list.OrderByDescending(s => s.Prisoner.Pesel);
                    break;

                default:
                    list = list.OrderBy(s => s.Prisoner.Prisoner_Id);
                    break;
            }

            return View(list);
        }

        [HttpGet]
        public ActionResult AddPrisoner()
        {
            var list = _db.CategoriesOfCrimes.ToList();
            ViewBag.KatList = new SelectList(list, "CategoryOfCrime_Id", "NameOfCategory");
            return View();
        }

        [HttpPost]
        public ActionResult AddPrisoner(PrisonerViewModel model)
        {
            var list = _db.CategoriesOfCrimes.ToList();
            ViewBag.KatList = new SelectList(list, "CategoryOfCrime_Id", "NameOfCategory");


            if (ModelState.IsValid)
            {
                var prisoner = new Prisoners
                {
                    PrisonerName = model.PrisonerName,
                    PrisonerSurname = model.PrisonerSurname,
                    Pesel = model.Pesel,
                    Sex = model.Sex
                };


                var wyrok = new Judgements
                {
                    FK_Judgements_CategoriesOfCrimes_Id = model.CategoryOfCrime_Id,
                    FK_Judgements_Prisoners_Id = prisoner.Prisoner_Id,
                    TimeOfJudgement = model.Time,
                    StartDate = model.StartDate
                };


                _db.Prisoners.Add(prisoner);
                _db.Judgements.Add(wyrok);
                _db.SaveChanges();

               return RedirectToAction("AllocatePrisoner", new {id = prisoner.Prisoner_Id});
                
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AllocatePrisoner(int? id)
        {
            var model = new AllocationViewModel();
            if (id != null)
            {
                model.Prisoner_Id = id.Value;

                Prisoners prisoner = _db.Prisoners.FirstOrDefault(x => x.Prisoner_Id == id);

                model.Sex = prisoner.Sex;

                Judgements judgement = _db.Judgements.FirstOrDefault(x => x.FK_Judgements_Prisoners_Id == id);
                model.CategoryOfCrime_Id = judgement.FK_Judgements_CategoriesOfCrimes_Id;

                model.Cells = from c in _db.Cells
                    join o in _db.Branches
                    on c.FK_Cells_Branch equals o.Branch_id
                    join x in _db.BranchesSex
                    on o.Branch_id equals x.FK_BranchesSex_Branches_Id
                    where x.FK_BranchesSex_CategoriesOfCrimes_Id == judgement.FK_Judgements_CategoriesOfCrimes_Id && c.IsEmpty &&
                          x.Sex == prisoner.Sex && c.Cell_Id != ReleasePrisonerClass.Placeholder
                    select c;

                ViewBag.CelList = new SelectList(model.Cells, "Cell_Id", "Cell_Id");
                return View(model);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult AllocatePrisoner(AllocationViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var cell = _db.Cells.FirstOrDefault(x => x.Cell_Id == model.Cell_Id);
                if (cell == null)
                    return HttpNotFound();
                var count = _db.Allocation.Count(x => x.FK_Allocation_Cells_Id == cell.Cell_Id);
                if (cell.IsEmpty && count < cell.Size)
                {
                    var allocation = new Allocation
                    {
                        FK_Allocation_Cells_Id = cell.Cell_Id,
                        FK_Allocation_Prisoners_Id = model.Prisoner_Id
                    };
                    if (count + 1 == cell.Size)
                    {
                        cell.IsEmpty = false;
                        _db.Entry(cell).State = EntityState.Modified;
                    }
                    _db.Allocation.Add(allocation);
                    _db.SaveChanges();
                    return RedirectToAction("GetPrisoners", "Prisoners");
                }
            }
            return View(model);
        }

        public ActionResult ReleasePrisoner(int? id)
        {
            if (id != null)
            {
                var przydzial = _db.Allocation.FirstOrDefault(x => x.FK_Allocation_Prisoners_Id == id);
                var cela = _db.Cells.FirstOrDefault(x => x.Cell_Id == przydzial.FK_Allocation_Cells_Id);
                var model = new Allocation
                {
                    FK_Allocation_Cells_Id = Models.ReleasePrisonerClass.Placeholder,
                    FK_Allocation_Prisoners_Id = id.Value
                };
                _db.Entry(przydzial).CurrentValues.SetValues(model);

                _db.SaveChanges();
                if (cela.IsEmpty == false)
                    cela.IsEmpty = true;
                return RedirectToAction("GetPrisoners");
            }
            return HttpNotFound();
        }
    }
}