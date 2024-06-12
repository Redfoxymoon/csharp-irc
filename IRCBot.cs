using System.Net.Sockets;

namespace IRC;

    public class IrcBot
    {
        private readonly string _server;
        private readonly int _port;
        private readonly string _user;
        private readonly string _nick;
        private readonly string _channel;

        private List<ICommand> cmds;
        private readonly int _maxRetries;

        public StreamReader reader;
        public StreamWriter writer;
        private CommandHandler _commandhandler;
        
        public IrcBot(string server, int port, string user, string nick, string channel, int maxRetries = 3)
        {
            _server = server;
            _port = port;
            _user = user;
            _nick = nick;
            _channel = channel;
            _maxRetries = maxRetries;
        }

        public void Start()
        {
            var retry = false;
            var retryCount = 0;
            
            do
            {
                try
                {
                    var irc = new TcpClient(_server, _port);
                    var stream = irc.GetStream();
                    reader = new StreamReader(stream);
                    writer = new StreamWriter(stream);
                    Msg.SendRawMsg(writer, "NICK " + _nick);
                    Msg.SendRawMsg(writer, _user);

                    Msg.SendPrivMsg(writer, "NickServ", "identify <snip>");
                    
                    _commandhandler = new CommandHandler(reader, writer, _channel, _nick);
 
                    _commandhandler.RegisterCmd(new CmdObject(new HelloCommand(), new HelloCommand().Cmd));
                    _commandhandler.RegisterCmd(new CmdObject(new NickCommand(), new NickCommand().Cmd));
                    _commandhandler.RegisterCmd(new CmdObject(new JoinCommand(), new JoinCommand().Cmd));
                    _commandhandler.RegisterCmd(new CmdObject(new NtStatusCommand(), new NtStatusCommand().Cmd));
                    _commandhandler.RegisterCmd(new CmdObject(new TestCommand(), new TestCommand().Cmd));

                   //var test = CmdsToArr(cmds, _nick);
                    //_commandhandler.RegisterCmd(new CmdObject(new ListCommand(test), new ListCommand(test).Cmd));
                    
                    while (true)
                    {
                        string inputLine;
                        while ((inputLine = reader.ReadLine()) != null)
                            _commandhandler.Invoke(inputLine);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Thread.Sleep(5000);
                    retry = ++retryCount <= _maxRetries;
                }
            } while (retry);
        }
        
        private string[] CmdsToArr(List<ICommand> cmds, string nick)
        {
            var strList = new List<string>();
            foreach(var str in cmds)
            {
                if (str.Cmd != nick || str.Cmd != "")
                    strList.Add(str.Cmd);
            }

            return strList.ToArray();
        }
    }
