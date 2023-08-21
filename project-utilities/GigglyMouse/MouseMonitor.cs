using System;
using System.Runtime.InteropServices;
using System.Timers;
using Timer = System.Timers.Timer;

public class MouseMonitor
{
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    public struct POINT
    {
        public int X;
        public int Y;
    }

    private readonly Timer _monitoringTimer;
    private POINT _lastMousePoint;

    public event Action OnMouseIdle;

    public MouseMonitor(int idleSeconds)
    {
        _monitoringTimer = new Timer(idleSeconds * 1000);
        _monitoringTimer.Elapsed += HandleTimerElapsed;
    }

    private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (GetCursorPos(out POINT currentMousePoint))
        {
            if (currentMousePoint.X == _lastMousePoint.X && currentMousePoint.Y == _lastMousePoint.Y)
            {
                OnMouseIdle?.Invoke();
            }
            else
            {
                _lastMousePoint = currentMousePoint;
            }
        }
    }

    public void StartMonitoring()
    {
        _monitoringTimer.Start();
    }

    public void StopMonitoring()
    {
        _monitoringTimer.Stop();
    }
}

public class MouseGiggler
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    public MouseGiggler(MouseMonitor mouseMonitor)
    {
        mouseMonitor.OnMouseIdle += HandleMouseIdle;
    }

    private void HandleMouseIdle()
    {
        Random random = new Random();
        int x = random.Next(0, 1920); // Assuming a 1920 pixels screen width
        int y = random.Next(0, 1080); // Assuming a 1080 pixels screen height

        SetCursorPos(x, y);
        Console.WriteLine($"{DateTime.UtcNow:O} - I giggled the mouse !");
    }
}
