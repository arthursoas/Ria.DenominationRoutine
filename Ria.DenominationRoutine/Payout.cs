namespace Ria.DenominationRoutine
{
    public class Payout
    {
        public static ICollection<IDictionary<int, int>> CalculatePayoutPossibilities(ICollection<int> denominations, int amount)
        {
            var possibilities = new List<IDictionary<int, int>>();

            var denominationLimits = new Dictionary<int, int>();
            foreach(var denomination in denominations)
            {
                denominationLimits[denomination] = int.MaxValue;
            }

            return CalculatePayoutPossibilities(amount, possibilities, denominationLimits);
        }

        private static ICollection<IDictionary<int, int>> CalculatePayoutPossibilities(
            int amount,
            ICollection<IDictionary<int, int>> possibilities,
            IDictionary<int, int> denominationLimits)
        {
            var denominations = denominationLimits.Keys.OrderByDescending(d => d);
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
                    denominationLimits = possibility;
                }

                // Decrement the limit of the smallest denomination value that still can be used
                // Ignore the smallest denomination to avoid unnecessary processing
                var changed = -1;
                var index = denominations.Count() - 2;
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

                // If some limit was changed, calculate new possibilities
                if (changed == -1)
                {
                    stop = true;
                }
            }

            return possibilities;
        }
    }
}
