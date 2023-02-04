using System.Collections.Generic;

public class PerkTypeEqualityComparer : IEqualityComparer<IPerk>
{
    public bool Equals(IPerk x, IPerk y)
    {
        return x.GetType() == y.GetType();
    }

    public int GetHashCode(IPerk obj)
    {
        return obj.GetType().GetHashCode();
    }
}
