# HihutEx Memory Class
Simpler, yet faster way to read and write process memory externally.

### Features:
- Implements `ReadProcessMemory` and `WriteProcessMemory` with any type, class or structure,
- `ntdll` methods instead of `kernel32`,
- pointers,
- `System.Runtime.CompilerServices.Unsafe` class instead of `System.Runtime.InteropServices.Marshal` for marshalling.
