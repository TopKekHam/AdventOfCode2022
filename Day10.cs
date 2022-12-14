
public class Day10 : DayBase
{
    public Day10() : base(10, "")
    {
        if(true) {
            _shortInput = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";
        }
    }

    struct PC
    {
        public int RegX;
        public int Cycles;
        public int Sum;

        public PC()
        {
            RegX = 1;
            Cycles = 0;
            Sum = 0;
        }

        public void Noop()
        {
            Cycle();
        }

        public void AddX(int x)
        {
            Cycle();
            Cycle();
            RegX += x;
        }

        void Cycle()
        {
            Cycles += 1;
            
            if (Cycles == 20 ||
                ((Cycles - 20) % 40 == 0 && Cycles > 20 && Cycles <= 220))
            {
                Sum += (Cycles * RegX);
            }
            
            Rasterize();
        }

        void Rasterize()
        {
            int pos = (Cycles - 1) % 40;
            int sprite = RegX - 1;
            
            if (pos == 0)
            {
                Console.WriteLine();
            }

            char c = pos >= sprite && pos < sprite + 3 ? '#' : '.';
            Console.Write(c);
        }
    }

    public override void Run1()
    {
        PC pc = new PC();

        string[] lines = _input.Split("\r\n");

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("addx"))
            {
                int x = int.Parse(lines[i].Substring(5));
                pc.AddX(x);
            }
            else
            {
                pc.Noop();
            }
        }
        
        Console.WriteLine();
        Console.WriteLine(pc.Sum);
    }

    public override void Run2()
    {
    }
}