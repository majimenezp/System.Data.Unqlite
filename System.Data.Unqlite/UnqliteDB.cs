using System;
using System.Collections.Generic;
using System.Data.Unqlite.Interop;
using System.Linq;
using System.Text;

namespace System.Data.Unqlite
{
    public class UnqliteDB
    {
        private readonly UnqliteDBProxy dbProxy;

        internal UnqliteDB(UnqliteDBProxy proxy)
        {
            if (proxy == null)
            {
                throw new ArgumentNullException("dbproxy");
            }
            dbProxy = proxy;
        }

        public static UnqliteDB Create()
        {
            var proxy = new UnqliteDBProxy();
            return new UnqliteDB(proxy);
        }

        public bool Open(string fileName, Unqlite_Open iMode)
        {
            return dbProxy.Open(fileName, iMode);
        }

        public bool SaveKeyValue(string Key, string Value)
        {
            return dbProxy.SaveKeyValue(Key, Value);
        }

        public bool SaveKeyBinaryValue(string Key, byte[] Value)
        {
            return dbProxy.SaveKeyValue(Key, Value);
        }

        public string GetKeyValue(string Key)
        {
            return dbProxy.GetKeyValue(Key);
        }
        public byte[] GetKeyBinaryValue(string Key)
        {
            return dbProxy.GetKeyBinaryValue(Key);
        }

        public void GetKeyValue(string Key, Action<string> action)
        {
            dbProxy.GetKeyValue(Key, action);
        }

        public void GetKeyBinaryValue(string Key, Action<byte[]> action)
        {
            dbProxy.GetKeyBinaryValue(Key, action);
        }

        public void Close()
        {
            dbProxy.Close();
        }

        public KeyValueCursor CreateKeyValueCursor()
        {
            return new KeyValueCursor(dbProxy,true);
        }

        public KeyValueCursor CreateReverseKeyValueCursor()
        {
            return new KeyValueCursor(dbProxy,false);
        }

    }
}
