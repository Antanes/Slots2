using Microsoft.AspNetCore.Mvc;
using Slots.Models.Domain;
using Slots.Data.ViewModel;
using Slots.Data.Static;
using Slots.Data.Services;

namespace Slots.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IDrinkService _service;

        public DrinkController(IDrinkService service)
        {
            _service = service;
        }


        public async Task<ActionResult> GetDrinks()
        {

            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<ActionResult> GetDrinksAdmin(int p)
        {
            if (p == 123456)
            {
                Machine.Login = true;  
            }

            var data = await _service.GetAllAsync();
            return View(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var drink = await _service.GetByIdAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            return PartialView(drink);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _service.GetByIdAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            
            return RedirectToAction("GetDrinksAdmin");
        }

        [HttpGet]
        public async Task <IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView();
            }
            var drink = await _service.GetByIdAsync(id);
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
        public async Task<IActionResult> Save(Drink drink, DrinkViewModel viewModel)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {

                await _service.UpdateAsync(drink, viewModel);

            }
                       
            return RedirectToAction("GetDrinksAdmin");
        }

        public async Task<IActionResult> BuyDrink(int id)
        {
            var drink = await _service.GetByIdAsync(id);
            await _service.Purchase(drink);            
            return RedirectToAction("GetDrinks");
        }

        public int PlusOne()
        {
            _service.PlusOne();
            return Machine.Sum;
        }

        public int PlusTwo()
        {
            _service.PlusTwo();
            return Machine.Sum;
        }
        public int PlusFive()
        {
            _service.PlusFive();
            return Machine.Sum;
        }
        public int PlusTen()
        {
            _service.PlusTen();
            return Machine.Sum;
        }

        public void BlockOne()
        {
            _service.BlockOne();
        }

        public void BlockTwo()
        {
            _service.BlockTwo();
        }

        public void BlockFive()
        {
            _service.BlockFive();
        }

        public void BlockTen()
        {
            _service.BlockTen();
        }
              

    }
}