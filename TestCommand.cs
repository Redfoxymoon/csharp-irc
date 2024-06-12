namespace IRC;

public class TestCommand : ICommand
{
    public string Cmd => "test";
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        Msg.SendPrivMsg(writer, ircLine.channel, "a major testing success! *explodes*");
    }
}