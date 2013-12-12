using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.BTree.Client
{
    public class SimpleBTreeClient
    {
        static SimpleBTree btree = new SimpleBTree();
        public static void Run()
        {
            TestSplitChild();
        }

        public static void TestSplitChild()
        {
            Console.WriteLine("a");
        }
    }

    public class TestRecord : Record
    {
        String field1;

        public override IComparable GetKey()
        {
            return field1;
        }

    }
}
