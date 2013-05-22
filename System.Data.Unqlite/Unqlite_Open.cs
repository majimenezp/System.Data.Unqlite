using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Unqlite
{
    public enum Unqlite_Open
    {
        READONLY = 0x00000001,  /* Read only mode. Ok for [unqlite_open] */
        READWRITE = 0x00000002,  /* Ok for [unqlite_open] */
        CREATE = 0x00000004,  /* Ok for [unqlite_open] */
        EXCLUSIVE = 0x00000008,  /* VFS only */
        TEMP_DB = 0x00000010,  /* VFS only */
        NOMUTEX = 0x00000020,  /* Ok for [unqlite_open] */
        OMIT_JOURNALING = 0x00000040,  /* Omit journaling for this database. Ok for [unqlite_open] */
        IN_MEMORY = 0x00000080,  /* An in memory database. Ok for [unqlite_open]*/
        MMAP = 0x00000100  /* Obtain a memory view of the whole file. Ok for [unqlite_open] */
    }
}
