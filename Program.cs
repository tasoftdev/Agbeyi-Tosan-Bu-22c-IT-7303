using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string directoryName = "FileCollection";
        string resultsFileName = "results.txt";
        
        // Create the directory if it doesn't exist
        Directory.CreateDirectory(directoryName);

        // Initialize counters and size variables
        int excelCount = 0, wordCount = 0, pptCount = 0;
        long excelSize = 0, wordSize = 0, pptSize = 0;

        // Create DirectoryInfo object
        DirectoryInfo dirInfo = new DirectoryInfo(directoryName);

        // Enumerate files in the directory
        foreach (var file in dirInfo.EnumerateFiles())
        {
            if (IsOfficeFile(file))
            {
                // Update counters and sizes based on file type
                switch (file.Extension.ToLower())
                {
                    case ".xlsx":
                        excelCount++;
                        excelSize += file.Length;
                        break;
                    case ".docx":
                        wordCount++;
                        wordSize += file.Length;
                        break;
                    case ".pptx":
                        pptCount++;
                        pptSize += file.Length;
                        break;
                }
            }
        }

        // Write results to results.txt
        using (StreamWriter writer = new StreamWriter(Path.Combine(directoryName, resultsFileName)))
        {
            writer.WriteLine("Summary of Office files in directory: " + directoryName);
            writer.WriteLine($"Excel files: {excelCount}, Total size: {FormatSize(excelSize)}");
            writer.WriteLine($"Word files: {wordCount}, Total size: {FormatSize(wordSize)}");
            writer.WriteLine($"PowerPoint files: {pptCount}, Total size: {FormatSize(pptSize)}");
            writer.WriteLine($"Total Office files: {excelCount + wordCount + pptCount}");
            writer.WriteLine($"Total size of Office files: {FormatSize(excelSize + wordSize + pptSize)}");
        }

        Console.WriteLine("Summary written to " + resultsFileName);
    }

    static bool IsOfficeFile(FileInfo file)
    {
        string[] officeExtensions = { ".xlsx", ".docx", ".pptx" };
        return officeExtensions.Contains(file.Extension.ToLower());
    }

    static string FormatSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return String.Format("{0:0.##} {1}", len, sizes[order]);
    }
}
