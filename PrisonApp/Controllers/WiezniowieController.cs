using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PrisonApp.Models;
using PrisonApp.Models.ViewModels;
using PrisonApp.Models.ViewModels.HistoriaViewModel;

namespace PrisonApp.Controllers
{
    [Authorize(Roles = "Pracownik, Administrator")]
    public class WiezniowieController : Controller
    {
        private readonly Model _db = new Model();


        [HttpGet]
        public ActionResult DodajWieznia()
        {
            var list = _db.KategoriePrzestepstwa.ToList();
            ViewBag.KatList = new SelectList(list, "idKategoriiPrzestepstwa", "NazwaKategorii");
            return View();
        }

        [HttpPost]
        public ActionResult DodajWieznia(WiezienViewModel model)
        {
            var list = _db.KategoriePrzestepstwa.ToList();
            ViewBag.KatList = new SelectList(list, "idKategoriiPrzestepstwa", "NazwaKategorii");


            if (ModelState.IsValid)
            {
                var wiezien = new Wiezniowie
                {
                    ImieWieznia = model.ImieWieznia,
                    NazwiskoWieznia = model.NazwiskoWieznia,
                    Pesel = model.Pesel,
                    Plec = model.Plec
                };


                var wyrok = new Wyroki
                {
                    FK_idKategoriiPrzestepstwa = model.idKategoriiPrzestepstwa,
                    FK_idWieznia = wiezien.idWieznia,
                    Czas = model.Czas,
                    DataRozpoczecia = model.DataRozpoczecia
                };


                _db.Wiezniowie.Add(wiezien);
                _db.Wyroki.Add(wyrok);
                _db.SaveChanges();
                return RedirectToAction("PrzydzielWieznia", new {id = wiezien.idWieznia});
            }

            return View(model);
        }

        public ActionResult WyswietlWiezniow()
        {
            var list = _db.Wiezniowie.Where(
                x => x.idWieznia != x.Przydzialy.FK_idWieznia || x.Przydzialy.FK_idCeli == Models.ZwolnijWieznia.Placeholder);
            return View(list);
        }

        [HttpGet]
        public ActionResult PrzydzielWieznia(int? id)
        {
            var model = new PrzydzialyViewModel();
            if (id != null)
            {
                model.idWieznia = id.Value;

                var wiezien = _db.Wiezniowie.FirstOrDefault(x => x.idWieznia == id);

                model.Plec = wiezien.Plec;

                Wyroki wyrok = _db.Wyroki.FirstOrDefault(x => x.FK_idWieznia == id);
                model.FK_idKategoriiPrzestepstwa = wyrok.FK_idKategoriiPrzestepstwa;
                model.Cele = from c in _db.Cele
                    join o in _db.Odzial
                    on c.FK_idOdzial equals o.idOdzial
                    join x in _db.KategoriaPrzestepstwaOdzial
                    on o.idOdzial equals x.FK_idOdzial
                    where x.FK_idKategoriiPrzestepstwa == wyrok.FK_idKategoriiPrzestepstwa && c.Wolna &&
                          x.PlecOdzial == wiezien.Plec && c.idCeli != Models.ZwolnijWieznia.Placeholder
                    select c;
                ViewBag.CelList = new SelectList(model.Cele, "idCeli", "idCeli");
                return View(model);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult PrzydzielWieznia(PrzydzialyViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var cela = _db.Cele.FirstOrDefault(x => x.idCeli == model.FK_idCeli);
                if (cela == null)
                    return HttpNotFound();
                var count = _db.Przydzialy.Count(x => x.FK_idCeli == cela.idCeli);
                if (cela.Wolna && count < cela.IloscMiejsc && cela.Aktywna_Nieaktywna)
                {
                    var przydzial = new Przydzialy
                    {
                        FK_idCeli = cela.idCeli,
                        FK_idWieznia = model.idWieznia
                    };
                    if (count + 1 == cela.IloscMiejsc)
                    {
                        cela.Wolna = false;
                        _db.Entry(cela).State = EntityState.Modified;
                    }
                    _db.Przydzialy.Add(przydzial);
                    _db.SaveChanges();
                    return RedirectToAction("WyswietlWiezniow", "Zmiany");
                }
            }
            return RedirectToAction("PrzydzielWieznia");
        }

        public ActionResult WyswietlHistorieWieznia()
        {
            var list = from x in _db.HistoriaZmian
                join y in _db.HistoriaZmianWieznia on x.idWpisu equals y.FK_HistoriaZmian_idWpisuWieznia
                select new HistoriaZmianWiezniaViewModel
                {
                    idWieznia = y.FK_idWieznia,
                    ImieWieznia = y.ImieWieznia,
                    NazwiskoWieznia = y.NazwiskoWieznia,
                    Pesel = y.Pesel,
                    Plec = y.Plec,
                    Pracownik = (from o in _db.Users
                        where o.Id == x.idPracownika
                        select o.UserName).FirstOrDefault(),
                    Data = x.Data
                };

            return View(list);
        }

        public ActionResult WyswietlHistorieWyrokow()
        {
            var list = from x in _db.HistoriaZmian
                join y in _db.HistoriaZmianWyrokow on x.idWpisu equals y.FK_HistoriaZmian_idWpisu
                select new HistoriaZmianWyrokowViewModel
                {
                    idWieznia = y.idWieznia,
                    Czas = y.Czas,
                    Data = x.Data,
                    DataRozpoczecia = y.DataRozpoczecia,
                    NazwaKategorii = (from o in _db.KategoriePrzestepstwa
                        where y.idKategoriiPrzestepstwa == o.idKategoriiPrzestepstwa
                        select o.NazwaKategorii).FirstOrDefault(),
                    Pracownik = (from o in _db.Users
                        where o.Id == x.idPracownika
                        select o.UserName).FirstOrDefault()
                };

            return View(list);
        }

        public ActionResult WyswietlHistoriePrzydzialow()
        {
            var list = from x in _db.HistoriaZmian
                join y in _db.HistoriaZmianPrzydzialow on x.idWpisu equals y.FK_HistoriaZmian_idWpisuPrzydzialu
                select new HistoriaZmianPrzydzialowViewModel
                {
                    idWieznia = y.FK_idWiezniaPrzydzialu,
                    idCeli = y.idCeli,
                    Pracownik = (from o in _db.Users
                        where o.Id == x.idPracownika
                        select o.UserName).FirstOrDefault(),
                    Data = x.Data
                };
            return View(list);
        }

        public ActionResult ZwolnijWieznia(int? id)
        {
            if (id != null)
            {
                var przydzial = _db.Przydzialy.FirstOrDefault(x => x.FK_idWieznia == id);
                var cela = _db.Cele.FirstOrDefault(x => x.idCeli == przydzial.FK_idCeli);
                var model = new Przydzialy
                {
                    FK_idCeli = Models.ZwolnijWieznia.Placeholder,
                    FK_idWieznia = id.Value
                };
                _db.Entry(przydzial).CurrentValues.SetValues(model);

                _db.SaveChanges();
                if (cela.Wolna == false)
                    cela.Wolna = true;
                return RedirectToAction("WyswietlWiezniow", "Zmiany");
            }
            return HttpNotFound();
        }
    }
}