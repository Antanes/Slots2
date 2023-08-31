using Slots.Data.ViewModel;
using Slots.Models.Domain;

namespace Slots.Data.Services
{
    public interface IDrinkService
    {
        Task<IEnumerable<Drink>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<Drink> GetByIdAsync(int id);
        Task UpdateAsync(Drink drink, DrinkViewModel viewModel);
        Task Purchase(Drink drink);        
        void PlusOne();
        void PlusTwo();
        void PlusFive();
        void PlusTen();
        void BlockOne();
        void BlockTwo();
        void BlockFive();
        void BlockTen();
    }
}
