using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DataStructures.BST
{

    public class SimpleBSTClient
    {
        public static void Run()
        {
            SimpleBST bst = new SimpleBST();
            bst.Put(2,2);
            bst.Put(4, 4);
            bst.Put(3, 3);
            bst.Put(1, 1);

            //bst.Delete(2);

            Queue q = bst.Traverse();
            /*foreach (SimpleBSTNode a in q)
            {
                Console.WriteLine(a.key);
            }*/

            Console.WriteLine(bst.Ceil(-2).key);

            

            /*Console.WriteLine("min:{0}", bst.min().key);
            Console.WriteLine("min:{0}", bst.min(bst.root.right).key);
            Console.WriteLine("max:{0}", bst.max());

            q = bst.RangeSelect(2, 4);
            foreach (SimpleBSTNode a in q)
            {
                Console.WriteLine(a.key);
            }*/

        }
    }

    public class SimpleBSTNode
    {
        public int key;
        public int val;
        public SimpleBSTNode left, right;
        public int count;

        public SimpleBSTNode(int key, int val)
        {
            this.key = key; this.val = val;
            //left = null; right = null;
            left = right = null;
        }
    }

    public class SimpleBST
    {
        public SimpleBSTNode root;

        public void Put(int key, int val)
        {
            root = Put(root, key, val);
        }

        private SimpleBSTNode Put(SimpleBSTNode x, int key, int val)
        {
            if (x == null)
            {
                x = new SimpleBSTNode(key, val);
            }
            else
            {
                int cmp = key.CompareTo(x.key);
                if (cmp < 0)
                    x.left = Put(x.left, key, val);
                if (cmp > 0)
                    x.right = Put(x.right, key, val);
                if (cmp == 0)
                    x.val = val;
            }
            x.count = 1 + Size(x.left) + Size(x.right);
            return x;           
        }

        public int Size()
        {
            return Size(root);
        }

        public int Size(SimpleBSTNode x)
        {
            if (x == null)
                return 0;
            return x.count;
        }


        public int Get(int key)
        {
            SimpleBSTNode x = root;
            while (x != null)
            {
                int cmp = key.CompareTo(x.key);
                if (cmp == 0)
                    return x.val;
                if (cmp < 0)
                    x = x.left;
                if (cmp > 0)
                    x = x.right;
            }
            return -1; //null
        }

        public Queue Traverse()
        {
            Queue q=new Queue();
            Inorder(root,q);
            return q;
        }

        private void Inorder(SimpleBSTNode x,Queue q)
        {
            if (x == null)
                return;
            Inorder(x.left, q);
            q.Enqueue(x);
            Inorder(x.right, q);
        }

        public SimpleBSTNode Min()
        {
            return Min(root);
        }

        public SimpleBSTNode Min(SimpleBSTNode x)
        {
            while ((x != null)&&(x.left!=null))
                x = x.left;
            return x;
        }

        public int Max()
        {
            return Max(root);
        }

        public int Max(SimpleBSTNode x)
        {
            while ((x != null) && (x.right != null))
                x = x.right;
            return x.key;
        }

        public Queue RangeSelect(int lo, int hi)
        {
            Queue q = new Queue();
            RangeSelect(root, lo, hi,q);
            return q;
        }

        private void RangeSelect(SimpleBSTNode x, int lo, int hi, Queue q)
        {
            if (x == null)
                return;
            if (x.key.CompareTo(lo) < 0) //lower bound of interval more than current node
                RangeSelect(x.right, lo, hi,q);
            else if (x.key.CompareTo(hi) > 0) //upper bound of interval less than current node
                RangeSelect(x.left, lo, hi,q);
            else
            {
                //Inorder(x, q);
                RangeSelect(x.left, lo, hi, q);
                q.Enqueue(x);
                RangeSelect(x.right, lo, hi, q);
            }
        }

        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        //Hibbard deletion : replace node to be deleted with smallest node in right subtree and make appropriate adjustments
        //Problem with Hibbard deletion : it is assymetric. After a series of insertions and deletions with this technique, 
        //the height of the tree becomes sqrt(N) as opposed to log(N). It might make a difference b/w acceptable and unacceptable
        //solution in real world applications. Simple and efficient deletion for BSTs is a long standing open problem. - Robert Sedgewick

        private SimpleBSTNode Delete(SimpleBSTNode x, int key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.key);

            if (cmp < 0)
                x.left = Delete(x.left, key);
            else if (cmp > 0)
                x.right = Delete(x.right, key);
            else
            {//cmp==0 => x is the node to be deleted

                if (x.right == null)
                    return x.left;

                SimpleBSTNode t = x;
                x = Min(t.right);
                x.right = DeleteMin(t.right);
                x.left = t.left;
                
            }
            x.count = 1 + Size(x.left) + Size(x.right);
            return x;
        }

        public void DeleteMin()
        {
            DeleteMin(root);
        }

        public SimpleBSTNode DeleteMin(SimpleBSTNode x)
        {
            if (x.left == null)
                return x.right;
            x.left = DeleteMin(x.left);
            x.count = 1 + Size(x.left) + Size(x.right);
            return x;
        }


        public SimpleBSTNode Floor(int key)
        {
            return Floor(root, key);
        }

        public SimpleBSTNode Floor(SimpleBSTNode x, int key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.key);

            if (cmp == 0)
                return x;
            else if (cmp < 0)
                return Floor(x.left, key);
            else
            {   //reached a node whose key is less than input key
                //check if the right subtree of this node.
                //if floor in the right subtree is null, then this node's key is the floor
                SimpleBSTNode t = Floor(x.right, key);
                if (t != null)
                    return t;
                else 
                    return x;
            }
        }



        public SimpleBSTNode Ceil(int key)
        {
            return Ceil(root, key);
        }

        public SimpleBSTNode Ceil(SimpleBSTNode x, int key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.key);

            if (cmp == 0)
                return x;
            else if (cmp > 0)
                return Ceil(x.right, key);
            else
            {   //reached a node whose key is more than input key
                //check if the right subtree of this node.
                //if floor in the right subtree is null, then this node's key is the floor
                SimpleBSTNode t = Ceil(x.left, key);
                if (t != null)
                    return t;
                else
                    return x;
            }
        }


        //returns the number of nodes in the tree whose keys are lesser than given key

        public int Rank(int key)
        {
            return Rank(root, key);
        }

        public int Rank(SimpleBSTNode x, int key)
        {
            if (x == null)
                return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0)
                return Size(x.left);
            else if (cmp < 0)
                return Rank(x.left, key);
            else
            {
                return 1 + Size(x.left) + Rank(x.right, key);
            }
        }


        public int RangeCount(int lo, int hi)
        {
            if (Get(hi) != -1)
                return Rank(hi) - Rank(lo) + 1;
            else
                return Rank(hi) - Rank(lo);
        }

    }
}
