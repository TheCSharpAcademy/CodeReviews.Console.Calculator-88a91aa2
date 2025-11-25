namespace calculator.schoffen;

public enum MainMenuOptions
{
    Calculate,
    ShowLatestTransactions,
    DeleteLatestTransactions,
    Exit
}

public static class MainMenuOptionExtensions
{
    public static string ToDisplay(this MainMenuOptions options)
    {
        return options switch
        {
            MainMenuOptions.Calculate => "Calculate",
            MainMenuOptions.ShowLatestTransactions => "Show Latest Transactions",
            MainMenuOptions.DeleteLatestTransactions => "Delete Latest Transactions",
            MainMenuOptions.Exit => "Exit",
            _ => options.ToString()
        };
    }
}
