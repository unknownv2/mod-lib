using NoMod.Hooking;
using NoMod.Win32;

namespace NoMod.SpeedHack
{
    public class HookedSpeedHacker : ISpeedHacker
    {
        private readonly object Mutex = new object();

        private uint _initialRealTickCount, _initialFakeTickCount;
        private ulong _initialRealTickCount64, _initialFakeTickCount64;
        private long _initialRealPerformanceCounter, _initialFakePerformanceCounter;

        private IHook<Kernel32.GetTickCountT> _getTickCount;
        private IHook<Kernel32.GetTickCount64T> _getTickCount64;
        private IHook<Kernel32.QueryPerformanceCounterT> _queryPerformanceCounter;

        private double _multiplier = 1;

        public HookedSpeedHacker(IHooker hooker)
        {
            lock (Mutex)
            {
                _initialFakeTickCount = _initialRealTickCount = _getTickCount.Original();
                _initialFakeTickCount64 = _initialRealTickCount64 = _getTickCount64.Original();
                while (!_queryPerformanceCounter.Original(out _initialRealPerformanceCounter)) ;
                _initialFakePerformanceCounter = _initialRealPerformanceCounter;

                _getTickCount = hooker.CreateHook("kernel32.dll", "GetTickCount", new Kernel32.GetTickCountT(GetTickCount));
                _getTickCount64 = hooker.CreateHook("kernel32.dll", "GetTickCount64", new Kernel32.GetTickCount64T(GetTickCount64));
                _queryPerformanceCounter = hooker.CreateHook("kernel32.dll", "QueryPerformanceCounter", new Kernel32.QueryPerformanceCounterT(QueryPerformanceCounter));
            }
        }

        private void RecordInitialTimings()
        {
            _initialFakeTickCount = GetTickCount();
            _initialFakeTickCount64 = GetTickCount64();
            while (!QueryPerformanceCounter(out _initialFakePerformanceCounter)) ;

            _initialRealTickCount = _getTickCount.Original();
            _initialRealTickCount64 = _getTickCount64.Original();
            while (!_queryPerformanceCounter.Original(out _initialRealPerformanceCounter)) ;
        }

        public void SetSpeedMultiplier(double value)
        {
            lock (Mutex)
            {
                RecordInitialTimings();
                _multiplier = value;
            }
        }

        private uint GetTickCount()
        {
            lock (Mutex)
            {
                return _initialFakeTickCount + (uint)((_getTickCount.Original() - _initialRealTickCount) * _multiplier);
            }
        }

        private ulong GetTickCount64()
        {
            lock (Mutex)
            {
                return _initialFakeTickCount64 + (ulong)((_getTickCount64.Original() - _initialRealTickCount64) * _multiplier);
            }
        }

        private bool QueryPerformanceCounter(out long performanceCount)
        {
            lock (Mutex)
            {
                if (!_queryPerformanceCounter.Original(out performanceCount))
                {
                    return false;
                }
                performanceCount = _initialFakePerformanceCounter + (long)((performanceCount - _initialRealPerformanceCounter) * _multiplier);
                return true;
            }
        }

        public void Dispose()
        {
            lock (Mutex)
            {
                _getTickCount.Dispose();
                _getTickCount64.Dispose();
                _queryPerformanceCounter.Dispose();
            }
        }
    }
}
