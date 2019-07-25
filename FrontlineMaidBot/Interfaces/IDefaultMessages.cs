namespace FrontlineMaidBot.Interfaces
{
    /// <summary> Default texts for generic messages </summary>
    public interface IDefaultMessages
    {
        /// <summary> When bot can't find anything </summary>
        string CantFind { get; }

        /// <summary> When user initiate a session</summary>
        string Greeting { get; }
        
        /// <summary> When bot don't know how to answer</summary>
        string DontKnow { get; }

        /// <summary> When there are many choices and bot give user suggestions </summary>
        string Suggestion { get; }

        /// <summary> When the command was wrong </summary>
        string WrongCommand { get; }

        /// <summary> When error happens </summary>
        string ErrorHappens { get; }

        /// <summary> When user input wrong parameters</summary>
        string WrongParams { get; }


        
    }
}
