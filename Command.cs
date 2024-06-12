namespace IRC;

public abstract class Command(StreamReader reader, StreamWriter writer, IrcLine ircLine)
{
    public abstract string Cmd { get; }
    public abstract void Run();
}