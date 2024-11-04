namespace CsharpStarterkit
{

    public class MapData
    {
        public string name { get; set; }
        public int budget { get; set; }
        public int gameLengthInMonths { get; set; }
        public Customer[] customers { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public Loan loan { get; set; }
        public string personality { get; set; }
        public decimal capital { get; set; }
        public decimal income { get; set; }
        public decimal monthlyExpenses { get; set; }
        public int numberOfKids { get; set; }
        public decimal homeMortgage { get; set; }
        public bool hasStudentLoan { get; set; }
    }

    public class Loan
    {
        public string product { get; set; }
        public decimal environmentalImpact { get; set; }
        public decimal amount { get; set; }
    }

}