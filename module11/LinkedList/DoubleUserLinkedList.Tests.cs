using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedList.Tests
{
    [TestClass]
    public class DoubleLinkedList_Tests
    {
        [TestMethod]
        public void TestAddFirst()
        {
            User kristian = new User("Kristian", 1);
            User mads = new User("Mads", 2);
            User torill = new User("Torill", 3);

            DoubleUserLinkedList list = new DoubleUserLinkedList();
            list.AddFirst(kristian);
            Assert.AreEqual(kristian, list.GetFirst());
        }

        [TestMethod]
        public void TestRemoveFirst()
        {
            User kristian = new User("Kristian", 1);
            User mads = new User("Mads", 2);
            User torill = new User("Torill", 3);

            DoubleUserLinkedList list = new DoubleUserLinkedList();
            list.AddFirst(kristian);
            list.AddFirst(mads);
            list.AddFirst(torill);
            Assert.AreEqual(torill, list.RemoveFirst());
        }
        [TestMethod]
        public void TestRemoveLast()
        {
            User kristian = new User("Kristian", 1);
            User mads = new User("Mads", 2);
            User torill = new User("Torill", 3);

            DoubleUserLinkedList list = new DoubleUserLinkedList();
            list.AddFirst(kristian);
            list.AddFirst(mads);
            list.AddFirst(torill);
            Assert.AreEqual(kristian, list.RemoveLast());
        }
        [TestMethod]
        public void TestAddLast()
        {
            User kristian = new User("Kristian", 1);
            User mads = new User("Mads", 2);
            User torill = new User("Torill", 3);
            User asger = new User("Asger", 4);

            DoubleUserLinkedList list = new DoubleUserLinkedList();
            list.AddFirst(kristian);
            list.AddFirst(mads);
            list.AddFirst(torill);
            list.AddLast(asger);
            Assert.AreEqual(asger, list.GetLast());
        }
    }
}