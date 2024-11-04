using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpStarterkit;

public class GameInput
{
    public required string MapName { get; init; }
    public required List<CustomerLoanRequestProposal> Proposals { get; init; }
    public required List<Dictionary<string, CustomerAction>> Iterations { get; init; }
}
public class CustomerLoanRequestProposal
{
    public string CustomerName { get; set; } = null!;
    public decimal YearlyInterestRate { get; set; }
    public int MonthsToPayBackLoan { get; set; }
}

public enum CustomerActionType
{
    Skip,
    Punish,
    Award
}
public record CustomerAction
{
    public string Type { get; set; }
    public string Award { get; set; }
}

public enum AwardType
{
    None,
    IkeaCheck,
    IkeaFoodCoupon,
    IkeaDeliveryCheck,
    NoInterestRate,
    GiftCard,
    HalfInterestRate,
}


public class AwardsRoot
{
    public Awards Awards { get; set; }
}

public class Awards
{
    public Ikeafoodcoupon IkeaFoodCoupon { get; set; }
    public Ikeadeliverycheck IkeaDeliveryCheck { get; set; }
    public Ikeacheck IkeaCheck { get; set; }
    public Giftcard GiftCard { get; set; }
    public Halfinterestrate HalfInterestRate { get; set; }
    public Nointerestrate NoInterestRate { get; set; }
}

public class Ikeafoodcoupon
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "IkeaFoodCoupon";
}

public class Ikeadeliverycheck
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "IkeaDeliveryCheck";
}

public class Ikeacheck
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "IkeaCheck";
}

public class Giftcard
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "GiftCard";
}

public class Halfinterestrate
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "HalfInterestRate";
}

public class Nointerestrate
{
    public float cost { get; set; }
    public int baseHappiness { get; set; }

    public const string Value = "NoInterestRate";
}