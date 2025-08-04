using System;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface IMonoRuntimeAdapter
    {
        bool IsLoaded();
        bool IsAssemblyLoaded(
            [MarshalAs(UnmanagedType.LPStr)] string name);
        bool ClassExists(
            [MarshalAs(UnmanagedType.LPStr)] string assembly, 
            [MarshalAs(UnmanagedType.LPStr)] string nameSpace, 
            [MarshalAs(UnmanagedType.LPStr)] string klass);
        bool MethodExists(
            [MarshalAs(UnmanagedType.LPStr)] string assembly, 
            [MarshalAs(UnmanagedType.LPStr)] string nameSpace, 
            [MarshalAs(UnmanagedType.LPStr)] string klass, 
            [MarshalAs(UnmanagedType.LPStr)] string method, 
            int numParams);
        bool CompileAssembly(
            [MarshalAs(UnmanagedType.LPStr)] string name);
        bool CompileClass(
            [MarshalAs(UnmanagedType.LPStr)] string assembly, 
            [MarshalAs(UnmanagedType.LPStr)] string nameSpace, 
            [MarshalAs(UnmanagedType.LPStr)] string klass);
        IntPtr CompileMethod(
            [MarshalAs(UnmanagedType.LPStr)] string assembly, 
            [MarshalAs(UnmanagedType.LPStr)] string nameSpace, 
            [MarshalAs(UnmanagedType.LPStr)] string klass, 
            [MarshalAs(UnmanagedType.LPStr)] string method, 
            int numParams);
    }

    public class MonoRuntimeAdapter : IMonoRuntimeAdapter
    {
        public bool ClassExists(string assembly, string nameSpace, string klass)
        {
            throw new NotImplementedException();
        }

        public bool CompileAssembly(string name)
        {
            throw new NotImplementedException();
        }

        public bool CompileClass(string assembly, string nameSpace, string klass)
        {
            throw new NotImplementedException();
        }

        public IntPtr CompileMethod(string assembly, string nameSpace, string klass, string method, int numParams)
        {
            throw new NotImplementedException();
        }

        public bool IsAssemblyLoaded(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public bool MethodExists(string assembly, string nameSpace, string klass, string method, int numParams)
        {
            throw new NotImplementedException();
        }
    }
}
