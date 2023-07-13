using Microsoft.AspNetCore.Mvc;
using Slots.Models.Domain;
using Slots.Models;
using Slots.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Slots.Controllers
{
    public class DrinkController : Controller
    {

        private readonly ApplicationDbContext db;

        public DrinkController(ApplicationDbContext db)
        {
            this.db = db;
        }

       
        public async Task<ActionResult> GetDrinks()
        {

            var drinks = await db.Drinks.ToListAsync();
            return View(drinks);
        }

        public async Task<ActionResult> GetDrinksAdmin(int p)
        {
            if (p == 123456)
            {
                Machine.Login = true;
            }
            var drinks = await db.Drinks.ToListAsync();
            return View(drinks);
        }


        public async Task<ActionResult> BuyDrink(DrinkViewModel model, Machine machine)
        {
           var drink = await db.Drinks.FindAsync(model.Id);
            
            machine.Purchase(drink);
            db.SaveChanges();

            return RedirectToAction("GetDrinks");
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            var drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return NotFound();
            }
            return PartialView(drink);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            var drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return NotFound();
            }
            db.Drinks.Remove(drink);
            db.SaveChanges();
            return RedirectToAction("GetDrinksAdmin");
        }

        [HttpGet]
        public ActionResult Save(long id)
        {
            if (id == 0)
            {
                return PartialView();
            }
            var drink = db.Drinks.Find(id);
            var data = new DrinkViewModel()
            {
                Name = drink.Name,
                Price = drink.Price,
                Quantity = drink.Quantity,
                Image = drink.Avatar,
            };
            if (drink != null)
            {
                return PartialView(data);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Save(Drink drink, DrinkViewModel viewModel)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {

                    if (viewModel.Avatar != null)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
                        }
                        drink.Avatar = imageData;
                    }
                    if (viewModel.Avatar == null) { return RedirectToAction("GetDrinksAdmin"); }

                    db.Drinks.Add(drink);
                    db.SaveChanges();
                    return RedirectToAction("GetDrinksAdmin");
                }
                else
                {
                    db.Drinks.Attach(drink);

                    // Only the fields you want to update, will change.
                    db.Entry(drink).Property(p => p.Quantity).IsModified = true;
                    db.Entry(drink).Property(p => p.Price).IsModified = true;


                    //  only if the value is not null, the field will change.
                    db.Entry(drink).Property(p => p.Name).IsModified =
                    drink.Name != null;
                    db.Entry(drink).Property(p => p.Avatar).IsModified =
                    drink.Avatar != null;

                    db.SaveChanges();

                    return RedirectToAction("GetDrinksAdmin");
                }
            }
            return RedirectToAction("GetDrinksAdmin");
        }

       
        public int PlusOne()
        {
            var sum = Machine.Sum + 1;         
            Machine.Sum = sum;          

            return Machine.Sum;
        }

        public int PlusTwo(int Sum)
        {
            var sum = Machine.Sum + 2;
            Machine.Sum = sum;
            
            return Machine.Sum;
        }

        public int PlusFive(int Sum)
        {
            var sum = Machine.Sum + 5;
            Machine.Sum = sum;
            
            return Machine.Sum;
        }

        public int PlusTen(int Sum)
        {
            var sum = Machine.Sum + 10;
            Machine.Sum = sum;
            
            return Machine.Sum;
        }

        public void BlockOne(bool BlockOne)
        {
            if (Machine.BlockOne == true) { Machine.BlockOne = false; } else { Machine.BlockOne = true; }


        }

        public void BlockTwo(bool BlockTwo)
        {
            if (Machine.BlockTwo == true) { Machine.BlockTwo = false; } else { Machine.BlockTwo = true; }
        }

        public void BlockFive(bool BlockFive)
        {
            if (Machine.BlockFive == true) { Machine.BlockFive = false; } else { Machine.BlockFive = true; }
        }

        public void BlockTen(bool BlockTen)
        {
            if (Machine.BlockTen == true) { Machine.BlockTen = false; } else { Machine.BlockTen = true; }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



    }
}