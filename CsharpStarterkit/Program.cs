using CsharpStarterkit;
using Newtonsoft.Json;
using System.Text;

string gameUrl = "https://api.considition.com/";
string apiKey = "";
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

foreach (Customer customer in mapData.customers)
{
    input.Proposals.Add(new CustomerLoanRequestProposal()
    {
        CustomerName = customer.name,
        MonthsToPayBackLoan = mapData.gameLengthInMonths,
        YearlyInterestRate = 0.1m,
    });
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
    foreach (var customer in mapData.customers)
    {
        string randomType = actionTypes[random.Next(actionTypes.Length)];
        string randomAward = randomType == "Skip" ? "None" : awardTypes[random.Next(awardTypes.Length)];
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