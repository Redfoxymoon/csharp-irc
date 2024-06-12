namespace IRC;

public class PingCommand() : ICommand
{
    public string Cmd => "";
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        Msg.SendRawMsg(writer,"PONG " + ircLine.type);
    }
}