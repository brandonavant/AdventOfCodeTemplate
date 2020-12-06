namespace AdventOfCodeTemplate.Base.Rules
{
    public interface IRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
