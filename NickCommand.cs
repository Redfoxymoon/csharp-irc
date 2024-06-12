namespace IRC;

public class NickCommand : ICommand
{
    public string Cmd => "setnick";

    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        if(ircLine.arguments != "")
            Msg.SendRawMsg(writer, "NICK " + ircLine.arguments);
    }
}