using CsharpStarterkit;
using Newtonsoft.Json;
using System.Text;

string gameUrl = "https://api.considition.com/";
string apiKey = File.ReadAllText(@"..\..\..\Key");
string mapFile = "Map.json";

HttpClient client = new();
client.BaseAddress = new Uri(gameUrl, UriKind.Absolute);

GameInput input = new()
{
    MapName = "Gothenburg",
    Proposals = new(),
    Iterations = new()
};

string mapDataText = File.ReadAllText(mapFile);
MapData mapData = JsonConvert.DeserializeObject<MapData>(mapDataText);

//Hard coded personality values
personality[] personality = new personality[5]
{
    new()
    {
        name = "conservative",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.0m,
        acceptedMaxInterest = 0.02m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "riskTaker",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.1m,
        acceptedMaxInterest = 100000m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "innovative",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.02m,
        acceptedMaxInterest = 0.1m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "practical",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.01m,
        acceptedMaxInterest = 0.05m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "spontaneous",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.01m,
        acceptedMaxInterest = 0.5m,
        livingStandardMultiplier = 0.1m
    }
};

List<Customer> activeCustomers = new List<Customer>();

foreach (Customer customer in mapData.customers)
{


    decimal interestRate = interestRateCalculator(customer);


    if (interestRate != -1)
    {
        activeCustomers.Add(customer);
        input.Proposals.Add(new CustomerLoanRequestProposal()
        {
            CustomerName = customer.name,
            MonthsToPayBackLoan = mapData.gameLengthInMonths,
            YearlyInterestRate = interestRate,
        });
    }
    
}

Random random = new Random();

string[] actionTypes = { "Award", "Skip" };
string[] awardTypes = { "IkeaFoodCoupon", "IkeaDeliveryCheck", "IkeaCheck", "GiftCard", "HalfInterestRate", "NoInterestRate" };

//This is so more data to optimize awards
//var awardsData = JsonConvert.DeserializeObject<AwardsRoot>(File.ReadAllText("Awards.json"));


for (int i = 0; i < mapData.gameLengthInMonths; i++)
{
    //var x = new CustomerActionIteration();
    //input.Iterations.Add(x);
    Dictionary<string, CustomerAction> t = new();
    input.Iterations.Add(t);
    foreach (var customer in activeCustomers)
    {
        //Award good people here

        string randomType = actionTypes[random.Next(actionTypes.Length)];
        string randomAward = randomType == "Skip" ? "None" : awardTypes[random.Next(awardTypes.Length)];

        randomType = "Award";
        randomAward = "NoInterestRate";





        t.Add(customer.name, new CustomerAction
        {
            Type = randomType,
            Award = randomAward
        });
    }
}

HttpRequestMessage request = new();
request.Method = HttpMethod.Post;
request.RequestUri = new Uri(gameUrl + "game", UriKind.Absolute);
request.Headers.Add("x-api-key", apiKey);
request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

var res = client.Send(request);
Console.WriteLine(res.StatusCode);
Console.WriteLine(await res.Content.ReadAsStringAsync());





decimal interestRateCalculator(Customer customer)
{
    //The basic interestRate, aka the maximum
    decimal interestRate = 0;

    decimal maximumInterest = personality.ToList().Find(n => n.name.ToLower() == customer.personality.ToLower()).acceptedMaxInterest; ;
    decimal minimumInterest = personality.ToList().Find(n => n.name.ToLower() == customer.personality.ToLower()).acceptedMinInterest;

    interestRate = (maximumInterest + minimumInterest) / 2;
    interestRate -= interestRate - (interestRate - minimumInterest) * 0.1m;

    interestRate = minimumInterest;


    return interestRate;
}






class personality
{
    public string name;
    public decimal happinesMultiplier { get; set; }
    public decimal acceptedMinInterest { get; set; }
    public decimal acceptedMaxInterest { get; set; }
    public decimal livingStandardMultiplier {  get; set; }
}using CsharpStarterkit;
using Newtonsoft.Json;
using System.Text;

string gameUrl = "https://api.considition.com/";
string apiKey = File.ReadAllText(@"..\..\..\Key");
string mapFile = "Map.json";

HttpClient client = new();
client.BaseAddress = new Uri(gameUrl, UriKind.Absolute);

GameInput input = new()
{
    MapName = "Gothenburg",
    Proposals = new(),
    Iterations = new()
};

string mapDataText = File.ReadAllText(mapFile);
MapData mapData = JsonConvert.DeserializeObject<MapData>(mapDataText);

//Hard coded personality values
personality[] personality = new personality[5]
{
    new()
    {
        name = "conservative",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.0m,
        acceptedMaxInterest = 0.02m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "riskTaker",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.1m,
        acceptedMaxInterest = 100000m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "innovative",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.02m,
        acceptedMaxInterest = 0.1m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "practical",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.01m,
        acceptedMaxInterest = 0.05m,
        livingStandardMultiplier = 0.1m
    },
    new()
    {
        name = "spontaneous",
        happinesMultiplier = 0.2m,
        acceptedMinInterest = 0.01m,
        acceptedMaxInterest = 0.5m,
        livingStandardMultiplier = 0.1m
    }
};

List<Customer> activeCustomers = new List<Customer>();

foreach (Customer customer in mapData.customers)
{


    decimal interestRate = interestRateCalculator(customer);


    if (interestRate != -1)
    {
        activeCustomers.Add(customer);
        input.Proposals.Add(new CustomerLoanRequestProposal()
        {
            CustomerName = customer.name,
            MonthsToPayBackLoan = mapData.gameLengthInMonths,
            YearlyInterestRate = interestRate,
        });
    }
    
}

Random random = new Random();

string[] actionTypes = { "Award", "Skip" };
string[] awardTypes = { "IkeaFoodCoupon", "IkeaDeliveryCheck", "IkeaCheck", "GiftCard", "HalfInterestRate", "NoInterestRate" };

//This is so more data to optimize awards
//var awardsData = JsonConvert.DeserializeObject<AwardsRoot>(File.ReadAllText("Awards.json"));


for (int i = 0; i < mapData.gameLengthInMonths; i++)
{
    //var x = new CustomerActionIteration();
    //input.Iterations.Add(x);
    Dictionary<string, CustomerAction> t = new();
    input.Iterations.Add(t);
    foreach (var customer in activeCustomers)
    {
        //Award good people here

        string randomType = actionTypes[random.Next(actionTypes.Length)];
        string randomAward = randomType == "Skip" ? "None" : awardTypes[random.Next(awardTypes.Length)];

        randomType = "Award";
        randomAward = "NoInterestRate";





        t.Add(customer.name, new CustomerAction
        {
            Type = randomType,
            Award = randomAward
        });
    }
}

HttpRequestMessage request = new();
request.Method = HttpMethod.Post;
request.RequestUri = new Uri(gameUrl + "game", UriKind.Absolute);
request.Headers.Add("x-api-key", apiKey);
request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

var res = client.Send(request);
Console.WriteLine(res.StatusCode);
Console.WriteLine(await res.Content.ReadAsStringAsync());





decimal interestRateCalculator(Customer customer)
{
    //The basic interestRate, aka the maximum
    decimal interestRate = 0;

    decimal maximumInterest = personality.ToList().Find(n => n.name.ToLower() == customer.personality.ToLower()).acceptedMaxInterest; ;
    decimal minimumInterest = personality.ToList().Find(n => n.name.ToLower() == customer.personality.ToLower()).acceptedMinInterest;

    interestRate = (maximumInterest + minimumInterest) / 2;
    interestRate -= interestRate - (interestRate - minimumInterest) * 0.1m;

    interestRate = minimumInterest;


    return interestRate;
}






class personality
{
    public string name;
    public decimal happinesMultiplier { get; set; }
    public decimal acceptedMinInterest { get; set; }
    public decimal acceptedMaxInterest { get; set; }
    public decimal livingStandardMultiplier {  get; set; }
}
