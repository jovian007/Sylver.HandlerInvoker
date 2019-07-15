namespace HandlerInvoker.App.Models
{
    public interface ICustomInterface
    {
        void DoSomething(int integer);
    }

    public class CustomObject : ICustomInterface
    {
        public int IntegerValue { get; private set; }

        public void DoSomething(int integer)
        {
            this.IntegerValue = integer;
        }
    }
}
