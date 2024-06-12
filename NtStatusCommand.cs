namespace IRC;

public class NtStatusCommand : ICommand
{
    public string Cmd => "ntstatus";
    private static readonly string[] _ntstatus = File.ReadAllLines("./ntstatus.txt");
    
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        foreach (var str in _ntstatus)
        {
            var line = str.Split(' ');
            if (line[0] == ircLine.arguments)
            {
#if DEBUG
                Console.WriteLine($"SendPrivMsg, {ircLine.channel}, {line[1]}");
#endif
                Msg.SendPrivMsg(writer, ircLine.channel,line[1]);
                return;
            }
        }
#if DEBUG
        Console.WriteLine($"SendPrivMsg, {ircLine.channel}, status code not found");
#endif
        Msg.SendPrivMsg(writer, ircLine.channel,"status code not found");
    }
}