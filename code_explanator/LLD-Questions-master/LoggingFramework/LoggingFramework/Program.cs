namespace LoggingFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger logger = Logger.GetInstance();
            logger.Info(" i am info");
            logger.Debug("I am debugging");
            logger.Error("i am error");
        }
    }
}
