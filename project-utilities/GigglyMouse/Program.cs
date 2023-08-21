Console.WriteLine("Hello, Starting GigglyMouse!");

Console.WriteLine("Enter the idle time in seconds (default is 10):");
string input = Console.ReadLine();
int idleTime = 10;

if (!string.IsNullOrEmpty(input) && !int.TryParse(input, out idleTime))
{
    Console.WriteLine("You did not enter a valid number. Using default idle time.");
}

MouseMonitor mouseMonitor = new MouseMonitor(idleTime);
MouseGiggler mouseGiggler = new MouseGiggler(mouseMonitor);
mouseMonitor.StartMonitoring();

Console.WriteLine("GigglyMouse started!");