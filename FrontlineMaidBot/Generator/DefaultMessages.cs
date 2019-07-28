using FrontlineMaidBot.Interfaces;

namespace FrontlineMaidBot.Generator
{
    public class DefaultMessages : IDefaultMessages
    {
        public string CantFind => "I'm sorry. I can't find anything.";
        public string DontKnow => "I'm sorry. I don't know.";
        public string ErrorHappens => "I'm not feeling very well... please, inform @ChiefNoir about that. He will take care of me.";
        public string Greeting => "Guten Tag, commander.";
        public string Suggestion => "I'm sorry... did you mean someone like:";
        public string WrongCommand => "I'm sorry. I'm afraid I can't do that.";
        public string WrongParams => "I'm sorry... what?";
        public string EmptyParams => "You must write at least something, I can't work with nothingness.";
        public string EverythingWentGood => "Thank you for your cooperation! Your request has been submitted to my master.";
    }
}