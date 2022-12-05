
class Day2
{

    static int Rock = 0;
    static int Paper = 1;
    static int Scissors = 2;

    static int[] scores = new int[] { 1, 2, 3 };

    static int Lose = 0;
    static int Draw = 1;
    static int Win = 2;

    public static void Run1()
    {
        string[] input = File.ReadAllLines("Day2.txt");

        int score = 0;

        for (int i = 0; i < input.Length; i++)
        {

            int opp = input[i][0] - 'A';

            int self = input[i][2] - 'X';

            score += scores[self];

            if (opp == self)
            {
                score += 3;
            }
            else
            {
                bool won = (opp == Rock && self == Paper) ||
                           (opp == Paper && self == Scissors) ||
                           (opp == Scissors && self == Rock);

                score += won ? 6 : 0;
            }

        }

        Console.WriteLine(score);
    }

    public static void Run2()
    {
        string[] input = File.ReadAllLines("Day2.txt");

        int score = 0;

        for (int i = 0; i < input.Length; i++)
        {

            int opp = input[i][0] - 'A';

            int self = input[i][2] - 'X';

            score += self == Lose ? 0 : self == Draw ? 3 : 6;

            int play = 0;

            if (self == Win)
            {
                play = (opp + 1) % 3;
            }
            else if (self == Draw)
            {
                play = opp;
            }
            else if (self == Lose)
            {
                play = opp - 1;

                if (play == -1) play = 2;
            }

            score += scores[play];
        }

        Console.WriteLine(score);
    }

}

