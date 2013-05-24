using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Unqlite.Interop
{
    internal class UnqliteDBProxy : IDisposable
    {
        public IntPtr DBHandle { get; private set; }
        private bool disposed;

        ~UnqliteDBProxy()
        {
            Dispose(false);
        }

        internal bool Open(string fileName, Unqlite_Open iMode)
        {
            IntPtr handler = IntPtr.Zero;
            int res = Libunqlite.unqlite_open(out handler, fileName, (int)iMode);
            DBHandle = handler;
            return res == 0;
        }

        internal bool SaveKeyValue(string Key, string Value)
        {
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            byte[] data = Encoding.UTF8.GetBytes(Value);

            int res = Libunqlite.unqlite_kv_store(DBHandle, keyData, keyData.Length, data, (UInt64)data.Length);
            return res == 0;
        }
        internal bool SaveKeyValue(string Key, byte[] data)
        {
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            int res = Libunqlite.unqlite_kv_store(DBHandle, keyData, keyData.Length, data, (UInt64)data.Length);
            return res == 0;
        }

        internal bool AppendKeyValue(string Key, string Value)
        {
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            byte[] data = Encoding.UTF8.GetBytes(Value);
            int res = Libunqlite.unqlite_kv_append(DBHandle, keyData, keyData.Length, data, (UInt64)data.Length);
            return res == 0;
        }

        internal bool AppendKeyValue(string Key, byte[] data)
        {
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            int res = Libunqlite.unqlite_kv_append(DBHandle, keyData, keyData.Length, data, (UInt64)data.Length);
            return res == 0;
        }

        internal string GetKeyValue(string Key)
        {
            byte[] value = null;
            UInt64 valueLength = 0;
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            int res = Libunqlite.unqlite_kv_fetch(DBHandle, keyData, keyData.Length, null, out valueLength);
            if (res == 0)
            {
                value = new byte[valueLength];
                Libunqlite.unqlite_kv_fetch(DBHandle, keyData, keyData.Length, value, out valueLength);
            }
            if (value != null)
            {
                return Encoding.UTF8.GetString(value);
            }
            else
            {
                return null;
            }
        }

        internal byte[] GetKeyBinaryValue(string Key)
        {
            byte[] value = null;
            UInt64 valueLength = 0;
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            int res = Libunqlite.unqlite_kv_fetch(DBHandle, keyData, keyData.Length, null, out valueLength);
            if (res == 0)
            {
                value = new byte[valueLength];
                Libunqlite.unqlite_kv_fetch(DBHandle, keyData, keyData.Length, value, out valueLength);
            }
            if (value != null)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        internal void Close()
        {
            Libunqlite.unqlite_close(DBHandle);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Terminate();
            }

            disposed = true;
        }

        private void Terminate()
        {
            if (DBHandle == IntPtr.Zero)
            {
                return;
            }
        }

        internal void GetKeyValue(string Key, Action<string> action)
        {
            ;
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            Libunqlite.unqlite_kv_fetch_callback(DBHandle, keyData, keyData.Length,
            (dataPointer, dataLen, pUserData) =>
            {
                string value = Marshal.PtrToStringAnsi(dataPointer, (int)dataLen);
                action(value);
                return 0;
            }, null);
        }

        internal void GetKeyBinaryValue(string Key, Action<byte[]> action)
        {
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            Libunqlite.unqlite_kv_fetch_callback(DBHandle, keyData, keyData.Length,
            (dataPointer, dataLen, pUserData) =>
            {
                byte[] buffer = new byte[dataLen];
                Marshal.Copy(dataPointer, buffer, 0, (int)dataLen);
                action(buffer);
                return 0;
            }, null);
        }

        internal bool InitKVCursor(out IntPtr cursor)
        {
            int res = Libunqlite.unqlite_kv_cursor_init(DBHandle, out cursor);
            return res == 0;
        }

        internal bool KVMoveToFirstEntry(IntPtr cursor)
        {
            int res = Libunqlite.unqlite_kv_cursor_first_entry(cursor);
            return res == 0;
        }

        internal bool KVMoveToLastEntry(IntPtr cursor)
        {
            int res = Libunqlite.unqlite_kv_cursor_last_entry(cursor);
            return res == 0;
        }

        internal bool KV_ValidEntry(IntPtr cursor)
        {
            int res = Libunqlite.unqlite_kv_cursor_valid_entry(cursor);
            return res == 1;
        }

        internal void KV_PrevEntry(IntPtr cursor)
        {
            Libunqlite.unqlite_kv_cursor_prev_entry(cursor);
        }

        internal void KV_NextEntry(IntPtr cursor)
        {
            Libunqlite.unqlite_kv_cursor_next_entry(cursor);
        }

        internal byte[] KV_GetCurrentKey(IntPtr cursor)
        {
            byte[] value = null;
            int keyLength = 0;
            int res = Libunqlite.unqlite_kv_cursor_key(cursor, null, out keyLength);
            if (res == 0)
            {
                value = new byte[keyLength];
                Libunqlite.unqlite_kv_cursor_key(cursor, value, out keyLength);
            }
            return value;
        }

        internal byte[] KV_GetCurrentValue(IntPtr cursor)
        {
            byte[] value = null;
            UInt64 valueLength = 0;
            int res = Libunqlite.unqlite_kv_cursor_data(cursor, null, out valueLength);
            if (res == 0)
            {
                value = new byte[valueLength];
                Libunqlite.unqlite_kv_cursor_data(cursor, value, out valueLength);
            }
            return value;
        }

        internal void GetCursorKeyValue(IntPtr cursor, Action<string> action)
        {
            Libunqlite.unqlite_kv_cursor_key_callback(cursor,
                (dataPointer, dataLen, pUserData) =>
                {
                    string value = Marshal.PtrToStringAnsi(dataPointer, (int)dataLen);
                    action(value);
                    return 0;
                }, null);
        }

        internal void GetCursorKeyValue(IntPtr cursor, Action<byte[]> action)
        {
            Libunqlite.unqlite_kv_cursor_key_callback(cursor,
                (dataPointer, dataLen, pUserData) =>
                {
                    byte[] buffer = new byte[dataLen];
                    Marshal.Copy(dataPointer, buffer, 0, (int)dataLen);
                    action(buffer);
                    return 0;
                }, null);
        }

        internal void GetCursorValue(IntPtr cursor, Action<string> action)
        {
            Libunqlite.unqlite_kv_cursor_data_callback(cursor,
                (dataPointer, dataLen, pUserData) =>
                {
                    string value = Marshal.PtrToStringAnsi(dataPointer, (int)dataLen);
                    action(value);
                    return 0;
                }, null);
        }

        internal void GetCursorValue(IntPtr cursor, Action<byte[]> action)
        {
            Libunqlite.unqlite_kv_cursor_data_callback(cursor,
                (dataPointer, dataLen, pUserData) =>
                {
                    byte[] buffer = new byte[dataLen];
                    Marshal.Copy(dataPointer, buffer, 0, (int)dataLen);
                    action(buffer);
                    return 0;
                }, null);
        }

        internal void ReleaseCursor(IntPtr cursor)
        {
            Libunqlite.unqlite_kv_cursor_release(DBHandle, cursor);
        }

        internal void SeekKey(IntPtr cursor, string key, Unqlite_Cursor_Seek seekMode)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            Libunqlite.unqlite_kv_cursor_seek(cursor, keyBytes, keyBytes.Length, (int)seekMode);
        }
    }
}
