// See https://aka.ms/new-console-template for more information
/*
You are given a sorted list of distinct integers from 0 to 99, for instance [0, 1, 2, 50, 52, 75]. Your task is to produce a string that describes numbers missing from the list; in this case "3-49,51,53 74,76-99".
Examples:
[] “0-99”
[0] “1-99”
[3, 5] “0-2,4,6-99”

[5, 3]
*/
Console.WriteLine("Hello, World!");

string outputString;
int[] input;

input = new int[] { };
outputString = Process(input); // 0-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 0 };
outputString = Process(input); // 1-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 99 };
outputString = Process(input); // 0-98
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 1 };
outputString = Process(input); // 0,2-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 98 };
outputString = Process(input); // 0-97,99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 50 };
outputString = Process(input); // 0-49,51-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 1, 3, 5, 7 };
outputString = Process(input); // 0,2,4,6,8-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 3, 5 };
outputString = Process(input); // 0-2,4,6-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 0, 3, 7, 99 };
outputString = Process(input); // 1,2,4-6,8-98
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 0, 1, };
outputString = Process(input); // 2-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 98, 99, };
outputString = Process(input); // 0-97
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 1, 2, };
outputString = Process(input); // 0,3-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 2, 3, };
outputString = Process(input); // 0,1,4-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");

input = new int[] { 0, 1, 2, 50, 52, 75 };
outputString = Process(input); // 3-49,51,53-74,76-99
Console.WriteLine($"I: {string.Join(",", input)}   O:{outputString}");


string Process(int[] input, int minValue = 0, int maxValue = 99)
{
    string outputString = "";

    if (input.Any() == false)
    {
        return $"{minValue}-{maxValue}";
    }

    if (input.Count() == 1)
    {
        if (input[0] == minValue)
        {
            return $"{minValue + 1}-{maxValue}";
        }

        if (input[0] == maxValue)
        {
            return $"{minValue}-{maxValue - 1}";
        }

        if (input[0] == minValue + 1)
        {
            return $"{minValue + 2}-{maxValue}";
        }

        if (input[0] == maxValue - 1)
        {
            return $"{minValue}-{maxValue - 2},{maxValue}";
        }

        return $"{minValue}-{input[0] - 1},{input[0] + 1}-{maxValue}";
    }

    input = input.OrderBy(x => x).ToArray();
    var firstIsMin = input.Any() && input[0] == minValue;
    var lastIsMax = input.Any() && input[input.Count() - 1] == maxValue;
    var invInput = input.Select(x => x - 1)
        .Union(input.Select(x => x + 1))
        .Union(new int[] { 0 })
        .Where(x => x < maxValue)
        .OrderBy(x => x)
        .Except(input)
        .ToArray();

    for (int i = 0; i < invInput.Length; i++)
    {
        if (i == 0)
        {
            if (!firstIsMin)
            {
                outputString += 0;
            }

            continue;
        }

        if (invInput[i] - invInput[i - 1] == 1)
        {
            outputString += $",{invInput[i]}";
        }
        else
        {
            outputString += input.Any(x => x > invInput[i - 1] && x < invInput[i])
                ? $",{invInput[i]}"
                : $"-{invInput[i]}";
        }
    }

    

    if (lastIsMax == false)
    {
        outputString += maxValue - input[input.Length - 1] > 1
            ? $"-{maxValue}"
            : $",{maxValue}";
    }

    return outputString.TrimStart(',');
}