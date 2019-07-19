using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Memory
{
    [SuppressUnmanagedCodeSecurity]
    public class Memory
    {
        [DllImport("ntdll")]
        private static extern bool NtReadVirtualMemory(
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            byte[] Buffer,
            int NumberOfBytesToRead,
            int NumberOfBytesRead);

        [DllImport("ntdll")]
        private static extern bool NtWriteVirtualMemory(
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            byte[] Buffer,
            int NumberOfBytesToWrite,
            int NumberOfBytesWritten);

        private readonly IntPtr processHandle;

        public Memory(IntPtr processHandle) 
            => this.processHandle = processHandle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T Read<T>(IntPtr address) where T : struct
        {
            byte[] buffer = new byte[Unsafe.SizeOf<T>()];

            NtReadVirtualMemory(processHandle, address, buffer, buffer.Length, 0);

            fixed (byte* b = buffer)
                return Unsafe.Read<T>(b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe bool Write<T>(IntPtr address, T value) where T : struct
        {
            byte[] buffer = new byte[Unsafe.SizeOf<T>()];

            fixed (byte* b = buffer)
                Unsafe.Write<T>(b, value);

            return NtWriteVirtualMemory(processHandle, address, buffer, buffer.Length, 0);
        }
    }
}
