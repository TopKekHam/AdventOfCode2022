
public abstract class DayBase
{
    protected string _shortInput;
    protected string _input;
    
    public DayBase(int number, string shortInput)
    {
        _input = File.ReadAllText($"Day{number}.txt");
        _shortInput = shortInput;
    }

    public abstract void Run1();
    public abstract void Run2();
}