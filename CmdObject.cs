namespace IRC;

public class CmdObject(ICommand command, string cmd)
{
    public ICommand Command = command;
    public string Cmd = cmd;
}