using System;
using System.Windows.Input;

namespace AP.WordParser.Gui.ViewModels
{
    /// <summary>
    /// Responsible for handling user-initiated events in WPF
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;

        private Predicate<object> _canExecute;

        /// <summary>
        ///
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<object> execute)
            : this(execute, x => true)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        ///
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}