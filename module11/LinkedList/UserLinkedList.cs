namespace LinkedList
{
    class Node
    {
        public Node(User data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public User Data;
        public Node Next;
    }

    class UserLinkedList
    {
        private Node first = null!;

        public void AddFirst(User user)
        {
            Node node = new Node(user, first);
            first = node;
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

        public User GetLast()
        {
            while (first.Next != null)
            {
                first = first.Next;
            }
            return first.Data ?? null!;

        }

        public int CountUsers()
        {
            Node node = first;
            int count = 0;
            while (node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

        public override String ToString()
        {
            Node node = first;
            String result = "";
            while (node != null)
            {
                result += node.Data.Name + ", ";
                node = node.Next;
            }
            return result.Trim();
        }
        public bool Contains(User user)
        {
            while (first != null)
            {
                if (first.Data.Name == user.Name)
                {
                    return true;
                }
                first = first.Next;
            }
            return false!;
        }
    }
}