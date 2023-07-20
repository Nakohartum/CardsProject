namespace _Root.Test.Words
{
    public interface IHintable
    {
        bool Hinted { get; set; }
        bool Correct { get; set; }
        void ShowHint();
        void DisableHint();
    }
}