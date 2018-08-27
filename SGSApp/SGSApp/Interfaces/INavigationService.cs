using System;
using System.Threading.Tasks;

namespace SGSApp.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        ///     Returns true/false whether we can go backwards on the Nav Stack.
        /// </summary>
        /// <value><c>true</c> if can go back; otherwise, <c>false</c>.</value>
        bool CanGoBack { get; }

        /// <summary>
        ///     Event raised when NavigateAsync is used.
        /// </summary>
        event EventHandler Navigated;

        /// <summary>
        ///     Event raised when a GoBackAsync operation occurs.
        /// </summary>
        event EventHandler NavigatedBack;

        /// <summary>
        ///     Navigate to a page using the known key.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="pageKey">Page key.</param>
        /// <param name="viewModel">View model.</param>
        Task NavigateAsync(object pageKey, object viewModel = null);

        /// <summary>
        ///     Pops the last page off the stack and navigates to it.
        /// </summary>
        /// <returns>Async response</returns>
        Task GoBackAsync();

        /// <summary>
        ///     Push a page onto the modal stack.
        /// </summary>
        /// <returns>Async response</returns>
        /// <param name="pageKey">Page key.</param>
        /// <param name="viewModel">View model.</param>
        Task PushModalAsync(object pageKey, object viewModel = null);

        /// <summary>
        ///     Pops the last page off the modal stack
        /// </summary>
        /// <returns>Async response</returns>
        Task PopModalAsync();
    }
}