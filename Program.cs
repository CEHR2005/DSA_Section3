namespace Section_3;

internal class Program
{
    // Define available coin denominations
    private static readonly decimal[] Coins = { 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1.00M, 2.00M };

    // List to store all possible combinations
    private static readonly List<List<decimal>> Combinations = new();

    // Temporary list to hold the current combination being checked
    private static readonly List<decimal> CurrentCombination = new();

    private static void Main()
    {
        try
        {
            Console.Write("Enter the amount (<= 5.00): ");
            // Read and parse the input amount
            var amount = decimal.Parse(Console.ReadLine() ?? string.Empty);

            // Validate the entered amount
            if (amount <= 0)
            {
                Console.WriteLine("Wrong amount");
                return;
            }

            // Find all possible combinations for the given amount
            FindCombinations(amount, 0);

            // Display all found combinations and their respective coin counts
            foreach (var combination in Combinations)
            {
                Console.WriteLine(string.Join(", ", combination) + $" - Total Coins: {combination.Count}");
            }

            // Find and display the combination with the fewest number of coins
            var minCoinsCombination = Combinations.MinBy(c => c.Count);
            if (minCoinsCombination == null) return;
            Console.WriteLine("\nCombination with the Least Coins:");
            Console.WriteLine(string.Join(", ", minCoinsCombination) + $" - Total Coins: {minCoinsCombination.Count}");
        }
        catch (FormatException)
        {
            // Handle input format errors
            Console.WriteLine("Invalid input format. Try number formatted like: 4,34");
        }
    }

    // Recursive method to find all combinations that sum up to the given amount
    private static void FindCombinations(decimal amount, int coinIndex)
    {
        // If the exact amount is reached and the combination has 10 or fewer coins
        if (amount == 0 && CurrentCombination.Count <= 10)
        {
            // Add the current combination to the list of combinations
            Combinations.Add(new List<decimal>(CurrentCombination));
            return;
        }

        // If the amount is overshot or if there are more than 10 coins, return
        if (amount < 0 || CurrentCombination.Count >= 10)
        {
            return;
        }

        // Iterate through the coin denominations
        for (var i = coinIndex; i < Coins.Length; i++)
        {
            // Skip if the current combination already has 9 coins
            if (CurrentCombination.Count >= 9) continue;

            // Add the current coin to the combination and recurse
            CurrentCombination.Add(Coins[i]);
            FindCombinations(amount - Coins[i], i);

            // Remove the last coin to backtrack and try the next coin
            CurrentCombination.RemoveAt(CurrentCombination.Count - 1);
        }
    }
}
