using System.Collections;
using System.Text;

public class Day8
{
    private static string shortInput = @"30373
25512
65332
33549
35390";

    public struct Tallest
    {
        public int Row;
        public int Column;
        public int Length;
        public bool TallestInLine;

        public Tallest(int row, int column, int length)
        {
            Row = row;
            Column = column;
            Length = length;
            TallestInLine = true;
        }

        public void Exchange(Tallest other)
        {
            if (other.Length > Length)
            {
                this.Row = other.Row;
                this.Column = other.Column;
                this.Length = other.Length;
                this.TallestInLine = true;
            }
            else if (other.Length == Length)
            {
                this.TallestInLine = false;
            }
        }
    }

    public static void Run1()
    {
        string[] lines = File.ReadAllText("Day8.txt").Split("\r\n");

        int width = lines[0].Length;
        int height = lines.Length;
        
        Dictionary<int, int> dict = new Dictionary<int, int>();

        for (int y = 0; y < height; y++)
        {
            int max = -1;
            
            for (int x = 0; x < width; x++)
            {
                int length = lines[y][x] - '0';
                
                if (max < length)
                {
                    TryAdd(dict, x, y, length);
                    max = length;
                }
            }
            
            max = -1;
            
            for (int x = width - 1; x >= 0; x--)
            {
                int length = lines[y][x] - '0';
                
                if (max < length)
                {
                    TryAdd(dict, x, y, length);
                    max = length;
                }
            }
        }
        
        for (int x = 0; x < width; x++)
        {
            int max = -1;
            
            for (int y = 0; y < height; y++)
            {
                int length = lines[y][x] - '0';
                
                if (max < length)
                {
                    TryAdd(dict, x, y, length);
                    max = length;
                }
            }
            
            max = -1;
            
            for (int y = height - 1; y >= 0; y--)
            {
                int length = lines[y][x] - '0';
                
                if (max < length)
                {
                    TryAdd(dict, x, y, length);
                    max = length;
                }
            }
        }

        Console.WriteLine(dict.Count);
    }

    static void TryAdd(Dictionary<int, int> dict, int x, int y, int height)
    {
        int idx = x + (y << 8);
        dict.TryAdd(idx, height);
    }
    
    public static void Run2()
    {
        
        string[] lines = File.ReadAllText("Day8.txt").Split("\r\n");
        
        int width = lines[0].Length;
        int height = lines.Length;

        int max = 0;
        
        for (int ty = 0; ty < height; ty++)
        {
            for (int tx = 0; tx < width; tx++)
            {

                char length = lines[ty][tx];
                int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
                
                for (int x = tx - 1; x >= 0; x--)
                {
                    x1 += 1;
                    
                    if (lines[ty][x] >= length)
                    {
                        break;
                    }
                }

                for (int x = tx + 1; x < width; x++)
                {
                    x2 += 1;
                    
                    if (lines[ty][x] >= length)
                    {
                        break;
                    }
                }
                
                for (int y = ty - 1; y >= 0; y--)
                {
                    y1 += 1;
                    
                    if (lines[y][tx] >= length)
                    {
                        break;
                    }
                }

                for (int y = ty + 1; y < height; y++)
                {
                    y2 += 1;
                    
                    if (lines[y][tx] >= length)
                    {
                        break;
                    }
                }

                int sum = x1 * x2 * y1 * y2;

                if (max < sum)
                {
                    max = sum;
                }
            }
        }

        Console.WriteLine(max);
    }
}