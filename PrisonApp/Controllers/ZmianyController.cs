using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrisonApp.Models;
using PrisonApp.Models.ViewModels;

namespace PrisonApp.Controllers
{
    [Authorize(Roles = "Pracownik, Administrator")] 
    public class ZmianyController : Controller
    {
        private readonly Model _db = new Model();

        // GET: Zmiany

        public ActionResult WyswietlWiezniow(string searchString, string sortOrder)
        {
            ViewBag.IDSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";


            ViewBag.NazwiskoSortParm = sortOrder == "nazwisko" ? "nazwisko_desc" : "nazwisko";
            ViewBag.ImieSortParm = sortOrder == "imie" ? "imie_desc" : "imie";
            ViewBag.PeselSortParm = sortOrder == "pesel" ? "pesel_desc" : "pesel";
            ViewBag.PlecSortParm = sortOrder == "plec" ? "plec_desc" : "plec";

            var list = from w in _db.Wiezniowie
                join wyr in _db.Wyroki on w.idWieznia equals wyr.FK_idWieznia
                join przy in _db.Przydzialy on w.idWieznia equals przy.FK_idWieznia
                where przy.FK_idCeli != ZwolnijWieznia.Placeholder
                select new WyswietlWiezniowViewModel
                {
                    Wiezien = w,
                    Wyrok = wyr,
                    Przydzial = przy,
                    Date = EntityFunctions.AddMonths(wyr.DataRozpoczecia, wyr.Czas).Value
                };


            if (TempData["alertMessage"] != null)
                ViewBag.alert = TempData["alertMessage"].ToString();

            if (!string.IsNullOrEmpty(searchString))
                list = list.Where(s => s.Wiezien.Pesel.Contains(searchString) ||
                                       s.Wiezien.NazwiskoWieznia.Contains(searchString) ||
                                       s.Wiezien.ImieWieznia.Contains(searchString));


            switch (sortOrder)
            {
                case "id_desc":
                    list = list.OrderByDescending(s => s.Wiezien.idWieznia);
                    break;


                case "nazwisko":
                    list = list.OrderBy(s => s.Wiezien.NazwiskoWieznia);
                    break;

                case "nazwisko_desc":
                    list = list.OrderByDescending(s => s.Wiezien.NazwiskoWieznia);
                    break;


                case "imie":
                    list = list.OrderBy(s => s.Wiezien.ImieWieznia);
                    break;

                case "imie_desc":
                    list = list.OrderByDescending(s => s.Wiezien.ImieWieznia);
                    break;


                case "plec":
                    list = list.OrderBy(s => s.Wiezien.Plec);
                    break;

                case "plec_desc":
                    list = list.OrderByDescending(s => s.Wiezien.Plec);
                    break;


                case "pesel":
                    list = list.OrderBy(s => s.Wiezien.Pesel);
                    break;

                case "pesel_desc":
                    list = list.OrderByDescending(s => s.Wiezien.Pesel);
                    break;

                default:
                    list = list.OrderBy(s => s.Wiezien.idWieznia);
                    break;
            }

            return View(list);
        }

        [HttpGet]
        public ActionResult PrzeniesWieznia(int? id)
        {
            if (id != null)
            {
                Wiezniowie wiezien = _db.Wiezniowie.FirstOrDefault(x => x.idWieznia == id.Value);
                Wyroki wyrok = _db.Wyroki.FirstOrDefault(x => x.FK_idWieznia == id);

                var model = new PrzydzialyViewModel
                {
                    idWieznia = id.Value,
                    FK_idKategoriiPrzestepstwa = wyrok.FK_idKategoriiPrzestepstwa,
                    Plec = wiezien.Plec
                };

                
               
                model.Cele = from c in _db.Cele
                    join o in _db.Odzial
                    on c.FK_idOdzial equals o.idOdzial
                    join x in _db.KategoriaPrzestepstwaOdzial
                    on o.idOdzial equals x.FK_idOdzial
                    where x.FK_idKategoriiPrzestepstwa == wyrok.FK_idKategoriiPrzestepstwa && c.Wolna &&
                          x.PlecOdzial == wiezien.Plec && c.idCeli != ZwolnijWieznia.Placeholder
                    select c;

                if (model.Cele.Any() == false)
                {
                    TempData["alertMessage"] = "Brak dostępnych cel";
                    return RedirectToAction("WyswietlWiezniow");
                }
                ViewBag.CelList = new SelectList(model.Cele, "idCeli", "idCeli");
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PrzeniesWieznia(PrzydzialyViewModel model)
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
           

            var cela = _db.Cele.FirstOrDefault(x => x.idCeli == model.FK_idCeli);
            if (cela == null)
                return HttpNotFound();
            var count = _db.Przydzialy.Count(x => x.FK_idCeli == cela.idCeli);
            if (cela.Wolna && count < cela.IloscMiejsc && cela.Aktywna_Nieaktywna)
            {
             

                var przydzial = _db.Przydzialy.FirstOrDefault(s => s.FK_idWieznia == model.idWieznia);

                if (przydzial != null)
                {
                    przydzial.FK_idCeli = cela.idCeli;
                    _db.Entry(przydzial).State = EntityState.Modified;
                }
                var historia = new HistoriaZmian
                {
                    idPracownika = id,
                    Data = DateTime.Now.Date
                };

                var historiaZmianPrzydzialu = new HistoriaZmianPrzydzialow
                {
                    idCeli = cela.idCeli,
                    FK_HistoriaZmian_idWpisuPrzydzialu = historia.idWpisu,
                    FK_idWiezniaPrzydzialu = przydzial.FK_idWieznia
                };
                if (count + 1 == cela.IloscMiejsc)
                {
                    cela.Wolna = false;
                    _db.Entry(cela).State = EntityState.Modified;
                }
                if (count - 1 < cela.IloscMiejsc)
                {
                    cela.Wolna = true;
                    _db.Entry(cela).State = EntityState.Modified;
                }


                _db.HistoriaZmian.Add(historia);
                _db.HistoriaZmianPrzydzialow.Add(historiaZmianPrzydzialu);
                _db.SaveChanges();
                return RedirectToAction("WyswietlWiezniow");
            }
            return RedirectToAction("PrzeniesWieznia");
        }

        [HttpGet]
        public ActionResult EdytujWieznia(int? id)
        {
            var wiezien = _db.Wiezniowie.FirstOrDefault(x => x.idWieznia == id);
            return View(wiezien);
        }

        [HttpPost]
        public ActionResult EdytujWieznia(Wiezniowie wiezien)
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var historia = new HistoriaZmian
                {
                    idPracownika = id,
                    Data = DateTime.Now.Date
                };
                _db.Entry(wiezien).State = EntityState.Modified;

                var historiaZmianWieznia = new HistoriaZmianWieznia
                {
                    ImieWieznia = wiezien.ImieWieznia,
                    NazwiskoWieznia = wiezien.NazwiskoWieznia,
                    Pesel = wiezien.Pesel,
                    FK_idWieznia = wiezien.idWieznia,
                    Plec = wiezien.Plec,
                    FK_HistoriaZmian_idWpisuWieznia = historia.idWpisu
                };

                _db.HistoriaZmian.Add(historia);
                _db.HistoriaZmianWieznia.Add(historiaZmianWieznia);
                _db.SaveChanges();
                return RedirectToAction("WyswietlWiezniow");
            }
            return View(wiezien);
        }

        [HttpGet]
        public ActionResult EdytujWyrok(int? id)
        {
            if (id != null)
            {
                var model = new EdytujWyrokViewModel
                {
                    Wyrok = _db.Wyroki.FirstOrDefault(x => x.FK_idWieznia == id),
                    idWieznia = id.Value
                };
                var list = _db.KategoriePrzestepstwa.ToList();
                ViewBag.KatList = new SelectList(list, "idKategoriiPrzestepstwa", "NazwaKategorii");
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EdytujWyrok(EdytujWyrokViewModel model)
        {
            var list = _db.KategoriePrzestepstwa.ToList();
            ViewBag.KatList = new SelectList(list, "idKategoriiPrzestepstwa", "NazwaKategorii");
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var historia = new HistoriaZmian
                {
                    idPracownika = id,
                    Data = DateTime.Now.Date
                };
                _db.Entry(model.Wyrok).State = EntityState.Modified;

                var historiaZmianWyroku = new HistoriaZmianWyrokow
                {
                    idWieznia = model.idWieznia,
                    Czas = model.Wyrok.Czas,
                    DataRozpoczecia = model.Wyrok.DataRozpoczecia,
                    FK_HistoriaZmian_idWpisu = historia.idWpisu,
                    FK_idWyroku = model.Wyrok.idWyroku,
                    idKategoriiPrzestepstwa = model.Wyrok.FK_idKategoriiPrzestepstwa
                };

                _db.HistoriaZmian.Add(historia);
                _db.HistoriaZmianWyrokow.Add(historiaZmianWyroku);
                _db.SaveChanges();
                var idTemp = historiaZmianWyroku.idWieznia;
                return RedirectToAction("PrzeniesWieznia", new {id = idTemp});
            }
            return View(model);
        }
    }
}