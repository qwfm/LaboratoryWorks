using System;
using System.IO;

/*
(*)Інформація про об’єкти файлової системи – файли та каталоги (назва, розмір, час створення, час модифікації, тип файлу). 
Розрахунок повного шляху до файлу/каталогу. Пошук файлів за заданими критеріями в дереві підкаталогу. 
+* за підтримку критеріїв за кількома параметрами одночасно; 
*/
public class FileSystem
{
    private string path;

    public FileSystem(string folder)
    {
        this.path = folder;
    }

//All the files that are closed behind administrator rights will be skipped
    public void GetFileInfo()
{
    if (File.Exists(path) || Directory.Exists(path))
    {
        try
        {
            Console.WriteLine($"Information about: {path}\n");

            var info = (File.Exists(path)) ? new FileInfo(path) as FileSystemInfo : new DirectoryInfo(path);

            Console.WriteLine($"Name: {info.Name}");
            Console.WriteLine($"Creation time: {info.CreationTime}");
            Console.WriteLine($"Last update: {info.LastWriteTime}");

            if (info is FileInfo fileInfo)
            {
                Console.WriteLine($"Type: {fileInfo.Extension}");
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"UnauthorizedAccessException: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine($"The specified path does not exist.");
    }
}

  private FileSystemInfo FindByCriteria(string currentFolder, string name, Func<FileSystemInfo, bool> criteria)
    {
        try
        {
            foreach (var entry in new DirectoryInfo(currentFolder).GetFileSystemInfos())
            {
                try
                {
                    if (criteria(entry))
                    {
                        return entry;
                    }
                }
                catch (UnauthorizedAccessException ex) {}
            }

            foreach (var subfolder in Directory.GetDirectories(currentFolder))
            {
                try
                {
                    var result = FindByCriteria(subfolder, name, criteria);
                    if (result != null)
                        return result;
                }
                catch (UnauthorizedAccessException ex) {}
            }
        }
        catch (UnauthorizedAccessException ex) {}

        return null;
    }

    public void FindAndShow(string name, Func<FileSystemInfo, bool> criteria)
    {
        if (Directory.Exists(path))
        {
            var foundItem = FindByCriteria(path, name, criteria);
            if (foundItem != null)
            {
                if (foundItem is FileInfo)
                {
                    Console.WriteLine($"File {name} found: {foundItem.FullName}");
                }
                else if (foundItem is DirectoryInfo)
                {
                    Console.WriteLine($"Folder {name} found: {foundItem.FullName}");
                }
            }
            else 
            {
                Console.WriteLine($"File or folder {name} doesn't exist.");
            }
        }
        else 
        {
            Console.WriteLine($"Noted path doesn't exist.");
        }
    }
}
