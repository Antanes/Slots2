using Microsoft.EntityFrameworkCore;
using Slots.Data.Static;
using Slots.Data.ViewModel;
using Slots.Models.Domain;

namespace Slots.Data.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly ApplicationDbContext db;

        public DrinkService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Drink>> GetAllAsync()
        {
            var result = await db.Drinks.ToListAsync();
            return result;
        }
        public async Task<Drink> GetByIdAsync(int id)
        {
            var result = await db.Drinks.FindAsync(id);
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            var result = await db.Drinks.FindAsync(id);
            db.Drinks.Remove(result);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Drink drink, DrinkViewModel viewModel)
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
                    await db.Drinks.AddAsync(drink);
                    await db.SaveChangesAsync();
                }
            }
            else
            {
                db.Drinks.Attach(drink);

                // Only the fields you want to update, will change.
                db.Entry(drink).Property(p => p.Quantity).IsModified = true;
                db.Entry(drink).Property(p => p.Price).IsModified = true;

                //  only if the value is not null, the field will change.
                db.Entry(drink).Property(p => p.Name).IsModified = drink.Name != null;
                db.Entry(drink).Property(p => p.Avatar).IsModified = drink.Avatar != null;

                await db.SaveChangesAsync();
            }
        }

        public async Task Purchase(Drink drink)
        {
            Machine.Change = 0;
            Machine.Coin10quantity = 0;
            Machine.Coin5quantity = 0;
            Machine.Coin2quantity = 0;
            Machine.Coin1quantity = 0;

            if (drink.Price <= Machine.Sum && drink.Quantity > 0)
            {
                drink.Quantity--;
                Change(drink);
                ChangeNominalsQuantity();
                await db.SaveChangesAsync();
            }
            
        }
        public void Change(Drink drink)
        {                         
                Machine.Sum -= drink.Price;
                Machine.Change = Machine.Sum;                            
        }
        public void ChangeNominalsQuantity()
        {            
                int nominal10 = 10;
                int nominal5 = 5;
                int nominal2 = 2;
               
                Machine.Coin10quantity = Machine.Sum / nominal10;
                Machine.Sum -= Machine.Coin10quantity * nominal10;

                Machine.Coin5quantity = Machine.Sum / nominal5;
                Machine.Sum -= Machine.Coin5quantity * nominal5;

                Machine.Coin2quantity = Machine.Sum / nominal2;
                Machine.Sum -= Machine.Coin2quantity * nominal2;

                Machine.Coin1quantity = Machine.Sum;
                Machine.Sum = 0;            
        }

        public void PlusOne()
        {
            var sum = Machine.Sum + 1;
            Machine.Sum = sum;           
        }
        public void PlusTwo()
        {
            var sum = Machine.Sum + 2;
            Machine.Sum = sum;
        }
        public void PlusFive()
        {
            var sum = Machine.Sum + 5;
            Machine.Sum = sum;
        }
        public void PlusTen()
        {
            var sum = Machine.Sum + 10;
            Machine.Sum = sum;
        }
        public void BlockOne()
        {
            if (Machine.BlockOne == true) { Machine.BlockOne = false; } else { Machine.BlockOne = true; }
        }

        public void BlockTwo()
        {
            if (Machine.BlockTwo == true) { Machine.BlockTwo = false; } else { Machine.BlockTwo = true; }
        }

        public void BlockFive()
        {
            if (Machine.BlockFive == true) { Machine.BlockFive = false; } else { Machine.BlockFive = true; }
        }

        public void BlockTen()
        {
            if (Machine.BlockTen == true) { Machine.BlockTen = false; } else { Machine.BlockTen = true; }
        }
    }
}
