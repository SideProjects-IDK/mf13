using static mf13.modules.logger;
using static mf13.modules.core.db_manager;
using mf13.modules.core;

namespace mf13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log($"Starting at: {DateTime.Now.ToString("dd/M/yyyy hh:mm.ss")}");

            // --- Logger Tests

            //Log("Hi, Guys!");
            //Log_MsgR($"user{i}",$"This is {i} test message.");
            //Log_CmdR($"user{i}",$"ls -l -a ./dirs/{i};\nls -la ../",$"./\n../\n./{i}.txt\n\n/\n/root");
            //Log_Command_Process($"user{i}", $"ls -l -a ./dirs/{i};\nls -la ../", $"./\n../\n./{i}.txt\n\n/\n/root",true);

            // ---

            // --- DB MANAGER TESTS ---

            //Log(db_manager.Init_And_Return_A_UUID("", ""));
            Log("");

            // ---
        }
    }
}
