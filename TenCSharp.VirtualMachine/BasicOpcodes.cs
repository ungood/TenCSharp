namespace TenCSharp.VirtualMachine
{
    public enum BasicOpcode : byte
    {
        Set = 0x01,
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulus,
        ShiftLeft,
        ShiftRight,
        And,
        Or,
        Xor,
        IfEqual,
        IfNotEqual,
        IfGreater,
        Ifb // ?
    }
}
