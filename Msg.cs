namespace IRC;

public class Msg
{
    public static void SendRawMsg(StreamWriter writer, string message)
    {
        writer.WriteLine(message);
        writer.Flush();
    }

    public static void SendJoinMsg(StreamWriter writer, string channel)
    {
        writer.WriteLine("JOIN " + channel);
        writer.Flush();
    }
    
    public static void SendPrivMsg(StreamWriter writer, string channel, string message)
    {
        writer.WriteLine($"PRIVMSG {channel} :{message}");
        writer.Flush();
    }
}