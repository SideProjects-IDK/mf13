using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mf13.modules
{
    /// <summary>
    /// Avalable Commands:
    /// 
    ///  Log("Hi, Guys!");
    ///  Log_MsgR($"user{i}",$"This is {i} test message.");
    ///  Log_CmdR($"user{i}",$"ls -l -a ./dirs/{i};\nls -la ../",$"./\n../\n./{i}.txt\n\n/\n/root");
    ///  Log_Command_Process($"user{i}", $"ls -l -a ./dirs/{i};\nls -la ../", $"./\n../\n./{i}.txt\n\n/\n/root",true);
    ///
    /// </summary>
    public class logger
    {
        public static void Log(string Message)
        {
            Printf(ConsoleColor.White,$"\n[");
            Printf(ConsoleColor.Red, $"{DateTime.Now.ToString("hh:mm")}");
            Printf(ConsoleColor.White, $"]");

            Printf(ConsoleColor.Red,$" @System");
            Printf(ConsoleColor.White, $" {Message}");
        }
        public static void LogErr(string Message)
        {
            Printf(ConsoleColor.White, $"\n[");
            Printf(ConsoleColor.Red, $"{DateTime.Now.ToString("hh:mm")}");
            Printf(ConsoleColor.White, $"]");

            Printf(ConsoleColor.Red, $" @System -> Err");
            Printf(ConsoleColor.White, $" {Message}");
        }
        public static void Log_MsgR(string UserFrom,string Message)
        {
            Printf(ConsoleColor.Gray, $"\n<");
            Printf(ConsoleColor.Red, $"{UserFrom}");
            Printf(ConsoleColor.Gray, $">");
            Printf(ConsoleColor.White, $" {Message}");
        }
        public static void Log_Command_Process(string UserFrom, string Command, string Result, bool AccessGranted_Or_Not)
        {
            if (AccessGranted_Or_Not == true)
            {
                Log_ReqU(UserFrom, "Requests to run a command", "Okay");
                Log_CmdR(UserFrom, Command, Result);
            }
            else
                Log_ReqU(UserFrom, "Requests to run a command", "Absolutely Not");
        }
        public static void Log_CmdR(string UserFrom, string Command, string Reslt)
        {
            
            Printf(ConsoleColor.Red, $"\n cmd");
            Printf(ConsoleColor.Red, $"@ ");  

            string[] Command_Lines = Command.Split('\n');

            int x = 0;
            foreach (string Line in Command_Lines)
            {
                x++;
                if (x >= 2)
                    Printf(ConsoleColor.Red, $"\n    │ {Line}");
                else
                    Printf(ConsoleColor.Red, $"\n    │ {Line}");
            }
            
            string[] Res_Lines = Reslt.Split('\n');

            int y = 0;
            foreach (string Line in Res_Lines)
            {
                y++;
                if (y >= 2)
                    Printf(ConsoleColor.Green, $"\n    │ {Line}");
                else
                    Printf(ConsoleColor.Green, $"\n res@ {Line}");
            }

        }
        public static void Log_ReqU(string UserFrom, string Question, string Answer)
        {
            Printf(ConsoleColor.White, $"\n<");
            Printf(ConsoleColor.Red, $"{UserFrom}");
            Printf(ConsoleColor.White, $"> {Question}: {Answer}");
        }
        public static void Printf(ConsoleColor Clr, string Message)
        {
            Console.ForegroundColor = Clr;
            Console.Write(Message);
        }
    }
}
