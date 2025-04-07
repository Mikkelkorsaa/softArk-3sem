using Hashing;

public class HashSetChaining : HashSet
{
    private Node[] buckets;
    private int currentSize;

    private class Node
    {
        public Node(Object data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public Object Data { get; set; }
        public Node Next { get; set; }
    }

    public HashSetChaining(int size)
    {
        buckets = new Node[size];
        currentSize = 0;
    }
    private Node[] ReHash(int newSize)
    {
        Node[] newBuckets = new Node[newSize];
        for (int i = 0; i < buckets.Length; i++)
        {
            Node temp = buckets[i];
            while (temp != null)
            {
                // Calculate new hash directly for new size
                int h = temp.Data.GetHashCode();
                if (h < 0) h = -h;
                h = h % newSize;

                Node newNode = new Node(temp.Data, newBuckets[h]);
                newBuckets[h] = newNode;
                temp = temp.Next;
            }
        }
        buckets = newBuckets;
        return buckets;
    }
    public bool Contains(Object x)
    {
        int h = HashValue(x);
        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }
        return found;
    }

    public bool Add(Object x)
    {
        int h = HashValue(x);
        int size = buckets.Length;
        float loadFactor = currentSize / size;
        if (loadFactor > 0.75)
        {
            ReHash(size * 2);
        }

        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }

        if (!found)
        {
            Node newNode = new Node(x, buckets[h]);
            buckets[h] = newNode;
            currentSize++;
        }

        return !found;
    }

    public bool Remove(Object x)
    {
        int h = HashValue(x);
        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Next != null && bucket.Next.Data.Equals(x))
            {
                found = true;
                bucket.Next = bucket.Next.Next;
                currentSize--;
            }
            else if (bucket.Data.Equals(x))
            {
                buckets[h] = bucket.Next!;
                currentSize--;
                {

                    return true;
                }


            }
            else
            {
                bucket = bucket.Next!;
            }
        }
        return found;
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

    public int Size()
    {
        return currentSize;
    }

    public override String ToString()
    {
        String result = "";
        for (int i = 0; i < buckets.Length; i++)
        {
            Node temp = buckets[i];
            if (temp != null)
            {
                result += i + "\t";
                while (temp != null)
                {
                    result += temp.Data + " (h:" + HashValue(temp.Data) + ")\t";
                    temp = temp.Next;
                }
                result += "\n";
            }
        }
        return result;
    }
}
