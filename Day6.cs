

public class Day6
{

    static string shortInput = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";

    public static void Run1()
    {
        string input = File.ReadAllText("Day6.txt").Trim();
        Console.WriteLine(Find(input, 4));
    }

    public static void Run2()
    {
        string input = File.ReadAllText("Day6.txt").Trim();
        Console.WriteLine(Find(input, 14));
    }

    static int Find(string input, int length)
    {
        int[] slots = new int[26];
        Queue<int> idxs = new Queue<int>();
        int seqLen = length;

        for (int i = 0; i < seqLen - 1; i++)
        {
            char c = input[i];
            slots[c - 'a']++;
            idxs.Enqueue(c - 'a');
        }

        for (int i = seqLen - 1; i < input.Length; i++)
        {
            char c = input[i];
            slots[c - 'a']++;
            idxs.Enqueue(c - 'a');

            while (idxs.Count > seqLen)
            {
                int dc = idxs.Dequeue();
                slots[dc]--;
            }

            int validItems = 0;

            foreach (var item in idxs)
            {
                if (slots[item] == 1)
                {
                    validItems++;
                }
            }

            if (validItems == seqLen)
            {
                return i + 1;
            }

        }

        return -1;
    }

}

