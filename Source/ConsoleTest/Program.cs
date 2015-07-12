namespace ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (TestGame game = new TestGame())
            {
                game.Run();
            }
        }
    }
}