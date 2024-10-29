using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BytePatcherApp
{
    internal class Log
    {
        // Define a delegate and event for logging messages
        public delegate void LogEventHandler(string message, Color color);
        public static event LogEventHandler OnLog;

        public static void Error(string message)
        {
            RaiseLogEvent($"Error: {message}", Color.Red);
        }

        public static void Warning(string message)
        {
            RaiseLogEvent($"Warning: {message}", Color.Orange);
        }

        public static void Message(string message)
        {
            RaiseLogEvent($"Message: {message}", Color.Green);
        }

        // Raise the event with the message and specified color
        private static void RaiseLogEvent(string message, Color color)
        {
            OnLog?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}", color);
        }
    }
}
