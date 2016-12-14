//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace CygSoft.CodeCat.UI.WinForms
//{
//    public class OpenWithDialog
//    {
//        // Calling the Open With dialog box from your application, using C#
//        // https://www.codeproject.com/Articles/13103/Calling-the-Open-With-dialog-box-from-your-applica
//        [Serializable]
//        private struct ShellExecuteInfo
//        {
//            public int Size;
//            public uint Mask;
//            public IntPtr hwnd;
//            public string Verb;
//            public string File;
//            public string Parameters;
//            public string Directory;
//            public uint Show;
//            public IntPtr InstApp;
//            public IntPtr IDList;
//            public string Class;
//            public IntPtr hkeyClass;
//            public uint HotKey;
//            public IntPtr Icon;
//            public IntPtr Monitor;
//        }

//        private const uint SW_NORMAL = 1;

//        // Code For OpenWithDialog Box
//        [DllImport("shell32.dll", SetLastError = true)]
//        extern private static bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);


//        public void Open(string filePath)
//        {
//            ShellExecuteInfo sei = new ShellExecuteInfo();
//            sei.Size = Marshal.SizeOf(sei);
//            sei.Verb = "openas";
//            sei.File = filePath;
//            sei.Show = SW_NORMAL;
//            if (!ShellExecuteEx(ref sei))
//                throw new System.ComponentModel.Win32Exception();
//        }
//    }
//}
