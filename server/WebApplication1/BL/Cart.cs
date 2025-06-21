namespace WebApplication1.BL
{
    public class Cart
    {
        private int userId;
        private int movieId;
        private DateOnly rentEnd;
        private double totalPrice;

        public Cart(int userId, int movieId, DateOnly rentEnd, double totalPrice)
        {
            this.userId = userId;
            this.movieId = movieId;
            this.rentEnd = rentEnd;
            this.totalPrice = totalPrice;
        }
        public Cart() { }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateOnly RentEnd { get; set; }
        public double TotalPrice { get; set; }
    }
}
