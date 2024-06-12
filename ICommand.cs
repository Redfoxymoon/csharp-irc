namespace IRC;

public interface ICommand
{
    public string Cmd { get; }
    public void Run(StreamReader reader, StreamWriter writer, IrcLine ircLine);
}