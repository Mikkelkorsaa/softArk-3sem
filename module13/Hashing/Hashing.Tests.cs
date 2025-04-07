using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hashing.Tests
{
    [TestClass]
    public class Hashing_Tests
    {
        [TestMethod]
        public void TestAddChaining()
        {
            HashSetChaining names = new HashSetChaining(20);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Add("Eve");
            names.Add("Sarah");
            names.Add("Adam");
            names.Add("Tony");
            names.Add("Katherine");
            names.Add("Juliet");
            names.Add("Romeo");
            Console.WriteLine(names.Size());
            Assert.AreEqual(12, names.Size());
        }
        [TestMethod]
        public void TestAddChaining2()
        {
            HashSetChaining names = new HashSetChaining(4);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Add("Eve");
            names.Add("Sarah");
            names.Add("Adam");
            names.Add("Tony");
            names.Add("Katherine");
            names.Add("Juliet");
            names.Add("Romeo");
            Console.WriteLine(names.Size());
            Assert.AreEqual(12, names.Size());
        }
        [TestMethod]
        public void TestRehashing()
        {
            // Create a very small hash set (size 2) to force rehashing
            HashSetChaining hashSet = new HashSetChaining(2);

            // Add enough elements to trigger rehashing
            hashSet.Add("One");   // 1/2 = 0.5 load factor
            hashSet.Add("Two");   // 2/2 = 1.0 load factor - should trigger rehash to size 4
            hashSet.Add("Three"); // 3/4 = 0.75 load factor
            hashSet.Add("Four");  // 4/4 = 1.0 load factor - should trigger rehash to size 8
            hashSet.Add("Five");  // 5/8 = 0.625 load factor

            // Verify all elements are present after rehashing
            Assert.IsTrue(hashSet.Contains("One"));
            Assert.IsTrue(hashSet.Contains("Two"));
            Assert.IsTrue(hashSet.Contains("Three"));
            Assert.IsTrue(hashSet.Contains("Four"));
            Assert.IsTrue(hashSet.Contains("Five"));

            // Verify count is correct
            Assert.AreEqual(5, hashSet.Size());

            // Print the hash set for visual inspection
            Console.WriteLine(hashSet.ToString());
        }

        [TestMethod]
        public void TestRemoveChaining()
        {
            HashSetChaining names = new HashSetChaining(13);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Remove("Larry");
            names.Remove("Nina");
            Console.WriteLine(names.Size());
            Assert.AreEqual(3, names.Size());
        }

        [TestMethod]
        public void TestContainsChaining()
        {
            HashSetChaining names = new HashSetChaining(4);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Add("Eve");
            names.Add("Sarah");
            names.Add("Adam");
            names.Add("Tony");
            names.Add("Katherine");
            names.Add("Juliet");
            names.Add("Romeo");
            Assert.IsTrue(names.Contains("Nina"));
            Assert.IsTrue(names.Contains("Romeo"));
            Assert.IsTrue(names.Contains("Sarah"));

            names.Remove("Romeo");
            Assert.IsFalse(names.Contains("Romeo"));
        }

        [TestMethod]
        public void TestAddLinearProbing()
        {
            HashSetLinearProbing names = new HashSetLinearProbing(13);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Add("Eve");
            names.Add("Sarah");
            names.Add("Adam");
            names.Add("Tony");
            names.Add("Katherine");
            names.Add("Juliet");
            names.Add("Romeo");
            Assert.AreEqual(12, names.Size());
        }

        [TestMethod]
        public void TestRemoveLinearProbing()
        {
            HashSetLinearProbing names = new HashSetLinearProbing(13);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            names.Remove("Larry");
            names.Remove("Nina");
            Assert.AreEqual(3, names.Size());
        }

        [TestMethod]
        public void TestContainsLinearProbing()
        {
            HashSetLinearProbing names = new HashSetLinearProbing(5);
            names.Add("Harry");
            names.Add("Sue");
            names.Add("Nina");
            names.Add("Susannah");
            names.Add("Larry");
            Console.WriteLine(names.Size());
            Console.WriteLine(names.ToString());
            Assert.IsTrue(names.Contains("Nina"));
            Assert.IsTrue(names.Contains("Harry"));

            names.Remove("Sue");
            Assert.IsFalse(names.Contains("Sue"));
        }
    }
}