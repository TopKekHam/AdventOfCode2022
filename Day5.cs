

public class Day5
{

    static string shortInput = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

    public static void Run1()
    {
        Solve(MoveRegular, File.ReadAllText("Day5.txt"));
    }

    public static void Run2()
    {
        Solve(MoveMultiple, File.ReadAllText("Day5.txt"));
    }

    static void MoveRegular(Stack<char> from, Stack<char> to, int count)
    {
        for (int i = 0; i < count; i++)
        {
            char c = from.Pop();
            to.Push(c);
        }
    }

    static Stack<char> mid = new Stack<char>();
    static void MoveMultiple(Stack<char> from, Stack<char> to, int count)
    {

        for (int i = 0; i < count; i++)
        {
            mid.Push(from.Pop());
        }

        for (int i = 0; i < count; i++)
        {
            to.Push(mid.Pop());
        }

    }

    static void Solve(Action<Stack<char>, Stack<char>, int> moveFunction, string input)
    {
        string[] lines = input.Split("\r\n");
        var stacks = BuildStacks(lines);

        int i = 0;
        for (; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) break;
        }

        i++;

        for (; i < lines.Length; i++)
        {

            string[] words = lines[i].Split(' ');

            int count = int.Parse(words[1]);
            int from = int.Parse(words[3]) - 1;
            int to = int.Parse(words[5]) - 1;

            moveFunction(stacks[from], stacks[to], count);
        }


        for (int ii = 0; ii < stacks.Count; ii++)
        {
            if (stacks[ii].TryPop(out char c))
                Console.Write(c);
        }

        Console.WriteLine();
    }

    static List<Stack<char>> BuildStacks(string[] lines)
    {

        List<List<char>> reversedStacks = new List<List<char>>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrEmpty(line))
            {
                break;
            }
            else
            {

                for (int ic = 0; (ic * 4) < line.Length; ic++)
                {
                    char c = line[(ic * 4) + 1];

                    if (char.IsLetter(c))
                    {
                        while (reversedStacks.Count <= ic)
                        {
                            reversedStacks.Add(new List<char>());
                        }

                        reversedStacks[ic].Add(c);
                    }
                }
            }
        }

        List<Stack<char>> stacks = new List<Stack<char>>();

        for (int i = 0; i < reversedStacks.Count; i++)
        {
            reversedStacks[i].Reverse();
            stacks.Add(new Stack<char>(reversedStacks[i]));
        }

        return stacks;
    }


}
