using System;

namespace NoMod.Assembling.CETack
{
    public class CETackAssembler : IAssembler
    {
        public int TargetProcessId { get; private set; }

        public CETackAssembler(int processId)
        {
            TargetProcessId = processId;
        }

        public bool DataScans
        {
            get => true;
            set
            {
                if (!value)
                {
                    throw new NotSupportedException();
                }
            }
        }

        public ISymbolCollection Symbols => throw new NotImplementedException();

        public void Assemble(string script, bool enable)
        {
            throw new NotImplementedException();
        }
    }
}
