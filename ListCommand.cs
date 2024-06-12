namespace IRC;

//public class ListCommand(List<ICommand> cmds, string nick) : ICommand
 public class ListCommand(string[] array) : ICommand
{
    public string Cmd => "listcmds";
    
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        //var helpstr = "These are the available commands: " + CmdsToStr(cmds, nick);
        //Console.WriteLine(helpstr);
        Console.WriteLine(ircLine.channel);
        Msg.SendPrivMsg(writer, ircLine.channel, "helpstr");
    }
}