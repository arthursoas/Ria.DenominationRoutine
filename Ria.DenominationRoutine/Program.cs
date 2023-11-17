using Ria.DenominationRoutine;
using System.Text.Json;

var denominations = new int[] { 10, 50, 100 };
var amount = 100;

var a = Payout.CalculatePayoutPossibilities(denominations, amount);

Console.WriteLine(JsonSerializer.Serialize(a));
Console.ReadLine();