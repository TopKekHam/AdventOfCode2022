public class Day9
{
    private static string shortInput = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

    private static string input = File.ReadAllText("Day9.txt");

    struct Vec2I
    {
        public int x;
        public int y;

        public Vec2I()
        {
            x = 0;
            y = 0;
        }

        public override string ToString()
        {
            return $"{x}:{y}";
        }
    }

    public static void Run1()
    {
        string[] lines = input.Split("\r\n");

        Dictionary<Vec2I, int> poses = new Dictionary<Vec2I, int>();

        int knots = 10;
        Vec2I[] rope = new Vec2I[knots];
        for (int i = 0; i < rope.Length; i++) rope[i] = new Vec2I();

        poses.TryAdd(rope[^1], 1);

        for (int i = 0; i < lines.Length; i++)
        {
            char m = lines[i][0];
            int times = int.Parse(lines[i].Substring(2));

            int xm = m == 'R' ? 1 : (m == 'L' ? -1 : 0);
            int ym = m == 'U' ? 1 : (m == 'D' ? -1 : 0);

            for (int j = 0; j < times; j++)
            {
                Vec2I prev = rope[0];

                rope[0].x += xm;
                rope[0].y += ym;

                for (int r = 1; r < rope.Length; r++)
                {
                    if (Math.Abs(rope[r].x - rope[r - 1].x) > 1 || Math.Abs(rope[r].y - rope[r - 1].y) > 1)
                    {
                        (rope[r], prev) = (prev, rope[r]);

                        if (r == rope.Length - 1)
                        {
                            poses.TryAdd(rope[r], 1);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                DrawRope(rope, 5, 5);
                Console.ReadKey();
            }
        }

        Console.WriteLine(poses.Count);
    }

    static void DrawRope(Vec2I[] rope, int ox, int oy)
    {
        Console.Clear();
        
        for (int i = 0; i < rope.Length; i++)
        {
            Console.CursorLeft = rope[i].x + ox;
            Console.CursorTop = rope[i].y + ox;
            Console.Write(i);
        }
    }
    
    public static void Run2()
    {
        
    }
}