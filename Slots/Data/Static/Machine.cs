namespace Slots.Data.Static
{
    public class Machine
    {
        public static int Sum { get; set; }
        public static int Change { get; set; }       
        public static int Coin1quantity { get; set; } = 0;
        public static int Coin2quantity { get; set; } = 0;
        public static int Coin5quantity { get; set; } = 0;
        public static int Coin10quantity { get; set; } = 0;

        public static bool Login { get; set; } = false;


        public static bool BlockOne { get; set; } = false;
        public static bool BlockTwo { get; set; } = false;
        public static bool BlockFive { get; set; } = false;
        public static bool BlockTen { get; set; } = false;

    }
}
