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
            unqlite_kv_append = NativeLib.GetUnmanagedFunction<Unqlite_kv_append>("unqlite_kv_append");
            unqlite_kv_fetch_callback = NativeLib.GetUnmanagedFunction<Unqlite_kv_fetch_callback>("unqlite_kv_fetch_callback");
            unqlite_kv_cursor_init = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_init>("unqlite_kv_cursor_init");
            unqlite_kv_cursor_first_entry = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_first_entry>("unqlite_kv_cursor_first_entry");
            unqlite_kv_cursor_last_entry = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_last_entry>("unqlite_kv_cursor_last_entry");
            unqlite_kv_cursor_valid_entry = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_valid_entry>("unqlite_kv_cursor_valid_entry");
            unqlite_kv_cursor_next_entry = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_next_entry>("unqlite_kv_cursor_next_entry");
            unqlite_kv_cursor_prev_entry = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_prev_entry>("unqlite_kv_cursor_prev_entry");
            unqlite_kv_cursor_release = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_release>("unqlite_kv_cursor_release");
            unqlite_kv_cursor_reset = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_reset>("unqlite_kv_cursor_reset");

            unqlite_kv_cursor_key = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_key>("unqlite_kv_cursor_key");
            unqlite_kv_cursor_key_callback = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_key_callback>("unqlite_kv_cursor_key_callback");
            unqlite_kv_cursor_data = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_data>("unqlite_kv_cursor_data");
            unqlite_kv_cursor_data_callback = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_data_callback>("unqlite_kv_cursor_data_callback");
            unqlite_kv_cursor_seek = NativeLib.GetUnmanagedFunction<Unqlite_kv_cursor_seek>("unqlite_kv_cursor_seek");
            
            
        }

        public delegate int xConsumer(IntPtr dataPointer, UInt64 iDataLen, byte[] pUserData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_open(out IntPtr ppDB, string zFilename, int iMode);
        public static Unqlite_open unqlite_open;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public delegate int Unqlite_kv_store(IntPtr ppDB, byte[] key, int keylength, byte[] value, UInt64 valuelength);
        public static Unqlite_kv_store unqlite_kv_store;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_append(IntPtr ppDB, byte[] key, int keylength, byte[] value, UInt64 valuelength);
        public static Unqlite_kv_append unqlite_kv_append;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_close(IntPtr ppDB);
        public static Unqlite_close unqlite_close;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_fetch(IntPtr ppDB,byte[] key,int keylength,byte[] value,out UInt64 valuelength);
        public static Unqlite_kv_fetch unqlite_kv_fetch;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_fetch_callback(IntPtr ppDB, byte[] key, int keylength,xConsumer callback ,byte[] pUserData);
        public static Unqlite_kv_fetch_callback unqlite_kv_fetch_callback;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_init(IntPtr ppDB,out IntPtr ppOut);
        public static Unqlite_kv_cursor_init unqlite_kv_cursor_init;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_release(IntPtr ppDB, IntPtr pCur);
        public static Unqlite_kv_cursor_release unqlite_kv_cursor_release;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_reset(IntPtr ppDB, IntPtr pCur);
        public static Unqlite_kv_cursor_reset unqlite_kv_cursor_reset;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_first_entry(IntPtr pCursor);
        public static Unqlite_kv_cursor_first_entry unqlite_kv_cursor_first_entry;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_last_entry(IntPtr pCursor);
        public static Unqlite_kv_cursor_last_entry unqlite_kv_cursor_last_entry;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_valid_entry(IntPtr pCursor);
        public static Unqlite_kv_cursor_valid_entry unqlite_kv_cursor_valid_entry;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_next_entry(IntPtr pCursor);
        public static Unqlite_kv_cursor_next_entry unqlite_kv_cursor_next_entry;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_prev_entry(IntPtr pCursor);
        public static Unqlite_kv_cursor_prev_entry unqlite_kv_cursor_prev_entry;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_key(IntPtr pCursor,byte[] Key,out int KeyLen);
        public static Unqlite_kv_cursor_key unqlite_kv_cursor_key;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_key_callback(IntPtr pCursor, xConsumer callback, byte[] pUserData);
        public static Unqlite_kv_cursor_key_callback unqlite_kv_cursor_key_callback;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_data(IntPtr pCursor, byte[] Value,out UInt64 ValueLen);
        public static Unqlite_kv_cursor_data unqlite_kv_cursor_data;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_data_callback(IntPtr pCursor, xConsumer callback, byte[] pUserData);
        public static Unqlite_kv_cursor_data_callback unqlite_kv_cursor_data_callback;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int Unqlite_kv_cursor_seek(IntPtr pCursor, byte[] Key, int Keylen,int SeekMode);
        public static Unqlite_kv_cursor_seek unqlite_kv_cursor_seek;

    }
}
