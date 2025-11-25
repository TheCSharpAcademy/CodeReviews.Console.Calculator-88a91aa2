namespace calculator.schoffen;

public enum Messages
{
    Welcome,
    SelectOperation
}

public static class MessagesExtensions
{
    public static string ToDisplay(this Messages options)
    {
        return options switch
        {
            Messages.Welcome => "Welcome to C# console calculator!\nWhat would you like to do?",
            Messages.SelectOperation => "Select a operation:",
            _ => options.ToString()
        };
    }
}