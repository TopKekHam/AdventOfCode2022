
internal class Day3
{

    static string input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

    public static void Run1Func()
    {
        string[] lines = File.ReadAllText("Day3.txt").Split("\r\n");

        Func<char, int> ToPriority = (c) => c >= 'a' ? c - 'a' + 1 : c - 'A' + 27;
        
        var sum = lines
        .Select(line => new Tuple<string, string>(line.Substring(0, line.Length / 2), line.Substring(line.Length / 2, line.Length / 2)))
        .Select(t => ToPriority(t.Item1.First(c => t.Item2.Contains(c))))
        .Sum();
        
        Console.WriteLine(sum);
    }

    public static void Run1()
    {

        string[] lines = File.ReadAllText("Day3.txt").Split("\r\n");

        int sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            sum += Sum(line.Substring(0, line.Length / 2), line.Substring(line.Length / 2, line.Length / 2));
        }

        Console.WriteLine(sum);
    }

    public static void Run2()
    {
        string[] lines = File.ReadAllText("Day3.txt").Split("\r\n");

        int sum = 0;
        int elfGroupSize = 3;

        for (int i = 0; i < lines.Length; i += elfGroupSize)
        {
            sum += Sum(lines[i], lines[i + 1], lines[i + 2]);
        }

        Console.WriteLine(sum);
    }

    static int GetPriority(char c)
    {
        return c >= 'a' ? c - 'a' + 1 : (c - 'A') + 27;
    }

    unsafe static int Sum(params string[] strs)
    {
        int length = 26 * 2;
        int* totalCount = stackalloc int[length];

        for (int si = 0; si < strs.Length; si++)
        {
            var line = strs[si];

            int* count = stackalloc int[26 * 2];

            for (int ci = 0; ci < line.Length; ci++)
            {
                int priority = GetPriority(line[ci]);

                count[priority - 1] += 1;
            }

            for (int i = 0; i < length; i++)
            {
                if (count[i] > 0)
                {
                    totalCount[i]++;
                }
            }
        }

        for (int ii = 0; ii < length; ii++)
        {
            if (totalCount[ii] == strs.Length)
            {
                return ii + 1;
            }
        }

        return 0;
    }

}

