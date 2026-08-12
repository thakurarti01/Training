using System;
using System.Collections.Generic;
using System.Text;

class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }

    public LogEntry(DateTime timestamp, string logLevel,
                    string message, string exception = "")
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        Message = message;
        Exception = exception;
    }
}

class LogProcessor
{
    private StringBuilder buffer;
    private int capacity;

    // Stores error logs separately
    private List<LogEntry> errorLogs;

    public LogProcessor(int capacity)
    {
        this.capacity = capacity;
        buffer = new StringBuilder();
        errorLogs = new List<LogEntry>();
    }

    public void ProcessLog(LogEntry log)
    {
        // Build formatted log using StringBuilder
        buffer.AppendLine(
            $"[{log.Timestamp:yyyy-MM-dd HH:mm:ss}] " +
            $"{log.LogLevel}: {log.Message}"
        );

        // Store error logs separately
        if (log.LogLevel.Equals("ERROR",
            StringComparison.OrdinalIgnoreCase))
        {
            errorLogs.Add(log);

            if (!string.IsNullOrEmpty(log.Exception))
            {
                buffer.AppendLine($"Exception: {log.Exception}");
            }
        }

        // Flush when buffer reaches capacity
        if (buffer.Length >= capacity)
        {
            FlushBuffer();
        }
    }

    public void FlushBuffer()
    {
        if (buffer.Length == 0)
            return;

        Console.WriteLine("----- BUFFER FLUSH -----");
        Console.WriteLine(buffer.ToString());

        // Clear buffer after flushing
        buffer.Clear();
    }

    public void DisplayErrorSummary()
    {
        // Flush remaining logs first
        FlushBuffer();

        Console.WriteLine("----- ERROR SUMMARY -----");
        Console.WriteLine($"Total Error Logs: {errorLogs.Count}");

        foreach (LogEntry error in errorLogs)
        {
            Console.WriteLine(
                $"{error.Timestamp:yyyy-MM-dd HH:mm:ss} - " +
                $"{error.Message}"
            );

            if (!string.IsNullOrEmpty(error.Exception))
            {
                Console.WriteLine($"Exception: {error.Exception}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Buffer capacity
        LogProcessor processor = new LogProcessor(200);

        // Create log entries
        LogEntry log1 = new LogEntry(
            DateTime.Now,
            "INFO",
            "Application started"
        );

        LogEntry log2 = new LogEntry(
            DateTime.Now,
            "INFO",
            "User logged in"
        );

        LogEntry log3 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "Database connection failed",
            "SqlException: Connection timeout"
        );

        LogEntry log4 = new LogEntry(
            DateTime.Now,
            "WARNING",
            "Memory usage is high"
        );

        LogEntry log5 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "File could not be found",
            "FileNotFoundException"
        );

        // Process logs
        processor.ProcessLog(log1);
        processor.ProcessLog(log2);
        processor.ProcessLog(log3);
        processor.ProcessLog(log4);
        processor.ProcessLog(log5);

        // Display remaining logs and error summary
        processor.DisplayErrorSummary();
    }
}