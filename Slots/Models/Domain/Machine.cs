namespace Slots.Models.Domain
{
    public class Machine
    {
        public static int Sum { get; set; }
        public static int Change { get; set; }
        public void Purchase(Drink drink)
        {
            if (drink.Price < Sum && drink.Quantity > 0)
            {
                drink.Quantity--;
                Sum -= drink.Price;
                Change = Sum;
                ChangePrice = Sum;
                Sum = 0;
                Coin10 = ChangePrice / 10;
                ChangePrice -= Coin10 * 10;
                Coin5 = ChangePrice / 5;
                ChangePrice -= Coin5 * 5;
                Coin2 = ChangePrice / 2;
                ChangePrice -= Coin2 * 2;
                Coin1 = ChangePrice;
            }
        }
        public static int ChangePrice { get; set; } = 0;
        public static int Coin1 { get; set; } = 0;
        public static int Coin2 { get; set; } = 0;
        public static int Coin5 { get; set; } = 0;
        public static int Coin10 { get; set; } = 0;

        public static bool Login { get; set; } = false;


        public static bool BlockOne { get; set; } = false;
        public static bool BlockTwo { get; set; } = false;
        public static bool BlockFive { get; set; } = false;
        public static bool BlockTen { get; set; } = false;
       
    }
}
