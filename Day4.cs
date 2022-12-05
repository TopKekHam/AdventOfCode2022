 

public class Day4
{

    static string shortInput = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

    struct Ranges {
        public int[] nums; 

        public Ranges(IEnumerable<int> nums)
        {
            this.nums = nums.ToArray();
        }

        public bool AllOverlap()
        {
            return (InRange(nums[0], nums[1], nums[2]) && InRange(nums[0], nums[1], nums[3])) ||
                   (InRange(nums[2], nums[3], nums[0]) && InRange(nums[2], nums[3], nums[1]));
        }

        public bool SemiOverlap()
        {
            return InRange(nums[0], nums[1], nums[2]) || InRange(nums[0], nums[1], nums[3]) ||
                   InRange(nums[2], nums[3], nums[0]) || InRange(nums[2], nums[3], nums[1]);
        }

        bool InRange(int min, int max, int num)
        {
            return min <= num && max >= num;
        }
    }

    public static void Run1()
    {
        int count = File.ReadAllText("Day4.txt").Split("\r\n")
        .Select(str => new Ranges(str.Split('-', ',').Select(num => int.Parse(num))))
        .Count(range => range.AllOverlap());

        Console.WriteLine(count);
    }

    public static void Run2()
    {
        int count = File.ReadAllText("Day4.txt").Split("\r\n")
           .Select(str => new Ranges(str.Split('-', ',').Select(num => int.Parse(num))))
           .Count(range => range.SemiOverlap());

        Console.WriteLine(count);
    }

}

