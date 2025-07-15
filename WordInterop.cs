using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace CounselQuickPlatinum
{
    class WordInterop
    {
        internal bool IsWordInitialized { get { return isWordInitialized; } }
        private bool isWordInitialized;

        private object WinWordApplication;

        private object docObject;
        //private List<object> bookmarks;
        private SortedDictionary<string, object> bookmarks;

        internal static bool WordExists
        {
            get
            {
                bool wordExists = true;

                Type type = Type.GetTypeFromProgID("Word.Application");

                if (type == null)
                    wordExists = false;

                return wordExists;
                //return false;
            }
        }


        public void Initialize()
        {
            try
            {
                Type objClassType = Type.GetTypeFromProgID("Word.Application");
                WinWordApplication = Activator.CreateInstance(objClassType);

                object []Visible = new object[1];
                Visible[0] = false;

                WinWordApplication.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, WinWordApplication, Visible);
            }
            catch (Exception ex)
            {
                isWordInitialized = false;
                throw new WordInteropException("An error occurred loading Microsoft Word on this PC.", ex);
            }

            isWordInitialized = true;
        }


        public void OpenDoc(string filename)
        {
            //WordInterop wordDoc = new WordInterop();
            object docs;
            object doc;
            object bookmarksProperty;
            int numBookmarks;

            if (!IsWordInitialized)
                Initialize();

            docs = WinWordApplication.GetType().InvokeMember("Documents", BindingFlags.GetProperty,
                                                                null, WinWordApplication, null);

            object[] args = new object[16];
            args[0] = filename;
            for (int i = 1; i < 16; i++)
                args[i] = Type.Missing;

            try
            {
                doc = docs.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null,
                                            docs, args);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            bookmarksProperty = doc.GetType().InvokeMember("Bookmarks", BindingFlags.GetProperty, null, doc, null);

            numBookmarks = (int) bookmarksProperty.GetType().InvokeMember("Count", BindingFlags.GetProperty, null, bookmarksProperty, null);

            bookmarks = new SortedDictionary<string, object>();

            object []bookmarkArgs = new object[1];
            for (int i = 1; i <= numBookmarks; i++)
            {
                bookmarkArgs[0] = i;
                object bookmark = bookmarksProperty.GetType().InvokeMember("Item", BindingFlags.InvokeMethod, null, bookmarksProperty, bookmarkArgs);
                string bookmarkName = (string)bookmark.GetType().InvokeMember("Name", BindingFlags.GetProperty, null, bookmark, null);

                //wordDoc.bookmarks.Add(new KeyValuePair<string, object>(bookmarkName, bookmark));
                bookmarks[bookmarkName] = bookmark;
            }

            docObject = doc;
            
            //return wordDoc;
        }


        public void Close()
        {
            if (!isWordInitialized)
                return;

            docObject.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, docObject, null);

            WinWordApplication.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null, WinWordApplication, null);

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WinWordApplication);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        public bool UpdateBookmark(string bookmarkName, string newValue)
        {
            foreach (KeyValuePair<string,object> bookmark in bookmarks)
            {
                if (bookmark.Key != bookmarkName)
                    continue;

                object Range = bookmark.Value.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, bookmark.Value, null);
                Range.GetType().InvokeMember("Text", BindingFlags.SetProperty, null, Range, new object[] { newValue });
                return true;
            }

            return false;
        }




        internal string GetBookmarkValue(string propertyName)
        {
            object bookmark = bookmarks[propertyName];

            object range = bookmark.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, bookmark, null);
            string value = range.GetType().InvokeMember("Text", BindingFlags.GetProperty, null, range, null ).ToString();

            return value;
        }
    }
}
