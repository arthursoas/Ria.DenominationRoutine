using Ria.DenominationRoutine;

var denominations = new[] { 10, 50, 100 };
var amounts = new[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

foreach(var amount in amounts)
{
    var combinations = ATM.CalculatePayoutCombinations(denominations, amount);
    var output = ATM.FormatCombinationsOutput(combinations, "EUR");

    Console.WriteLine($"============== {amount} ==============");
    Console.WriteLine(output);
}

Console.ReadKey();
