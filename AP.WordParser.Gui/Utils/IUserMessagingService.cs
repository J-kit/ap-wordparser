using System.Windows;

namespace AP.WordParser.Gui.Utils
{
    public interface IUserMessagingService
    {
        void Message(string message);
    }

    public class UserMessagingService : IUserMessagingService
    {
        public void Message(string message)
        {
            MessageBox.Show(message);
        }
    }
}