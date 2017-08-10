using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empiredtest.Controllers
{
    public class AnimalsController : Controller
    {
        AnimaldbEntities db = new AnimaldbEntities();
        static string animal="";
        //
        // GET: /Animals/
        public ActionResult Index()
        {
            
            //get the list of animals
            return View(db.Animals.ToList());
        }
        public ActionResult  Questions()
        {
            var data = db.Animals.ToList();

            return View(data);
        }
        [HttpPost]
        public ActionResult Add(Animal model)
        
        {
            //code for saving the new animal

            //if(ModelState.IsValid)
            if (model.Name != string.Empty && model.Parts != null && model.Sound != null && model.Color != null)
            {
               
                db.Animals.Add(new Animal
                {
                    
                    Name = model.Name,
                    Parts=model.Parts,
                    Sound=model.Sound,
                    Color=model.Color

                });

                if (db.Animals.Any(check => check.Name == model.Name))
                {
                    animal = "already exists";
                    return PartialView("_partial", animal);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                animal = "not valid";
                return PartialView("_partial", animal);

            }

           // return View(model);
        }

        
        public ActionResult Add()
        {
            //Displaying the edit code for adding the new animal
            return View();
        }
        [HttpPost]
        public ActionResult Questions(FormCollection collection)
            {
            //Questions to the user selection
                animal = "";
            var data= db.Animals.ToList();
            //if (!string.IsNullOrEmpty((collection["checkparts"]) || (collection["checksound"]) || (collection["checkcolor"])))
            if (collection["checkparts"] != string.Empty || collection["checksound"] != string.Empty || collection["checkcolor"] != string.Empty)
            {
                string checkparts = collection["checkparts"];
                string checksound = collection["checksound"];
                string checkcolor = collection["checkcolor"];


                foreach (var item in data)
                {
                    if (item.Parts == checkparts && item.Sound == checksound && item.Color == checkcolor)
                    {
                        animal = item.Name;
                        //return Content("The animal is" + item.Name);
                    }
                                        
                }
               // return Content("The animal is " + animal);  
                
            }
            else
            {
                animal = "none";
            }
            if(animal=="")
            {
                animal = "none";

            }


            return PartialView("_partial",animal);
        }
	}
}