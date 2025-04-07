using Hashing;

public class HashSetLinearProbing : HashSet
{
    private Object[] buckets;
    private int currentSize;
    private enum State { DELETED }

    public HashSetLinearProbing(int bucketsLength)
    {
        buckets = new Object[bucketsLength];
        currentSize = 0;
    }

    public bool Contains(Object x)
    {

        int h = HashValue(x);
        int i = h % buckets.Length;
        int start = i; // Remember where we started
        while (buckets[i] != null)
        {
            if (buckets[i].Equals(x))
            {
                return true; // Found it
            }
            i++;
            if (i >= buckets.Length)
            {
                i = 0; // Wrap around
            }
            if (i == start) // We have looped through the whole table
            {
                return false; // Not found
            }
        }



        return false;
    }

    public bool Add(Object x)
    {
        if (currentSize >= buckets.Length)
        {
            return false; // HashSet is full
        }

        int h = HashValue(x);
        int i = h % buckets.Length;
        while (buckets[i] != null && !buckets[i].Equals(State.DELETED))
        {
            if (buckets[i].Equals(x))
            {
                return false; // Already in the set
            }
            i++;
            if (i >= buckets.Length)
            {
                i = 0; // Wrap around
            }
        }
        buckets[i] = x;
        currentSize++;

        return false;
    }

    public bool Remove(Object x)
    {
        int h = HashValue(x);
        int i = h % buckets.Length;
        int start = i; // Remember where we started
        while (buckets[i] != null)
        {
            if (buckets[i].Equals(x))
            {
                buckets[i] = State.DELETED; // Mark as deleted
                currentSize--;
                return true; // Removed successfully
            }
            i++;
            if (i >= buckets.Length)
            {
                i = 0; // Wrap around
            }
            if (i == start) // We have looped through the whole table
            {
                break; // Not found
            }
        }
        return false;
    }

    public int Size()
    {
        return currentSize;
    }

    private int HashValue(Object x)
    {
        int h = x.GetHashCode();
        if (h < 0)
        {
            h = -h;
        }
        h = h % buckets.Length;
        return h;
    }

    public override String ToString()
    {
        String result = "";
        for (int i = 0; i < buckets.Length; i++)
        {
            int value = buckets[i] != null && !buckets[i].Equals(State.DELETED) ?
                    HashValue(buckets[i]) : -1;
            result += i + "\t" + buckets[i] + "(h:" + value + ")\n";
        }
        return result;
    }

}
