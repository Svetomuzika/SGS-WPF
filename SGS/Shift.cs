namespace SGS
{
    public class Shift
    {
        public string City { get; set; }
        public string Shop { get; set; }
        public string Employee { get; set; }
        public int Brigade { get; set; }

        public Shift(string city, string shop, string employee, int brigade)
        {
            City = city;
            Shop = shop;
            Employee = employee;
            Brigade = brigade;
        }
    }
}
