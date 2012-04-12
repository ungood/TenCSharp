using System;
using System.Collections.Generic;

using Word = System.UInt16;

namespace TenCSharp.VirtualMachine
{
    public partial class Processor
    {
        private IEnumerable<Action> Decode(Word instruction)
        {
            var opcode = (byte)(instruction & 0x000F);

            return opcode == 0
                ? DecodeNonBasic(instruction) 
                : DecodeBasic(opcode, instruction);
        }

        private IEnumerable<Action> DecodeBasic(byte opcode, ushort instruction)
        {
            var valueA = (byte)((instruction >> 4) & 0x3f);
            foreach(var micro in DecodeGetValue(value => ALU.InputA = value, valueA))
                yield return micro;
            
            var valueB = (byte)((instruction >> 10) & 0x3f);
            foreach(var micro in DecodeGetValue(value => ALU.InputB = value, valueB))
                yield return micro;

            switch ((BasicOpcode)opcode)
            {
                case BasicOpcode.Set:
                    yield return ALU.Set;
                    break;
                case BasicOpcode.Add:
                    yield return ALU.Add;
                    yield return SetOverflow;
                    break;
                case BasicOpcode.Subtract:
                    yield return ALU.Subtract;
                    yield return SetOverflow;
                    break;
                case BasicOpcode.Multiply:
                    yield return ALU.Multiply;
                    yield return SetOverflow;
                    break;
                    //case 0x05:
                    //    return new DivideInstruction();
                    //case 0x06:
                    //    return new ModulusInstruction();
                case BasicOpcode.ShiftLeft:
                    yield return ALU.ShiftLeft;
                    yield return SetOverflow;
                    break;
                case BasicOpcode.ShiftRight:
                    yield return ALU.ShiftRight;
                    yield return SetOverflow;
                    break;
                case BasicOpcode.And:
                    yield return ALU.And;
                    break;
                case BasicOpcode.Or:
                    yield return ALU.Or;
                    break;
                case BasicOpcode.Xor:
                    yield return ALU.Xor;
                    break;
                default:
                    throw new NotImplementedException();
            }

            foreach(var micro in DecodeSetValue(valueA, (Word)ALU.Output))
                yield return micro;
        }

        private IEnumerable<Action> DecodeGetValue(Action<Word> set, byte encoded)
        {
            if(0x00 <= encoded && encoded <= 0x07)
            {
                set(Registers[encoded]);
                yield break;
            }

            if(0x08 <= encoded && encoded <= 0x0f)
            {
                var address = Registers[encoded - 0x08];
                yield return () => set(Memory[address]);
                yield break;
            }

            if(0x10 <= encoded && encoded <= 0x17)
            {
                var address = (Word)(Registers[(encoded - 0x10)] + NextWord);
                yield return () => set(Memory[address]);
                yield break;
            }

            switch(encoded)
            {
                case 0x18:
                    set(Memory[--StackPointer]);
                    yield break;
                case 0x19:
                    set(Memory[StackPointer]);
                    yield break;
                case 0x1a:
                    set(Memory[StackPointer++]);
                    yield break;
                case 0x1b:
                    set(StackPointer);
                    yield break;
                case 0x1c:
                    set(ProgramCounter);
                    yield break;
                case 0x1d:
                    set(Overflow);
                    yield break;
                case 0x1e:
                    yield return () => set(Memory[NextWord]);
                    yield break;
                case 0x1f:
                    set(NextWord);
                    yield break;
                default:
                    set((Word)(encoded - 0x20));
                    yield break;
            }
        }

        private IEnumerable<Action> DecodeSetValue(byte encoded, Word value)
        {
            if(0x00 <= encoded && encoded <= 0x07)
            {
                Registers[encoded] = value;
                yield break;
            }

            if(0x08 <= encoded && encoded <= 0x0f)
            {
                var address = Registers[encoded - 0x08];
                yield return () => Memory[address] = value;
                yield break;
            }

            if(0x10 <= encoded && encoded <= 0x17)
            {
                var address = (Word)(Registers[(encoded - 0x10)] + NextWord);
                yield return () => Memory[address] = value;
                yield break;
            }

            switch(encoded)
            {
                case 0x18:
                    Memory[--StackPointer] = value;
                    yield break;
                case 0x19:
                    Memory[StackPointer] = value;
                    yield break;
                case 0x1a:
                    Memory[StackPointer++] = value;
                    yield break;
                case 0x1b:
                    StackPointer = value;
                    yield break;
                case 0x1c:
                    ProgramCounter = value;
                    yield break;
                case 0x1d:
                    Overflow = value;
                    yield break;
                case 0x1e:
                    yield return () => Memory[NextWord] = value;
                    yield break;
                case 0x1f:
                    yield break;
                default:
                    yield break;
            }
        }

        private IEnumerable<Action> DecodeNonBasic(Word instruction)
        {
            // Extension: HALT
            if(instruction == 0)
            {
                yield return () => Halt = true;
                yield break;
            }

            throw new NotImplementedException();
        }
    }
}
