using Word = System.UInt16;
using DoubleWord = System.UInt32;

namespace TenCSharp.VirtualMachine
{
    public class ALU
    {
        public Word InputA { get; set; }
        public Word InputB { get; set; }
        
        public DoubleWord Output { get; set; }
        
        public void Set()
        {
            Output = InputB;
        }

        public void Add()
        {
            Output = (DoubleWord)(InputA + InputB);
        }

        public void Subtract()
        {
            unchecked
            {
                var complement = (DoubleWord)(-InputB);
                Output = (DoubleWord)InputA - InputB;
            }
        }

        public void Multiply()
        {
            Output = (DoubleWord)(InputA * InputB);
        }

        public void ShiftLeft()
        {
            Output = (DoubleWord)(InputA << InputB);
        }

        public void ShiftRight()
        {
            var lsb = InputA >> InputB;
            var msb = ((InputA << 16) >> InputB) & 0xffff;
            Output = (DoubleWord)((msb << 16) | lsb);
        }

        public void And()
        {
            Output = (DoubleWord)(InputA & InputB);
        }

        public void Or()
        {
            Output = (DoubleWord)(InputA | InputB);
        }

        public void Xor()
        {
            Output = (DoubleWord)(InputA ^ InputB);
        }
    }
}
