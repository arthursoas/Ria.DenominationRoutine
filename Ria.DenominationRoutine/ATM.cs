﻿namespace Ria.DenominationRoutine
{
    public class ATM
    {
        public static ICollection<IDictionary<int, int>> CalculatePayoutCombinations(ICollection<int> denominations, int amount)
        {
            denominations = denominations.OrderByDescending(d => d).ToList();
            var possibilities = new List<IDictionary<int, int>>();
            var denominationLimits = new Dictionary<int, int>();
            foreach(var denomination in denominations)
            {
                denominationLimits[denomination] = int.MaxValue;
            }

            var possibility = new Dictionary<int, int>();
            var stop = false;

            while (!stop)
            {
                // Count how many notes of each denomination can be used
                var rest = amount;
                foreach (var denomination in denominations)
                {
                    var count = rest / denomination;

                    // Do not allow use more notes than available for this round
                    count = count > denominationLimits[denomination]
                        ? denominationLimits[denomination]
                        : count;

                    possibility[denomination] = count;
                    rest -= (denomination * count);
                }
                if (rest == 0)
                {
                    possibilities.Add(new Dictionary<int, int>(possibility));
                }
                denominationLimits = possibility;

                // Decrement the limit of the lowest denomination with more than 0 usage
                // Ignore the lowest denomination to avoid unnecessary processing
                var changed = -1;
                var index = denominations.Count - 2;
                while (changed == -1 && index >= 0)
                {
                    var denomination = denominations.ElementAt(index);
                    if (denominationLimits[denomination] > 0)
                    {
                        denominationLimits[denomination] -= 1;
                        changed = denomination;
                    }
                    index--;
                }

                // Allow infinite use of notes below the changed one
                foreach (var denomination in denominations.Where(d => d < changed))
                {
                    denominationLimits[denomination] = int.MaxValue;
                }

                // If no limit was changed this round, stop execution
                if (changed == -1)
                {
                    stop = true;
                }
            }

            return possibilities;
        }

        public static string FormatCombinationsOutput(ICollection<IDictionary<int, int>> combinations, string unit)
        {
            var output = string.Empty;
            foreach (var combination in combinations)
            {
                foreach (var denomination in combination)
                {
                    if (denomination.Value > 0)
                    {
                        output += $"{denomination.Value} x {denomination.Key} {unit} + ";
                    }
                }
                output = output.Remove(output.Length - 3);
                output += "\n";
            }

            return output;
        }
    }
}
