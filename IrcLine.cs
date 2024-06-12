namespace IRC;

public class IrcLine
{
    //:anthk_!~anthk_@texto-plano.xyz PRIVMSG #hyperbola :Haiku has nonfree bits
    public string sender;
    public string senderNick;
    public string type;
    public string channel;
    public string command;
    public string arguments;
    public string nick;

    public IrcLine(string sender, string senderNick, string type, string channel, string command, string arguments,
        string nick)
    {
        this.sender = sender;
        this.senderNick = senderNick;
        this.type = type;
        this.channel = channel;
        this.command = command;
        this.arguments = arguments;
        this.nick = nick;
    }
    
    public IrcLine(string ping, string pong)
    {
        sender = ping;
        type = pong;
    }
    
    public IrcLine(string channel)
    {
        this.channel = channel;
    }
}