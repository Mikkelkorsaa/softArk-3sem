using System.Runtime.CompilerServices;

namespace LinkedList;

class DoubleUserLinkedList
{
    class Node
    {
        public Node(User data, Node next, Node previous)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = previous;

        }
        public User Data;
        public Node Next;
        public Node Previous;
    }
    private Node first = null!;
    private Node last = null!;
    public void AddFirst(User user)
    {
        Node firstNode = new Node(user, null!, null!);
        Node node = new Node(user, first, null!);
        if (first == null)
        {
            first = firstNode;
            last = firstNode;
        }
        else
        {
            first.Previous = node;
            first = node;
        }
    }
    public void AddLast(User user)
    {
        Node lastNode = new Node(user, null!, null!);
        Node node = new Node(user, null!, last);
        if (last == null)
        {
            first = lastNode;
            last = lastNode;
        }
        else
        {
            last.Next = node;
            last = node;
        }
    }
    public User RemoveFirst()
    {
        User user = first.Data;
        first = first.Next;
        first.Previous = null!;
        return user;
    }
    public User RemoveLast()
    {
        User user = last.Data;

        last = last.Previous;
        last.Next = null!;
        return user;
    }
    public User GetFirst()
    {
        return first.Data;
    }
    public User GetLast()
    {
        return last.Data;
    }
    public User GetUser(int index)
    {
        Node node = first;
        for (int i = 0; i < index; i++)
        {
            node = node.Next;
        }
        return node.Data;
    }
}