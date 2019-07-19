# hihutex-memory-class
Small, performant and easy to understand external memory management class.

### Features:
- Implements ReadProcessMemory and WriteProcessMemory with generic type parameters using `ntdll` methods (_NtReadVirtualMemory_ and _NtWriteVirtualMemory_) instead of `kernel32` functions
- Uses unsafe pointers and _System.Runtime.CompilerServices.Unsafe_ class instead of _System.Runtime.InteropServices.Marshal_ for marshalling.
