using System.Runtime.CompilerServices;

namespace LinkedList;

class SortedUserLinkedList
{
    private Node first = null!;

    public void Add(User user)
    {
        if (first == null || user.Id < first.Data.Id)
        {
            Node newNode = new Node(user, null!);
            first = newNode;
            return;
        }
        Node current = first;
        while (current.Next != null && current.Next.Data.Id < user.Id)
        {
            current = current.Next;
        }
        Node node = new Node(user, current.Next);
        current.Next = node;
    }

    public User RemoveFirst()
    {
        User user = first.Data;
        first = first.Next;
        return user;
    }

    public void RemoveUser(User user)
    {
        Node node = first;
        Node previous = null!;
        bool found = false;

        while (!found && node != null)
        {
            if (node.Data.Name == user.Name)
            {
                found = true;
                if (node == first)
                {
                    RemoveFirst();
                }
                else
                {
                    previous.Next = node.Next;
                }
            }
            else
            {
                previous = node;
                node = node.Next;
            }
        }
    }

    public User GetFirst()
    {
        return first.Data;
    }

    public int CountUsers()
    {
        int count = 0;
        Node node = first;
        while (node != null)
        {
            count++;
            node = node.Next;
        }
        return count;
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