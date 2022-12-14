
public class Day11 : DayBase
{
    public Day11() : base(11, "")
    {
        if (true)
        {
            _shortInput = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
        }
    }

    interface IOperator
    {
        long Value { get; }
    }

    interface IOperand
    {
        long Value { get; }
    }

    class OperandConst : IOperand
    {
        public OperandConst(int value)
        {
            Value = value;
        }

        public long Value { get; private set; }
    }

    class OperandOld : IOperand
    {
        private readonly Monkey _monkey;

        public OperandOld(Monkey monkey)
        {
            _monkey = monkey;
        }

        public long Value
        {
            get => _monkey.HoldingItem;
        }
    }

    class OperatorPlus : IOperator
    {
        private IOperand _operandLeft, _operandRight;

        public OperatorPlus(IOperand operandLeft, IOperand operandRight)
        {
            _operandLeft = operandLeft;
            _operandRight = operandRight;
        }

        public long Value
        {
            get => _operandLeft.Value + _operandRight.Value;
        }
    }

    class OperatorMultiply : IOperator
    {
        private IOperand _operandLeft, _operandRight;

        public OperatorMultiply(IOperand operandLeft, IOperand operandRight)
        {
            _operandLeft = operandLeft;
            _operandRight = operandRight;
        }

        public long Value
        {
            get => _operandLeft.Value * _operandRight.Value;
        }
    }

    interface IWorryDetector
    {
        long CalcWorry(long current);
    }

    class DividedWorryDetector : IWorryDetector
    {
        private readonly long _divider;

        public DividedWorryDetector(long divider)
        {
            _divider = divider;
        }

        public long CalcWorry(long current)
        {
            return current / _divider;
        }
    }

    class IdentityWorryDetector : IWorryDetector
    {
        public long CalcWorry(long current)
        {
            return current;
        }
    }

    class DynamicWorryDetector : IWorryDetector
    {
        public long SubNumber = 0;

        public long CalcWorry(long current)
        {
            while (current > SubNumber)
            {
                current -= SubNumber;
            }

            return current;
        }
    }

    class Monkey
    {
        public IOperator Operator;
        public List<long> Items;
        public int TestNumber;
        public int MonkeyPassedTest;
        public int MonkeyFailedTest;
        public IWorryDetector WorryDetector;

        public long InspectedItems { get; private set; } = 0;

        public long HoldingItem
        {
            get => Items[0];
        }

        public int PassItemTo(out long itemValue)
        {
            InspectedItems++;
            itemValue = WorryDetector.CalcWorry(Operator.Value);
            Items.RemoveAt(0);
            return itemValue % TestNumber == 0 ? MonkeyPassedTest : MonkeyFailedTest;
        }

        public void AddItem(long item)
        {
            Items.Add(item);
        }
    }

    public override void Run1()
    {
        List<Monkey> monkeys = BuildList(new DividedWorryDetector(3), _shortInput);
        RunRounds(monkeys, 20);
    }

    public override void Run2()
    {
        var detector = new DynamicWorryDetector();
        List<Monkey> monkeys = BuildList(detector, _input);

        detector.SubNumber = 1;

        for (int i = 0; i < monkeys.Count; i++)
        {
            detector.SubNumber *= monkeys[i].TestNumber;
        }

        RunRounds(monkeys, 10_000);
    }

    static void RunRounds(List<Monkey> monkeys, int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            if (i % 100 == 0) Console.WriteLine(i);

            for (int j = 0; j < monkeys.Count; j++)
            {
                var monkey = monkeys[j];

                while (monkey.Items.Count > 0)
                {
                    int monkeyIdx = monkey.PassItemTo(out long item);
                    monkeys[monkeyIdx].AddItem(item);
                }
            }
        }

        List<long> list = new List<long>();

        for (int i = 0; i < monkeys.Count; i++)
        {
            list.Add(monkeys[i].InspectedItems);
        }

        list.Sort();

        Console.WriteLine(
            $"{list[^1]} * {list[^2]} = {list[^1] * list[^2]}");
    }

    static List<Monkey> BuildList(IWorryDetector detector, string input)
    {
        string[] lines = input.Split("\r\n");

        int lineIdx = 0;

        List<Monkey> monkeys = new List<Monkey>();

        while (lineIdx < lines.Length)
        {
            lineIdx++;

            Monkey monkey = new Monkey();
            monkey.WorryDetector = detector;
            monkeys.Add(monkey);

            // items
            string[] itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.Items = new List<long>();
            for (int i = 2; i < itemsToParse.Length; i++)
            {
                string line = itemsToParse[i];
                if (line[^1] == ',') line = line.Substring(0, line.Length - 1);
                monkey.Items.Add(int.Parse(line));
            }

            // operator
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");

            IOperand left, right;

            if (itemsToParse[^3] == "old") left = new OperandOld(monkey);
            else left = new OperandConst(int.Parse(itemsToParse[^3]));

            if (itemsToParse[^1] == "old") right = new OperandOld(monkey);
            else right = new OperandConst(int.Parse(itemsToParse[^1]));

            if (itemsToParse[^2] == "+") monkey.Operator = new OperatorPlus(left, right);
            else monkey.Operator = new OperatorMultiply(left, right);

            // test
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.TestNumber = int.Parse(itemsToParse[^1]);

            // test true
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.MonkeyPassedTest = int.Parse(itemsToParse[^1]);

            // test false
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.MonkeyFailedTest = int.Parse(itemsToParse[^1]);

            lineIdx += 2;
        }

        return monkeys;
    }
}

public class Day11Fast : DayBase
{
    public Day11Fast() : base(11, "")
    {
        if (true)
        {
            _shortInput = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
        }
    }


    enum Operator
    {
        PLUS,
        MULTIPLY
    }

    struct Monkey
    {
        public int itemListIndex;
        public int itemsInList;
        public Operator oprator;
        public int inspected;
        public int oparend; // if -1 oparend is 'old'
        public int testNumber;
        public int testTrue;
        public int testFalse;
    }

    struct LoopedIdx
    {
        public int idx;
        public int stride;

        public LoopedIdx(int stride)
        {
            idx = 0;
            this.stride = stride;
        }

        public int GetNext()
        {
            int temp = idx;

            idx++;

            if (idx >= stride)
            {
                idx = 0;
            }

            return temp;
        }
    }

    public override void Run1()
    {
        Monkey[] monkeys = BuildList(_input, out int[] items, out int stride);
        Console.WriteLine(RunRounds(monkeys, items, stride, 20, true));
    }

    public override void Run2()
    {
        Monkey[] monkeys = BuildList(_input, out int[] items, out int stride);
        Console.WriteLine(RunRounds(monkeys, items, stride, 10_000, false));
    }

    static long RunRounds(Monkey[] monkeys, int[] items, int stride, int rounds, bool divide)
    {
        int subtractor = 1;

        for (int i = 0; i < monkeys.Length; i++)
        {
            subtractor *= monkeys[i].testNumber;
        }
        
        for (int round = 0; round < rounds; round++)
        {
            for (int i = 0; i < monkeys.Length; i++)
            {
                int offset = i * stride;
                int itemIdx = 0;

                while (monkeys[i].itemsInList > 0)
                {
                    monkeys[i].itemsInList--;
                    monkeys[i].inspected++;

                    int item = items[offset + itemIdx];
                    int number = monkeys[i].oparend == -1 ? item : monkeys[i].oparend;

                    item = monkeys[i].oprator == Operator.PLUS ? item += number : item *= number;

                    if (divide)
                    {
                        item /= 3;
                    }

                    while (item > subtractor)
                    {
                        item -= subtractor;
                    }

                    int nextMonkey = item % monkeys[i].testNumber == 0 ? monkeys[i].testTrue : monkeys[i].testFalse;

                    items[(nextMonkey * stride) + monkeys[nextMonkey].itemsInList] = item;
                    monkeys[nextMonkey].itemsInList++;

                    itemIdx++;
                }
            }
        }

        var numbers = monkeys.Select(m => m.inspected).ToList();
        numbers.Sort();
        
        Console.WriteLine($"{numbers[^1]} * {numbers[^2]}");
        
        return (long)numbers[^1] * (long)numbers[^2];
    }

    static Monkey[] BuildList(string input, out int[] monkeyItemList, out int stride)
    {
        string[] lines = input.Split("\r\n");

        int lineIdx = 0;

        List<Monkey> monkeys = new List<Monkey>();
        List<List<int>> startItems = new List<List<int>>();
        stride = 0;
        int monkeyIndex = -1;
        while (lineIdx < lines.Length)
        {
            monkeyIndex++;

            lineIdx++;

            Monkey monkey = new Monkey();


            // items
            string[] itemsToParse = lines[lineIdx].Trim().Split(" ");
            startItems.Add(new List<int>());
            for (int i = 2; i < itemsToParse.Length; i++)
            {
                string line = itemsToParse[i];
                if (line[^1] == ',') line = line.Substring(0, line.Length - 1);
                startItems[monkeyIndex].Add(int.Parse(line));
            }

            monkey.itemsInList = startItems[monkeyIndex].Count;
            stride += startItems.Count;

            // operator
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");

            if (itemsToParse[^1] == "old") monkey.oparend = -1;
            else monkey.oparend = int.Parse(itemsToParse[^1]);

            if (itemsToParse[^2] == "+") monkey.oprator = Operator.PLUS;
            else monkey.oprator = Operator.MULTIPLY;

            // test
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.testNumber = int.Parse(itemsToParse[^1]);

            // test true
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.testTrue = int.Parse(itemsToParse[^1]);

            // test false
            lineIdx++;
            itemsToParse = lines[lineIdx].Trim().Split(" ");
            monkey.testFalse = int.Parse(itemsToParse[^1]);

            monkeys.Add(monkey);
            lineIdx += 2;
        }

        monkeyItemList = new int[stride * monkeys.Count];

        for (int i = 0; i < startItems.Count; i++)
        {
            for (int j = 0; j < startItems[i].Count; j++)
            {
                monkeyItemList[(i * stride) + j] = startItems[i][j];
            }
        }

        var monkeyArr = monkeys.ToArray();

        return monkeyArr;
    }
}