
class Day1
{

    static string input = File.ReadAllText("Day1.txt");

    static void Max(int[] maxArray, int number)
    {
        if (maxArray[0] < number)
        {
            maxArray[0] = number;
        }

        int i = 0;

        while (i < maxArray.Length - 1 && maxArray[i] > maxArray[i + 1])
        {
            if (maxArray[i] > maxArray[i + 1])
            {
                int temp = maxArray[i + 1];
                maxArray[i + 1] = maxArray[i];
                maxArray[i] = temp;
            }

            i++;
        }
    }

    public static void Run1()
    {

        int i = 0;

        int sum = 0;
        int max = 0;

        while (true)
        {

            if (Parser.NextInt(input, ref i, out var number))
            {
                sum += number;

                if (Parser.NextLine(input, ref i) == false)
                {
                    break;
                }
            }
            else
            {
                if (sum > max)
                {
                    max = sum;
                }

                sum = 0;

                Parser.RemoveWhiteSpace(input, ref i);
            }

        }

        if (sum > max)
        {
            max = sum;
        }

        Console.WriteLine(max);
    }

    public static void Run2()
    {

        int i = 0;

        int sum = 0;

        int[] maxArray = new int[3];

        while (true)
        {

            if (Parser.NextInt(input, ref i, out var number))
            {
                sum += number;

                if (Parser.NextLine(input, ref i) == false)
                {
                    break;
                }
            }
            else
            {

                Max(maxArray, sum);    

                sum = 0;

                Parser.RemoveWhiteSpace(input, ref i);
            }

        }

        Max(maxArray, sum);

        int max = maxArray.Sum();

        Console.WriteLine(max);
    }


}

