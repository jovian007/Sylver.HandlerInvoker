using System;

namespace HandlerInvoker.App.Services
{
    /// <summary>
    /// Provides default methods.
    /// </summary>
    public interface IDefaultService
    {
        /// <summary>
        /// Prints a message to the console.
        /// </summary>
        /// <param name="message">Message to be printed.</param>
        void Print(string message);
    }

    public sealed class DefaultService : IDefaultService
    {
        /// <inheritdoc />
        public void Print(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Console.WriteLine(message);
        }
    }
}
