using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Unqlite.Interop
{
    internal class UnqliteDBProxy:IDisposable
    {
        public IntPtr DBHandle { get; private set; }
        private bool disposed;

        ~UnqliteDBProxy()
        {
            Dispose(false);
        }

        internal bool Open(string fileName, Unqlite_Open iMode)
        {
            IntPtr handler=IntPtr.Zero;
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

        internal string GetKeyValue(string Key)
        {
            byte[] value=null;
            UInt64 valueLength=0;
            byte[] keyData = Encoding.UTF8.GetBytes(Key);
            //rc = unqlite_kv_fetch(pDb, "record", -1, NULL, &nBytes);
            int res = Libunqlite.unqlite_kv_fetch(DBHandle, keyData, keyData.Length, null,out valueLength);
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
    }
}
