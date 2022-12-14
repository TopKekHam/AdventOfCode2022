public class Day12 : DayBase
{
    public Day12() : base(12, @"")
    {
        if (true)
        {
            _shortInput = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";
        }
    }

    struct Index
    {
        public int x, y;

        public Index(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Index i1, Index i2)
        {
            return i1.x == i2.x && i1.y == i2.y;
        }

        public static bool operator !=(Index i1, Index i2)
        {
            return i1.x != i2.x || i1.y != i2.y;
        }
    }

    private char[,] graph;
    private int graphWidth;
    private int graphHeight;
    private int[,] distance;


    public override void Run1()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        var lines = _input.Split("\r\n");

        graphWidth = lines[0].Length;
        graphHeight = lines.Length;

        graph = new char[graphWidth, graphHeight];
        distance = new int[graphWidth, graphHeight];

        Index start = new Index();
        Index end = new Index();

        for (int y = 0; y < graphHeight; y++)
        {
            for (int x = 0; x < graphWidth; x++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write(lines[y][x]);

                distance[x, y] = -1;
                graph[x, y] = lines[y][x];

                if (graph[x, y] == 'S')
                {
                    start.x = x;
                    start.y = y;
                    graph[x, y] = 'a';
                }

                if (graph[x, y] == 'E')
                {
                    end.x = x;
                    end.y = y;
                    graph[x, y] = 'z';
                }
            }
        }

        Queue<Index> nodesToVisit = new Queue<Index>();
        nodesToVisit.Enqueue(start);

        distance[start.x, start.y] = 0;

        Console.ForegroundColor = ConsoleColor.Red;

        while (nodesToVisit.Count > 0)
        {
            var node = nodesToVisit.Dequeue();

            if (node == end) continue;

            Walk(node, 1, 0, nodesToVisit);
            Walk(node, -1, 0, nodesToVisit);
            Walk(node, 0, 1, nodesToVisit);
            Walk(node, 0, -1, nodesToVisit);
        }

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine();
        Console.WriteLine(distance[end.x, end.y]);
        Console.ReadKey();
    }

    void Walk(Index node, int x, int y, Queue<Index> nodesToVisit)
    {
        char height = graph[node.x, node.y];

        Index next = node;
        next.x += x;
        next.y += y;

        if (CanWalk(height, next.x, next.y))
        {
            int nodeDistance = distance[node.x, node.y];

            bool needToUpdate = distance[next.x, next.y] == -1 || distance[next.x, next.y] > nodeDistance + 1;

            Console.CursorLeft = next.x;
            Console.CursorTop = next.y;
            Console.Write(graph[next.x, next.y]);

            if (needToUpdate)
            {
                char c = x == 1 ? '>' : x == -1 ? '<' : y == 1 ? 'v' : '^';
                Console.CursorLeft = node.x;
                Console.CursorTop = node.y;
                Console.Write(c);

                distance[next.x, next.y] = nodeDistance + 1;
                nodesToVisit.Enqueue(next);
            }
        }
    }

    bool CanWalk(char currentHeight, int x, int y)
    {
        bool insideRange = (x >= 0 && x < graphWidth && y >= 0 && y < graphHeight);
        if (!insideRange) return false;

        char next = graph[x, y];

        return (next - currentHeight <= 1);
    }

    public override void Run2()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        var lines = _input.Split("\r\n");

        graphWidth = lines[0].Length;
        graphHeight = lines.Length;

        graph = new char[graphWidth, graphHeight];
        distance = new int[graphWidth, graphHeight];

        Index end = new Index();

        for (int y = 0; y < graphHeight; y++)
        {
            for (int x = 0; x < graphWidth; x++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write(lines[y][x]);

                distance[x, y] = -1;
                graph[x, y] = lines[y][x];

                if (graph[x, y] == 'S')
                {
                    graph[x, y] = 'a';
                }

                if (graph[x, y] == 'E')
                {
                    end.x = x;
                    end.y = y;
                    graph[x, y] = 'z';
                }
            }
        }

        Queue<Index> nodesToVisit = new Queue<Index>();
        nodesToVisit.Enqueue(end);

        distance[end.x, end.y] = 0;

        Console.ForegroundColor = ConsoleColor.Red;

        while (nodesToVisit.Count > 0)
        {
            var node = nodesToVisit.Dequeue();

            Walk2(node, 1, 0, nodesToVisit);
            Walk2(node, -1, 0, nodesToVisit);
            Walk2(node, 0, 1, nodesToVisit);
            Walk2(node, 0, -1, nodesToVisit);
        }

        int minDistance = int.MaxValue;

        for (int y = 0; y < graphHeight; y++)
        {
            for (int x = 0; x < graphWidth; x++)
            {
                if (graph[x, y] == 'a' && distance[x, y] > 0)
                {
                    if (distance[x, y] < minDistance)
                    {
                        minDistance = distance[x, y];
                    }
                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine();
        Console.WriteLine(minDistance);
        Console.ReadKey();
    }

    bool CanWalk2(Index node, Index next)
    {
        bool insideRange = (next.x >= 0 && next.x < graphWidth && next.y >= 0 && next.y < graphHeight);
        if (!insideRange) return false;

        char height = graph[next.x, next.y];
        char currentHeight = graph[node.x, node.y];

        return (currentHeight - height <= 1);
    }
    
    void Walk2(Index node, int x, int y, Queue<Index> nodesToVisit)
    {

        Index next = node;
        next.x += x;
        next.y += y;

        if (CanWalk2(node, next))
        {
            int nodeDistance = distance[node.x, node.y];

            bool needToUpdate = distance[next.x, next.y] == -1 || distance[next.x, next.y] > nodeDistance + 1;

            Console.CursorLeft = next.x;
            Console.CursorTop = next.y;
            Console.Write(graph[next.x, next.y]);

            if (needToUpdate)
            {
                char c = x == 1 ? '>' : x == -1 ? '<' : y == 1 ? 'v' : '^';
                Console.CursorLeft = node.x;
                Console.CursorTop = node.y;
                Console.Write(c);

                distance[next.x, next.y] = nodeDistance + 1;
                nodesToVisit.Enqueue(next);
            }
        }
    }
}