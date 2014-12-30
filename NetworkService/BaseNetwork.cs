using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService
{
    public abstract class BaseNetwork
    {
        private Process process;

        private static Process StartProcess(string command, string args, bool showWindow, bool redirectInput)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(command, args);
            p.StartInfo.RedirectStandardInput = redirectInput;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = !showWindow;
            p.ErrorDataReceived += (s, a) => Console.WriteLine(a.Data);
            p.Start();
            p.BeginErrorReadLine();

            return p;
        }

        protected void StopProcess()
        {
            if (IsProcessLaunched())
            {
                process.CloseMainWindow();
            }
        }

        protected bool IsProcessLaunched()
        {
            return process != null;
        }

        public void LaunchNodeProcess(string script, bool showWindows, bool redirectInput = true)
        {
            process = StartProcess("node.exe", script, showWindows, redirectInput);
        }

        public void LaunchProcess(string program, bool showWindows, bool redirectInput = true)
        {
            process = StartProcess(program, "", showWindows, redirectInput);
        }

        protected void SendMessage(string message)
        {
            if (process != null)
            {
                process.StandardInput.WriteLine(message);
            }
            else
            {
                throw new NullReferenceException("Bot is null. Please Launch a bot");
            }
        }

        protected void SendMessageAsync(string message)
        {
            if (process != null)
            {
                process.StandardInput.WriteLineAsync(message);
            }
            else
            {
                throw new NullReferenceException("Bot is null. Please Launch a bot");
            }
        }

        protected string ReceiveCommand()
        {
            string message = string.Empty;

            message = process.StandardOutput.ReadLine();

            return message;
        }

        protected Task<string> ReceiveCommandAsync()
        {
            return process.StandardOutput.ReadLineAsync();
        }
    }
}
