using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Podcatcher
{
    static class Program
    {
        #region ExceptionHandling1of4
        /// <summary>
        /// The registered event source to use for Exceptions generated in this application. 
        /// See 
        /// You must register this event log source in the registry. You can do this with WiX:
        /// &lt;util:EventSource Log="Application" Name="Exception-Test" EventMessageFile="C:\Windows\Microsoft.NET\Framework\v4.0.30319\EventLogMessages.dll"/>
        /// You can find the event sources at HKLM\System\CurrentControlSet\Services\EventLog
        /// </summary>
        private const string EVENT_LOG_SOURCE = "Podcatcher"; // <======= *** CHANGE ME!!!!******
        /// <summary>
        /// Unhandled error on the UI thread. Don't define
        /// any errors with this.
        /// </summary>
        private const Int32 EVENTID_UITHREADEXCEPTION = 1000;
        /// <summary>
        /// Unhandled error on any thread that isn't the UI thread.
        /// </summary>
        private const Int32 EVENTID_UNHANDLEDEXCEPTION = 1001;
        /// <summary>
        /// Handled error somewhere in my application that I'm sending to Log.
        /// </summary>
        public const Int32 EVENTID_APPLICATION = 1002;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        #region ExceptionHandling2of4
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain)]
        #endregion
        static void Main()
        {
            #region ExceptionHandling3of4
            // EXCEPTION HANDLING FOR WINDOWS FORMS
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UIThreadException);
            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
               new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            #endregion

            // Upgrade settings from previous installed versions.
            UpgradeSettings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }


        #region ExceptionHandling4of4
        /// <summary>
        /// All UI thread Exceptions come here. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UIThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogError(e.Exception, EVENTID_UITHREADEXCEPTION, System.Diagnostics.EventLogEntryType.Error);
        }
        /// <summary>
        /// Logs the given error to the System log if running as installed. Requires a registered event handler.
        /// </summary>
        /// <param name="e"></param>
        public static void LogError(Exception e, Int32 eventID, System.Diagnostics.EventLogEntryType entryType)
        {
            bool sourceExists;
            try
            {
                if (System.Diagnostics.EventLog.SourceExists(EVENT_LOG_SOURCE))
                {
                    sourceExists = true;
                }
                else
                {
                    sourceExists = false;
                }
            }
            catch //(System.Security.SecurityException)
            {
                sourceExists = false;
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Windows.Forms.MessageBox.Show("Trying to log an error in LogError, but no source exists." +
                        Environment.NewLine + e.Message);
                }
            }
            try
            {
                if (sourceExists)
                {
                    System.Diagnostics.EventLog.WriteEntry(EVENT_LOG_SOURCE, e.ToFullDisplayString(), entryType, eventID);
                }
                else
                {
                    System.Diagnostics.Debug.Print("Failed to write to event log - probably doesn't exist in registry?");
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Windows.Forms.MessageBox.Show("Failed to log Exception." +
                        Environment.NewLine + ex.Message);
                }
            }
        }

        private static System.Collections.Generic.IEnumerable<Exception> GetNestedExceptionList(this Exception exception)
        {
            Exception current = exception;
            do
            {
                current = current.InnerException;
                if (current != null)
                    yield return current;
            }
            while (current != null);
        }

        private static string ToFullDisplayString(this Exception ex)
        {
            System.Text.StringBuilder displayText = new System.Text.StringBuilder();
            WriteExceptionDetail(displayText, ex);
            displayText.Append(Environment.NewLine);
            foreach (Exception inner in ex.GetNestedExceptionList())
            {
                displayText.AppendFormat("Inner Exception Start {0}", Environment.NewLine);
                WriteExceptionDetail(displayText, inner);
                displayText.AppendFormat("Inner Exception End {0}{0}", Environment.NewLine);
            }
            return displayText.ToString();
        }

        private static void WriteExceptionDetail(System.Text.StringBuilder builder, Exception ex)
        {
            builder.AppendFormat("Message: {0}", ex.Message, Environment.NewLine);
            builder.AppendFormat(" ", Environment.NewLine);
            builder.AppendFormat("Type: {0}{1}", ex.GetType(), Environment.NewLine);
            builder.AppendFormat("HelpLink: {0}{1}", ex.HelpLink, Environment.NewLine);
            builder.AppendFormat("Source: {0}{1}", ex.Source, Environment.NewLine);
            builder.AppendFormat("TargetSite: {0}{1}", ex.TargetSite, Environment.NewLine);
            builder.AppendFormat("Data: {0}", Environment.NewLine);
            foreach (System.Collections.DictionaryEntry de in ex.Data)
            {
                builder.AppendFormat("\t{0} : {1}", de.Key, de.Value);
            }
            builder.AppendFormat("StackTrace: {0}{1}", ex.StackTrace, Environment.NewLine);
        }

        /// <summary>
        /// Fires when a thread other than the UI thread has an unhandled exception, which WILL kill the whole
        /// application. IsTerminating 
        /// http://msdn.microsoft.com/en-us/library/system.unhandledexceptioneventargs.isterminating(v=vs.110).aspx 
        /// will only be true in .NET 1.0 or 1.1, where non-UI thread exceptions didn't kill the application: let's
        /// assume that is old tech and ignore it, so always call .Exit() for a clean exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            // Doesn't matter what you do, the application will terminate.
            try
            {
                // Will always be an Exception object in C#, I think. 
                LogError((Exception)e.ExceptionObject, EVENTID_UNHANDLEDEXCEPTION, System.Diagnostics.EventLogEntryType.Error);
            }
            catch
            {
                //MessageBox.Show("Unhandled Thread: " + ((Exception)e.ExceptionObject).Message);
            }
            // Calling .Exit appears to prevent the horrid Windows "had a problem, checking online" message.
            // That is, it exits cleanly. HOWEVER, this means that auto-restart doesn't operate!
            try
            {
                //Application.Exit();
            }
            catch
            { }

        }
        #endregion

        /// <summary>
        /// UpgradeSettings. This function migrates your application's settings from the previous
        /// version, if any, to this one. This is because Properties.Settings are saved to a 
        /// different user folder with every version, so unless you explicitly call this function
        /// then user settings will be lost with every upgrade.
        /// You must create a String setting called "LastVersionRun"
        /// Alasdair 11 June 2013
        /// </summary>
        public static void UpgradeSettings()
        {
            try
            {
                if (Properties.Settings.Default.LastVersionRun != Application.ProductVersion)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.Reload();
                    Properties.Settings.Default.LastVersionRun = Application.ProductVersion;
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {
                //MessageBox.Show("Error in UpgradeSettings. Have you created a property called \"LastVersionRun\"?");
            }
        }
        // End of UpgradeSettings()
    }
}
