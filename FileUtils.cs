using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace CounselQuickPlatinum
{
    public class FileUtils
    {
        public enum FileType
        {
            UserGenerated,
            XFDL,
            DOC,
            PDF,
            Unknown
        };


        public static FileType GetFileTypeFromFilename(string filename)
        {
            FileInfo fileinfo = new FileInfo(filename);

            if (!fileinfo.Exists)
                throw new FileException(filename);

            if (fileinfo.Extension.ToLower() == "xfdl")
                return FileType.XFDL;
            if (fileinfo.Extension.ToLower() == "doc")
                return FileType.DOC;
            if (fileinfo.Extension.ToLower() == "pdf")
                return FileType.PDF;

            return FileType.Unknown;
        }

        public static FileType GetFileTypeFromFileTypeString(string filetypeString)
        {
            if (filetypeString.ToLower() == "xfdl")
                return FileType.XFDL;
            if (filetypeString.ToLower() == "doc")
                return FileType.DOC;
            if (filetypeString.ToLower() == "pdf")
                return FileType.PDF;
            if (filetypeString.ToLower() == "usergenerated")
                return FileType.UserGenerated;

            return FileType.Unknown;
        }


        internal static bool CopyUserFileToDirectory(string originalFile, string newFile)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(newFile);
            string directory = fileInfo.Directory.FullName;
 
            CreateDirectoryIfNotExists(directory);
            System.IO.File.Copy(originalFile, newFile);

            return true;
        }


        private static bool BlockingCreateDirectory(string directory)
        {
            System.IO.DirectoryInfo directoryToCreate = new System.IO.DirectoryInfo(directory);
            System.IO.DirectoryInfo target = new System.IO.DirectoryInfo(directoryToCreate.Parent.FullName);
            /*
            bool ready = false;
            
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.DirectoryName; // | NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.DirectoryName
            watcher.Path = target.FullName;
            watcher.InternalBufferSize = 65535;
            watcher.EnableRaisingEvents = true;
            
            bool firsttime = true;
            DateTime previousLastWriteTime = new DateTime();

            watcher.Created += (sender, e) =>
            {
                try
                {
                    // get the time the file was modified
                    // check it again in 100 ms
                    // when it has gone a while without modification, it's done?
                    while (!ready)
                    {
                        if (firsttime)
                        {
                            previousLastWriteTime = System.IO.File.GetLastWriteTime(target.FullName);
                            firsttime = false;
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }

                        DateTime currentLastWriteTime = System.IO.File.GetLastWriteTime(target.FullName);

                        bool fileModified = (currentLastWriteTime != previousLastWriteTime);

                        if (fileModified)
                        {
                            previousLastWriteTime = currentLastWriteTime;
                            ready = false;
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
                        else
                        {
                            ready = true;
                            break;
                        }
                    }

                }
                catch (Exception)
                {
                    ready = false;
                }
            };
            */
            

            try
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            catch (Exception)
            {
                return false;
            }
            /*
            while (!ready)*/
                System.Threading.Thread.Sleep(500);

            return true;
        }


        internal static bool CreateDirectoryIfNotExists(string directory)
        {
            if (System.IO.Directory.Exists(directory))
                return true;
            //else
            //    System.IO.Directory.CreateDirectory(directory);

            bool createSuccessful = BlockingCreateDirectory(directory);
            return createSuccessful;
            //return true;
        }


        internal static string GetNextAvailableFilename(string directory, string filename)
        {
            int i = 1;
            System.IO.FileInfo info = new System.IO.FileInfo(filename);
            string extension = info.Extension;
            string filenameWithoutExtension = info.Name.Substring(0, 
                                                info.Name.Length - extension.Length);

            string tempFilename = filenameWithoutExtension + extension;
            while (System.IO.File.Exists(directory + "\\" + tempFilename))
            {
                tempFilename = filenameWithoutExtension + "(" + i + ")" + extension;
                i++;
            }

            filename = tempFilename;

            return filename;
        }


        public static bool VerifyFileExists(string filename)
        {
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(filename);

            if (!fileinfo.Exists)
                return false;
            else
                return true;
        }


        public static bool VerifyDirectoryExists(string filename)
        {
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(filename);
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(fileinfo.DirectoryName);

            if (!directory.Exists)
                return false;
            else
                return true;
        }


        public static bool DirectoryReadOnly(string filename)
        {
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(filename);
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(fileinfo.DirectoryName);

            if ((directory.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                return true;
            else
                return false;

        }

        public static void CreateNewCopy(string templateFilename, string newFormFilename)
        {

            Logger.Trace("Create New Copy : " + Directory.GetCurrentDirectory());

            ThrowIfCopyNotOkay(templateFilename, newFormFilename);

            Logger.Trace("Create New Copy : " + Directory.GetCurrentDirectory());

            try
            {
                Logger.Trace("Create New Copy : " + Directory.GetCurrentDirectory());

                FileInfo templateFile = new FileInfo(templateFilename);
                FileInfo newFormFile = new FileInfo(newFormFilename);
                BlockingFileCopy(templateFile, newFormFile);

                Logger.Trace("Create New Copy : " + Directory.GetCurrentDirectory());
            }
            catch (UnauthorizedAccessException ex)
            {
                string message = "Could not save " + newFormFilename + ".  The specified directory is write only.";
                Logger.Error(message);
                throw new FileException(message, ex);
            }
            catch (Exception ex)
            {
                string message = "An unknonwn error occured attempting to save " + newFormFilename;
                Logger.Error(message);
                throw new FileException(message, ex);
            }
        }


        private static void ThrowIfCopyNotOkay(string templateFilename, string newFormFilename)
        {
            bool templateFileExists = VerifyFileExists(templateFilename);
            bool newFormDirectoryExists = VerifyDirectoryExists(newFormFilename);
            bool newFormDirectoryReadOnly = DirectoryReadOnly(newFormFilename);

            if (!templateFileExists)
            {
                string message = "Unable to generate " + newFormFilename + " because the template file "
                                + templateFilename + " is missing";

                Logger.Error(message);
                
                throw new CQPExportFailedException(ExportFailedReason.TemplateMissing, message);
            }
            if (!newFormDirectoryExists)
            {
                string message = "Unable to generate " + newFormFilename + " because the specified output directory does not exist.";
                
                Logger.Error(message);

                throw new CQPExportFailedException(ExportFailedReason.InvalidFilename, message);
            }
        }

        internal static void BlockingFileCopy(string src, string tgt)
        {
            FileInfo source = new FileInfo(src);
            FileInfo target = new FileInfo(tgt);

            BlockingFileCopy(source, target);
        }


        internal static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            if (file.Exists == false)
                return false;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }


        internal static void BlockingFileCopy(FileInfo original, FileInfo target)
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(target.DirectoryName);

            bool ready = false;

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Path = directory.FullName,
                Filter = "*" + target.Extension,
                InternalBufferSize = 65535,
                EnableRaisingEvents = true
            };

            bool firsttime = true;
            DateTime previousLastWriteTime = new DateTime();

            watcher.Changed += (sender, e) =>
            {
                try
                {
                    // get the time the file was modified
                    // check it again in 100 ms
                    // when it has gone a while without modification, it's done?
                    while (!ready)
                    {
                        if (firsttime)
                        {
                            previousLastWriteTime = System.IO.File.GetLastWriteTime(target.FullName);
                            firsttime = false;
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }

                        DateTime currentLastWriteTime = System.IO.File.GetLastWriteTime(target.FullName);

                        bool fileModified = (currentLastWriteTime != previousLastWriteTime);

                        if (fileModified)
                        {
                            previousLastWriteTime = currentLastWriteTime;
                            ready = false;
                            System.Threading.Thread.Sleep(50);
                            continue;
                        }
                        else
                        {
                            ready = true;
                            break;
                        }
                    }

                }
                catch (Exception)
                {
                    ready = false;
                }
            };


            System.IO.File.Copy(original.FullName, target.FullName, true);

            while (!ready)
            {
                System.Threading.Thread.Sleep(100);
            }
        }


        internal static bool FileTypeHandlerExists(string filename)
        {
            FileInfo file = new FileInfo(filename);
            string extension = file.Extension;

            if (extension == ".doc" || extension == ".docx")
            {
                if (WordIsInstalled())
                    return true;
                else
                    return false;
            }
            else if (extension == ".xfdl")
            {
                if (LotusIsInstalled())
                    return true;
                else
                    return false;
            }
            else if (extension == ".pdf")
            {
                if (AdobeIsInstalled())
                    return true;
                else
                    return false;
            }

            return false;
        }


        private static bool WordIsInstalled()
        {
            Type applicationType = Type.GetTypeFromProgID("Word.Application");

            if (applicationType == null)
                return false;
            else
                return true;
        }


        private static bool LotusIsInstalled()
        {
            if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("XFDL.Document.1") == null)
                return false;
            else
                return true;
        }


        private static bool AdobeIsInstalled()
        {
            Type applicationType = Type.GetTypeFromProgID("AcroPDF.PDF");

            if (applicationType == null)
                return false;
            else
                return true;
        }

        internal static string FilenameWithoutExtension(FileInfo file)
        {
            string extension = file.Extension;
            int nameLength = file.Name.Length;

            string nameWithoutExtension = file.Name.Substring(0, nameLength - extension.Length);
            return nameWithoutExtension;
        }
    }
}
