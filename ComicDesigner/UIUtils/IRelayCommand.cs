using System.Windows.Input;

namespace ComicDesigner.UIUtils
{
    /// <summary>
    /// Support for commanding.
    /// </summary>
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        void RaiseCanExecuteChanged();

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute(object parameter);

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Execute(object parameter);
    }
}