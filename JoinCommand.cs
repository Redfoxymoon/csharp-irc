namespace IRC;

public class JoinCommand : ICommand
{
    public string Cmd => "join";
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        if (ircLine.command != "" || ircLine.command == null)
            if(ircLine.arguments != "")
                Msg.SendJoinMsg(writer, ircLine.arguments);
            else
                Msg.SendPrivMsg(writer, ircLine.channel, "incorrect use of join");
        else
            Msg.SendJoinMsg(writer, "#kemonomimi");
    }
}