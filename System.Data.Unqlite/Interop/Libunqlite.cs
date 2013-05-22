using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Unqlite.Interop
{
    internal static class Libunqlite
    {
        public const string LibraryName = "unqlite";

        private static readonly UnmanagedLibrary NativeLib;

        static Libunqlite()
        {
            NativeLib = new UnmanagedLibrary(LibraryName);
            AssignCommonDelegates();
        }

        private static void AssignCommonDelegates()
        {
            unqlite_open = NativeLib.GetUnmanagedFunction<Unqlite_open>("unqlite_open");
            unqlite_kv_store = NativeLib.GetUnmanagedFunction<Unqlite_kv_store>("unqlite_kv_store");
            unqlite_close = NativeLib.GetUnmanagedFunction<Unqlite_close>("unqlite_close");
            unqlite_kv_fetch = NativeLib.GetUnmanagedFunction<Unqlite_kv_fetch>("unqlite_kv_fetch");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_open(out IntPtr ppDB, string zFilename, int iMode);
        public static Unqlite_open unqlite_open;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public delegate int Unqlite_kv_store(IntPtr ppDB, byte[] key, int keylength, byte[] value, UInt64 valuelength);
        public static Unqlite_kv_store unqlite_kv_store;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_close(IntPtr ppDB);
        public static Unqlite_close unqlite_close;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_fetch(IntPtr ppDB,byte[] key,int keylength,byte[] value,out UInt64 valuelength);
        public static Unqlite_kv_fetch unqlite_kv_fetch;
        


    }
}
