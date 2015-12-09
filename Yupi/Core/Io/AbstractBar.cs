namespace Yupi.Core.Io
{
    public abstract class AbstractBar
    {
        /// <summary>
        /// Prints a simple message
        /// </summary>
        /// <param name="msg">Message to print</param>
        public void PrintMessage(string msg)
        {
            System.Console.Write("  {0}", msg);
            System.Console.Write("\r".PadLeft(System.Console.WindowWidth - System.Console.CursorLeft - 1));
        }

        public abstract void Step();
    }
}