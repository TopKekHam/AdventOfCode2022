using System.Collections;
using System.Text;

public class Day7
{
    public class DirFile
    {
        public string Name { get; private set; }
        public int Size { get; private set; }

        public DirFile(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }

    public class Dir
    {
        public string Name { get; private set; }
        public Dir? Parent { get; set; }
        public IReadOnlyList<Dir> Children => _children;

        private List<DirFile> _files;
        private List<Dir> _children;

        public Dir(string name)
        {
            Name = name;
            Parent = null;
            _files = new List<DirFile>();
            _children = new List<Dir>();
        }

        public void AddFile(DirFile file)
        {
            _files.Add(file);
        }

        public void AddChild(Dir dir)
        {
            dir.Parent = this;
            _children.Add(dir);
        }
        
        public int GetTotalSize()
        {
            int sum = 0;

            for (int i = 0; i < _files.Count; i++)
            {
                sum += _files[i].Size;
            }

            for (int i = 0; i < _children.Count; i++)
            {
                sum += _children[i].GetTotalSize();
            }

            return sum;
        }

        public Dir GetChild(string name)
        {
            foreach (Dir child in _children)
            {
                if (child.Name == name) return child;
            }

            throw new Exception("Error");
        }

        public void PrintContent(int pad = 0)
        {
            Console.WriteLine($"{Pad(pad)}{Name}: ");
            
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].PrintContent(pad + 1);
            }

            string padStr = Pad(pad + 1);
            
            for (int i = 0; i < _files.Count; i++)
            {
                Console.WriteLine($"{padStr}{_files[i].Name} {_files[i].Size}");
            }
        }

        static string Pad(int count)
        {
            string str = "";
            
            for (int i = 0; i < count; i++)
            {
                str += "  ";
            }

            return str;
        }
    }

    private static string shortInput = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    public static void Run1()
    {

        Dir root = BuildTree(File.ReadAllText("Day7.txt"));
        int sum = Sum(root, 100000);
        
        Console.WriteLine(sum);
    }

    public static void Run2()
    {
        Dir root = BuildTree(File.ReadAllText("Day7.txt"));
        
        int updateSize = 30000000;
        int spaceNeeded = updateSize - (70000000 - root.GetTotalSize());
        int smallest = FindSmallest(root, spaceNeeded);
        
        Console.WriteLine(smallest);
    }

    static Dir BuildTree(string input)
    {
        string[] lines = input.Split("\r\n");
        
        Dir root = new Dir("/");
        Dir currentDir = root;
        
        for (int i = 0; i < lines.Length; i++)
        {
            string[] args = lines[i].Split(' ');

            if (args[0] == "$")
            {
                if (args[1] == "cd")
                {
                    if (args[2] == "/")
                    {
                        currentDir = root;
                    }
                    else if (args[2] == "..")
                    {
                        currentDir = currentDir.Parent;
                    }
                    else
                    {
                        currentDir = currentDir.GetChild(args[2]);
                    }
                }
                else if (args[1] == "ls")
                {
                    // do nothing
                }
            }
            else
            {
                if (args[0] == "dir")
                {
                    currentDir.AddChild(new Dir(args[1]));
                }
                else
                {
                    currentDir.AddFile(new DirFile(args[1], int.Parse(args[0])));
                }
            }
        }

        return root;
    }
    
    static int Sum(Dir dir, int maxSize)
    {
        int size = dir.GetTotalSize();
        int sum = 0;
        
        if (size < maxSize)
        {
            sum += size;
        }
        
        foreach (Dir child in dir.Children)
        {
            sum += Sum(child, maxSize);
        }
        
        return sum;
    }

    static int FindSmallest(Dir dir, int minSize)
    {
        int size = dir.GetTotalSize();

        if (minSize > size) return Int32.MaxValue;
        
        int currentClosest = size;

        foreach (var child in dir.Children)
        {
            int smallest = FindSmallest(child, minSize);

            if (smallest < currentClosest) currentClosest = smallest;
        }

        return currentClosest;
    }
    
}