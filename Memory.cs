using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace HihutEx
{
    public unsafe class Memory
    {
        private readonly nint processHandle;
        private static readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<nint, nuint, void*, nuint, nuint*, int> ntReadVirtualMemory;
        private static readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<nint, nuint, void*, nuint, nuint*, int> ntWriteVirtualMemory;

        static Memory()
        {
            nint ntdll = NativeLibrary.Load("ntdll.dll");

            ntReadVirtualMemory = (delegate* unmanaged[Stdcall, SuppressGCTransition]<nint, nuint, void*, nuint, nuint*, int>)
                NativeLibrary.GetExport(ntdll, "NtReadVirtualMemory");

            ntWriteVirtualMemory = (delegate* unmanaged[Stdcall, SuppressGCTransition]<nint, nuint, void*, nuint, nuint*, int>)
                NativeLibrary.GetExport(ntdll, "NtWriteVirtualMemory");
        }

        public Memory(nint processHandle) 
            => this.processHandle = processHandle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Read<T>(nuint address) where T : unmanaged
        {
            T result = default;
            ntReadVirtualMemory(processHandle, address, &result, (nuint)sizeof(T), null);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(nuint address, T value) where T : unmanaged
        {
            ntWriteVirtualMemory(processHandle, address, &value, (nuint)sizeof(T), null);
        }
    }
}
