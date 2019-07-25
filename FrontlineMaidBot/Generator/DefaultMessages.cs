using FrontlineMaidBot.Interfaces;

namespace FrontlineMaidBot.Generator
{
    public class DefaultMessages : IDefaultMessages
    {
        public string CantFind => "I'm sorry. I can't find anything.";

        public string Greeting => "Guten Tag, commander.";

        public string CantSlapMaster => "I am his bespoken and beloved maid. I will not commit an act of violence towards to him.";

        public string CantSlapHerself => "You are not a smart man, aren't you?";

        public string DontKnow => "I'm sorry. I don't know.";

        public string Suggestion => "I'm sorry... did you mean someone like:";

        public string WrongCommand => "I'm sorry. I'm afraid I can't do that.";

        public string ErrorHappens => "I'm not feeling very well... please, inform @ChiefNoir about that. He will take care of me.";

        public string WrongParams => "I'm sorry... what?";
    }
}
