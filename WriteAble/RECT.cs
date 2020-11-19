using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WriteAble
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
       
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        
    }
}
