using System.Windows;

namespace AP.WordParser.Gui.Utils
{
    /// <summary>
    /// Responsible for showing the user a message
    /// </summary>
    public interface IUserMessagingService
    {
        /// <summary>
        /// Displays a message to the user
        /// </summary>
        /// <param name="message"></param>
        void Message(string message);
    }

    /// <summary>
    /// Responsible for showing the user a messagebox
    /// </summary>
    public class UserMessagingService : IUserMessagingService
    {
        /// <summary>
        /// Displays a message to the user by using the <see cref="MessageBox"/>
        /// </summary>
        /// <param name="message"></param>
        public void Message(string message)
        {
            MessageBox.Show(message);
        }
    }
}