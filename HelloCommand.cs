namespace IRC;

public class HelloCommand : ICommand
{
    public string Cmd => "abc";

    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine)
    {
        Console.WriteLine("hello command" + ircLine.command);
        if (ircLine.senderNick == "midipix")
            Msg.SendPrivMsg(writer, ircLine.channel, (char)0x01 + "ACTION :-)" + (char)0x01);
        else
            Msg.SendPrivMsg(writer, ircLine.channel, $"{ircLine.senderNick}, hi!");
    }
}
