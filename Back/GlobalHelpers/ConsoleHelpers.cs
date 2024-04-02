namespace GlobalHelpers;

public static class ConsoleHelpers
{
    public static void WriteStartUpMessage(string serviceName)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Starting the {serviceName} service...\n at {DateTime.Now}");
        Console.WriteLine();
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Short info:");
        Console.WriteLine(Environment.OSVersion);
        Console.WriteLine(Environment.Version);
        Console.WriteLine(Environment.CurrentDirectory);
        Console.ResetColor();
        Console.WriteLine();
    }
}