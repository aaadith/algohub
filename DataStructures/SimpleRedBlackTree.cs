using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DataStructures.BST.RedBlack
{
    public enum Color {red, black };
    public class SimpleRedBlackTreeNode
    {
        public int key;
        public int val;
        public SimpleRedBlackTreeNode left, right;
        public int count;

        public Color color;

        public SimpleRedBlackTreeNode(int key, int val, Color color=Color.red)
        {
            this.key = key; this.val = val;
            left = right = null;
            this.color = color;
        }

        public bool IsRed()
        { return color == Color.red; }
    }

    public class SimpleRedBlackTree
    {
        public SimpleRedBlackTreeNode root;

        //same as simple bst
        public int Get(int key)
        {
            SimpleRedBlackTreeNode x = root;
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


        private SimpleRedBlackTreeNode RotateLeft(SimpleRedBlackTreeNode h)
        {
            SimpleRedBlackTreeNode x = h.right;
            h.right = x.left;
            x.left = h;
            x.color = h.color;
            h.color = Color.red;
            return x;
        }

        private SimpleRedBlackTreeNode RotateRight(SimpleRedBlackTreeNode h)
        {
            SimpleRedBlackTreeNode x = h.left;
            h.left = x.right;
            x.right = h;
            x.color = h.color;
            h.color = Color.red;
            return x;
        }

        private void FlipColor(SimpleRedBlackTreeNode h)
        {
            h.color = Color.red;
            h.left.color = h.right.color = Color.black;
        }

        public bool IsRed(SimpleRedBlackTreeNode x)
        {
            if (x == null)
                return false;
            return (x.color==Color.red);
        }

        public void Put(int key, int val)
        {
            root = Put(root,key,val);
            root.color = Color.black;
        }

        private SimpleRedBlackTreeNode Put(SimpleRedBlackTreeNode x, int key, int val)
        {
            if (x == null)
                return new SimpleRedBlackTreeNode(key,val, Color.red);
            int cmp = key.CompareTo(x.key);
            if (cmp == 0)
                x.val = val;
            if (cmp < 0)
                x.left = Put(x.left,key,val);
            if (cmp > 0)
                x.right = Put(x.right, key, val);

            if (!IsRed(x.left) && IsRed(x.right))
                x=RotateLeft(x);
            if (IsRed(x.left) && IsRed(x.left.left))
                x = RotateRight(x);
            if (IsRed(x.left) && IsRed(x.right))
                FlipColor(x);

            x.count = 1 + Size(x.left) + Size(x.right);
            return x;           
        }

        public int Size()
        {
            return Size(root);
        }

        public int Size(SimpleRedBlackTreeNode x)
        {
            if (x == null)
                return 0;
            return x.count;
        }

                public Queue Traverse()
        {
            Queue q=new Queue();
            Inorder(root,q);
            return q;
        }

        private void Inorder(SimpleRedBlackTreeNode x,Queue q)
        {
            if (x == null)
                return;
            Inorder(x.left, q);
            q.Enqueue(x);
            Inorder(x.right, q);
        }

        public SimpleRedBlackTreeNode Min()
        {
            return Min(root);
        }

        public SimpleRedBlackTreeNode Min(SimpleRedBlackTreeNode x)
        {
            while ((x != null)&&(x.left!=null))
                x = x.left;
            return x;
        }

        public int Max()
        {
            return Max(root);
        }

        public int Max(SimpleRedBlackTreeNode x)
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

        private void RangeSelect(SimpleRedBlackTreeNode x, int lo, int hi, Queue q)
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

        private SimpleRedBlackTreeNode Delete(SimpleRedBlackTreeNode x, int key)
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

                SimpleRedBlackTreeNode t = x;
                x = Min(t.right);
                x.right = DeleteMin(t.right);
                x.left = t.left;
                
            }

            //code to restore red-black property
            if (!IsRed(x.left) && IsRed(x.right))
                x = RotateLeft(x);
            if (IsRed(x.left) && IsRed(x.left.left))
                x = RotateRight(x);
            if (IsRed(x.left) && IsRed(x.right))
                FlipColor(x);
            //end of code to restore red-black property

            x.count = 1 + Size(x.left) + Size(x.right);
            return x;
        }

        public void DeleteMin()
        {
            DeleteMin(root);
        }

        public SimpleRedBlackTreeNode DeleteMin(SimpleRedBlackTreeNode x)
        {
            if (x.left == null)
                return x.right;
            x.left = DeleteMin(x.left);
            x.count = 1 + Size(x.left) + Size(x.right);
            return x;
        }


        public SimpleRedBlackTreeNode Floor(int key)
        {
            return Floor(root, key);
        }

        public SimpleRedBlackTreeNode Floor(SimpleRedBlackTreeNode x, int key)
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
                SimpleRedBlackTreeNode t = Floor(x.right, key);
                if (t != null)
                    return t;
                else 
                    return x;
            }
        }



        public SimpleRedBlackTreeNode Ceil(int key)
        {
            return Ceil(root, key);
        }

        public SimpleRedBlackTreeNode Ceil(SimpleRedBlackTreeNode x, int key)
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
                SimpleRedBlackTreeNode t = Ceil(x.left, key);
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

        public int Rank(SimpleRedBlackTreeNode x, int key)
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
