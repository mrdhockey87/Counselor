using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CounselQuickPlatinum
{
    class OpenDocumentMonitor
    {
        static List<Process> activeProcesses;
        static SortedDictionary<string, string> openDocuments;
        //static List<FileSystemWatcher> fileSystemWatchers;
        static string cqpDirectoryPathName;
        static string openFilesCacheFilename;
        static Mutex processlock;

        static OpenDocumentMonitor()
        {
            Init();
            FileInfo openFiles = new FileInfo(openFilesCacheFilename);
            if (openFiles.Exists && openFiles.Length > 0)
            {
                DeleteAllTempFiles();
            }
        }

        static void Init()
        {
            activeProcesses = new List<Process>();
            openDocuments = new SortedDictionary<string, string>();
            cqpDirectoryPathName = Utilities.GetCQPUserDataDirectory();
            openFilesCacheFilename = cqpDirectoryPathName + "\\" + "open";
            processlock = new Mutex();

            //FileInfo filename = new FileInfo(openFilesCacheFilename);
        }

        static void DeleteAllTempFiles()
        {
            List<string> names = File.ReadAllLines(openFilesCacheFilename).ToList();
            foreach (string name in names)
            {
                FileStream decryptedFileStream = new FileStream(name, FileMode.Open, FileAccess.Write);
                long bytes = name.Length;
                byte[] zero = new byte[1] { 0 };
                for (int i = 0; i < bytes; i++)
                    decryptedFileStream.Write(zero, 0, 1);
                decryptedFileStream.Close();
                File.Delete(name);
            }
        }

        static internal void OpenDocumentAsProcess(Document document, string decryptedDocumentPath)
        {
            processlock.WaitOne();

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

            string documentName = document.DocumentName;

            // copy back whenever the user saves..?
            FileInfo decryptedFileInfo = new FileInfo(decryptedDocumentPath);
            string directory = decryptedFileInfo.Directory.FullName;
            string filename = decryptedFileInfo.Name;

            /*FileSystemWatcher fsw = new FileSystemWatcher(directory);
            fsw.Filter = filename;
            fsw.EnableRaisingEvents = true;
            fsw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Size | NotifyFilters.CreationTime;
            fsw.Changed += (s, e) =>
                {
                    File.Copy(decryptedDocumentPath, 
                    document.Filepath = decryptedDocumentPath;
                    document.Save();
                };*/
            /*fsw.Created += (s, e) =>
                {
                    document.Filepath = decryptedDocumentPath;
                    document.Save();
                };*/
            //fileSystemWatchers.Add(fsw);
            
            EventHandler copyBackAndScrubOnClose = (s, e) =>
            {
                processlock.WaitOne();

                document.Filepath = decryptedDocumentPath;
                document.Save();

                // - Ensure we do not copy back a file of all zeroes back to the user directory
                // - Get rid of the FileSystemWatcher before we cleanse the decrypted file.
                //FileSystemWatcher watcher = fileSystemWatchers.Find(f => f.Path == directory && f.Filter == filename);
                //fileSystemWatchers.Remove(watcher);
                //watcher.Dispose();
                //watcher = null;

                //FileInfo decryptedFileInfo = new FileInfo(decryptedDocumentPath);
                FileStream decryptedFileStream = new FileStream(decryptedDocumentPath, FileMode.Open, FileAccess.Write);
                long bytes = decryptedFileInfo.Length;
                byte[] zero = new byte[1] { 0 };
                for (int i = 0; i < bytes; i++)
                    decryptedFileStream.Write(zero, 0, 1);
                decryptedFileStream.Close();
                File.Delete(decryptedDocumentPath);

                activeProcesses.Remove(p);
                openDocuments.Remove(decryptedDocumentPath);

                RefreshOpenProcessesFile();

                processlock.ReleaseMutex();
            };

            info.FileName = decryptedDocumentPath;
            p.EnableRaisingEvents = true;
            p.Exited += copyBackAndScrubOnClose;
            p.StartInfo = info;

            p.Start();
            activeProcesses.Add(p);
            openDocuments.Add(decryptedDocumentPath, document.DocumentName);

            RefreshOpenProcessesFile();

            processlock.ReleaseMutex();
        }

        static void RefreshOpenProcessesFile()
        {

            StreamWriter writer = new StreamWriter(openFilesCacheFilename, false);
            foreach (string name in GetOpenDocumentNames().Keys)
                writer.WriteLine(name);
            writer.Flush();
            writer.Close();
        }

        static internal bool OpenProcessesExist()
        {
            int numberProcessOpen = 0;
            processlock.WaitOne();
            numberProcessOpen = activeProcesses.Count;
            processlock.ReleaseMutex();

            return numberProcessOpen > 0;
        }

        static internal void CloseAll()
        {
            List<Process> activeProcessCopy = new List<Process>(activeProcesses);
            int numProcessesStart = activeProcessCopy.Count;

            for(int i = 0; i < activeProcessCopy.Count; i++)
            {
                //processlock.WaitOne();
                Process p = activeProcessCopy[i];
                //processlock.ReleaseMutex();

                p.Kill();
                p.WaitForExit();
                while (activeProcesses.Count >= numProcessesStart - i)
                {
                    Thread.Sleep(500);
                }
            }
        }

        static internal SortedDictionary<string, string> GetOpenDocumentNames()
        {
            return openDocuments;
        }
    }
}
