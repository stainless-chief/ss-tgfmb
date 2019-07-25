namespace FrontlineMaidBot.Interfaces
{
    public interface IDefaultMessages
    {
        string CantFind { get; }
        string Greeting { get; }
        string CantSlapMaster { get; }
        string CantSlapHerself { get; }
        string DontKnow { get; }
        string Suggestion { get; }
        string WrongCommand { get; }
        string ErrorHappens { get; }
        string WrongParams { get; }
    }
}
