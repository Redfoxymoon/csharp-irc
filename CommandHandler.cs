using System.Threading.Channels;

namespace IRC;

public class CommandHandler(StreamReader reader, StreamWriter writer, string channel, string nick)
{
    private const char Trigger = '.';
    private List<CmdObject> _cmds = [];
    
    /*private Command _ntStatusCommand = new NtStatusCommand(reader, writer);
    private Command _helloCommand = new HelloCommand(reader, writer);*/
    //private Command _pingCommand = new PingCommand(reader, writer, nick, channel);
    /*private Command _joinCommand = new JoinCommand(reader, writer);
    private Command _nickCommand = new NickCommand(reader, writer);*/

    public void RegisterCmd(CmdObject cmd)
    {
        _cmds.Add(cmd);
    }
    public void Invoke(string inputLine)
    {
        try
        {
#if !DEBUG
            if(!inputLine.StartsWith("PING"))
#endif
            Console.WriteLine(inputLine);
            var ircPieces = inputLine.Split(' ');
            var isPrivmsg = inputLine.Contains("PRIVMSG");
            var ircMessageIndex = inputLine.IndexOf(" :", StringComparison.Ordinal);
            var ircMessage = "";
            var senderNick = "";
            IrcLine ircLine;
            if (ircPieces[0] == "PING")
            {
                ircLine = new IrcLine(ircPieces[0], ircPieces[1]);
                Task.Run(() => new PingCommand().Run(reader, writer, ircLine));
            }
            else if (ircPieces[1] == "001")
            {
                ircLine = new IrcLine(channel);
                Task.Run(() => new JoinCommand().Run(reader, writer, ircLine));
            }
            else if (ircMessageIndex != -1 && isPrivmsg)
            {
                senderNick = inputLine.Split('!')[0].Substring(1);
                ircMessage = inputLine.Substring(ircMessageIndex + 2);
                if (ircMessage.StartsWith(nick + ",") || ircMessage.StartsWith(nick + ":"))
                {
                    ircLine = new IrcLine("", senderNick, "", channel, "", "", "");
                    Task.Run(() => new HelloCommand().Run(reader, writer, ircLine));
                }
                else
                {
                    foreach (var cmd in _cmds)
                    {
                        if (ircMessage.StartsWith(Trigger + cmd.Cmd))
                        {
                            string arguments;
                            try
                            {
                                arguments = ircMessage.Split(' ')[1];
                            }
                            catch
                            {
                                arguments = "";
                            }

                            ircLine = new IrcLine(ircPieces[0], senderNick, ircPieces[1], ircPieces[2], cmd.Cmd,
                                arguments, nick);
                            Console.WriteLine($"executing {cmd.Command.Cmd}!");
                            Task.Run(() => cmd.Command.Run(reader, writer, ircLine));
                        }
                    }
                }
            }
            
            /*if (ircPieces[0] == "PING")
            {
                var newPing = new PingCommand(reader, writer);
                var pingThread = new Thread(newPing.Run);
                pingThread.Start(ircPieces[1]);
            }*/

            /*if (ircPieces[1] == "001")
                _joinCommand.Run("");
            if (inputLine.Contains(nick + ",") || inputLine.Contains(nick + ":"))
                _helloCommand.Run(ircPieces[0]);
            if (ircPieces[3].StartsWith(":" + Trigger + _joinCommand.Cmd))
                _joinCommand.Run(ircPieces[4]);
            if (ircPieces[3].StartsWith(":" + Trigger + _ntStatusCommand.Cmd))
                _ntStatusCommand.Run(ircPieces[4]);
            if (ircPieces[3].StartsWith(":" + Trigger + _nickCommand.Cmd))
                _nickCommand.Run(ircPieces[4]);*/
        }
        catch (IndexOutOfRangeException e) // :-)
        {}
    }
}