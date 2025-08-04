using NoMod.Unmanaged;
using NoMod.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface ITaskAdapter
    {
        void Terminate();
        void End();
        bool ShouldEnd();
        bool HasEnded();
        uint ThreadId();
    }

    [ComVisible(true)]
    internal interface ITaskManagerAdapter
    {
        IntPtr CreateTask(IntPtr routine);
        void EndAllTasks();
        void TerminateAllTasks();
    }

    public class TaskManagerAdapter : ITaskManagerAdapter
    {
        private readonly List<TaskAdapter> _tasks = new List<TaskAdapter>();

        public IntPtr CreateTask(IntPtr routine)
        {
            lock (_tasks)
            {
                var task = new TaskAdapter(routine);
                _tasks.Add(task);
                return task.GetUnmanagedInterface<ITaskAdapter>();
            }
        }

        public void EndAllTasks()
        {
            lock (_tasks)
            {
                foreach (var task in _tasks)
                {
                    task.End();
                }
            }
        }

        public void TerminateAllTasks()
        {
            lock (_tasks)
            {
                foreach (var task in _tasks)
                {
                    task.Terminate();
                }
            }
        }
    }

    public class TaskAdapter : ITaskAdapter
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void TaskRoutineT(IntPtr instance, IntPtr task);

        internal readonly Thread Thread;

        private uint _threadId;
        private bool _shouldEnd;

        public TaskAdapter(IntPtr routine)
        {
            Thread = new Thread(TaskThread);
            Thread.Start(routine);
        }

        private void TaskThread(object instance)
        {
            _threadId = Kernel32.GetCurrentThreadId();
            var instancePtr = (IntPtr)instance;
            Marshal.ReadIntPtr(instancePtr).ToDelegate<TaskRoutineT>()(instancePtr, this.GetUnmanagedInterface<ITaskAdapter>());
        }

        public void End()
        {
            _shouldEnd = true;
        }

        public bool HasEnded()
        {
            return !Thread.IsAlive;
        }

        public bool ShouldEnd()
        {
            return _shouldEnd;
        }

        public void Terminate()
        {
            Thread.Abort();
        }

        public uint ThreadId()
        {
            return _threadId;
        }
    }
}
