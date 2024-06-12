namespace IRC;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Startup!");
        var ircBot = new IrcBot(
            server: "irc.libera.chat",
            port: 6667,
            user: "USER minired 0 * :minired",
            nick: "minired",
            channel: "#kemonomimi"
        );

        ircBot.Start();
    }
}