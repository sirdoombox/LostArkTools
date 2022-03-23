using System.Collections;

namespace LostArkTools.Misc;

public class ByteArrayComparer : IEqualityComparer<byte[]>
{
    public bool Equals(byte[] x, byte[] y) =>
        GetHashCode(x) == GetHashCode(y);

    public int GetHashCode(byte[] obj) =>
        ((IStructuralEquatable)obj).GetHashCode(EqualityComparer<byte>.Default);
}