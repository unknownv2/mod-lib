namespace NoMod.Debugging
{
    public interface IThreadContextX86
    {
        // DebugRegisters
        uint Dr0 { get; set; }
        uint Dr1 { get; set; }
        uint Dr2 { get; set; }
        uint Dr3 { get; set; }
        uint Dr6 { get; set; }
        uint Dr7 { get; set; }

        // FloatingPoint
        IFloatingSaveArea FloatSave { get; }

        // Segments
        uint Gs { get; set; }
        uint Fs { get; set; }
        uint Es { get; set; }
        uint Ds { get; set; }

        // Integer
        uint Edi { get; set; }
        uint Esi { get; set; }
        uint Ebx { get; set; }
        uint Edx { get; set; }
        uint Ecx { get; set; }
        uint Eax { get; set; }
        
        // Control
        uint Ebp { get; set; }
        uint Eip { get; set; }
        uint Cs { get; set; }
        uint EFlags { get; set; }
        uint Esp { get; set; }
        uint Ss { get; set; }

        // ExtendedRegisters
        byte[] ExtendedRegisters { get; }
    }

    public interface IFloatingSaveArea
    {
        uint ControlWord { get; set; }
        uint StatusWord { get; set; }
        uint TagWord { get; set; }
        uint ErrorOffset { get; set; }
        uint ErrorSelector { get; set; }
        uint DataOffset { get; set; }
        uint DataSelector { get; set; }
        byte[] RegisterArea { get; }
        uint Cr0NpxState { get; set; }
    }
}
