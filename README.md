# HihutEx Memory Class v2

Memory manipulation with native-like performance in C#.

The code has been updated _(v1 → v2)_ to move away from `DllImport` in favor of **C# 9 function pointers** (`delegate*`).

Perfect for external game trainers.

## Key changes

The core logic and philosophy remains the same (calling `ntdll.dll`), but the implementation has been optimized for best possible performance.

| Feature | v1 | v2 | Advantages |
| :--- | :--- | :--- | :--- |
| **Language** | C# 7.3 | **C# 9.0+** | Access to new C# type aliases - `nint` instead of `IntPtr` and `nuint` instead of `UIntPtr`. |
| **Runtime** | .NET Framework | **.NET 5.0+** | Required for `NativeLibrary` class.|
| **Invocation** | `[DllImport]` | **`delegate*`** | Function pointers allow us to bypass standard P/Invoke stub created by CLR, removing overhead.|
| **Garbage Collection** | GC Transition | **`[SuppressGCTransition]`** | Every time we call unmanaged code, C# switches its GC Mode from Cooperative to Preemptive, adding overhead. This attribute suppresses this transition.|
| **Memory** | `byte[]` allocation |  **`void*`** | Zero-copy stack pointers. We no longer create new object on each read.|

## Example usage

```csharp
// Get the process handle and init
Process process = Process.GetProcessesByName("game")[0];
Memory memory = new Memory((nint)process.Handle);

// Read single value
int health = memory.Read<int>((nuint)0x12345678);

// Read multiple values
[StructLayout(LayoutKind.Sequential)]
struct Vector3 { float x, y, z; }

Vector3 pos = memory.Read<Vector3>((nuint)0x12345678);
```