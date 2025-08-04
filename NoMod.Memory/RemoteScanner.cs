using System;

namespace NoMod.Memory
{
    internal sealed class RemoteScanner : Scanner
    {
        private readonly int _processId;

        public RemoteScanner(int processId, byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
            : base(bytes, startAddress, endAddress)
        {
            _processId = processId;
        }

        protected override IntPtr Next()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            // TODO: Close process handle.
        }
    }
}
