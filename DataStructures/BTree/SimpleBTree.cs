using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{

    public abstract class Record
    {
        public abstract IComparable GetKey();
    }

    public class SimpleBTreeNode
    {
        int MinDegree; //min number of children. (t)
        //Every node other than root must have at least t-1 keys.
        //if the tree is non-empty, the root must have at least one key.
        //Every internal node other than root must have at least t children.

        //Every node can have at most 2t-1 keys
        //When it has 2t-1 keys, the node is considered full.
        


        public int NumKeys; //no of keys currently stored in the node
        public bool IsLeaf;

        public Record[] records; //will store records by non-decreasing order of keys
        //referred to in literature as key. This implementation stores data associated 
        //with key along with the key, hence it is called record. Ever record is required 
        // to have a key. Within the BTree, all operations on records will be with
        //reference to the key.


        public SimpleBTreeNode[] child; //references to children

        public SimpleBTreeNode(int MinDegree, bool IsLeaf)
        {
            this.MinDegree = MinDegree;
            this.IsLeaf = IsLeaf;
            NumKeys = 0;

            records=new Record[2*MinDegree-1];
            child = new SimpleBTreeNode[2*MinDegree];
        }


        public bool IsFull()
        {
            return NumKeys == 2 * MinDegree - 1;
        }

        public void Insert(Record record)
        {

        }
        
    }


    public class SimpleBTree
    {
        SimpleBTreeNode root;
        int MinDegree;

        public SimpleBTree(int MinDegree=2)
        {
            this.MinDegree = MinDegree;
            root = new SimpleBTreeNode(MinDegree, true);
        }

        public Record Search(IComparable key)
        {
            return Search(root, key);
        }

        public Record Search(SimpleBTreeNode node, IComparable key)
        {
            int i;
            for (i = 0; i < node.NumKeys; i++)
            {
                int cmp = key.CompareTo(node.records[i].GetKey());
                if (cmp == 0)
                    return node.records[i];
                if (cmp > 1) //key > record.key
                    break;
            }
            if (node.IsLeaf)
                return null;
            else
            {
                return Search(node.child[i],key);
            }
        }


        //x is the parent of y - the node that is full and needs to be split
        //max keys = 2t-1, which will always be an odd number.
        //so there will always be 1 median key (as against two if the number of keys in thhe node were even) 
        //around which the split will happen.
        //median key will go to parent.
        //all keys to the right of median will go to a new node
        //all links to the right of median's position+1 will go to a new node
        //parent will get a link to the new node at specified position+1
        public void SplitChild(SimpleBTreeNode x, int position, SimpleBTreeNode y)
        {
            //creating new node z, resulting from split of y
            SimpleBTreeNode z = new SimpleBTreeNode(MinDegree, y.IsLeaf);

            //y has 2t-1 keys. After the split, it should have t-1 keys.
            //First t-1 keys of y to be left intact. t-th key will go to parent.
            //Remaining t-1 keys will go to z. (t-1)+1+(t-1)=2t-1
            //so, (t+1)th key to (2t-1)th key in y have to be moved to z.
            //ie, y.records[t] to y.records[2t-2] to be moved to z (because of 0-based indexing of arrays)
            for (int i = MinDegree; i <= 2 * MinDegree - 2; i++)
            {
                z.records[i - MinDegree] = y.records[i];
            }

            //If y is an internal node, it would have links pointing to children which would
            //have to be split between y and z.           
            if (!y.IsLeaf)
            {
                //y is full. It would have 2t links. [1 to the left of each of 2t-1 keys +1 to the right of last key]
                //t-1 keys to be retained in y, t-th key will go to parent and everything to the right goes to z.
                //similarly, first t-1 links will remain in y.
                //t-th link, which is the link to the left of t-th key will remain in y. it will be y's last link.
                //All links to the right of t-th link will go to z.
                //ie, (t+1)th link to (2t)th link will go to z
                //ie, y.child[t] to y.child[2t-1] will be moved to z.

                for (int i = MinDegree; i <= 2 * MinDegree - 1; i++)
                {
                    z.child[i - MinDegree] = y.child[i];
                }

            }
            y.NumKeys= z.NumKeys = MinDegree - 1;

            //make space in the parent for the new key
            for (int i = x.NumKeys-1; i >= position; i--)
            {
                x.records[i + 1] = x.records[i];
            }
            //insert new key at specified position
            x.records[position] = y.records[MinDegree];

            //make space in the larent for the new link
            //(postion+1)th link inparent should point to z
            //(position+1)th to (NumKeys+1)th links of parent to be moved by 1 position
            //x.child[position+1] to x.child[NumKeys] to be moved by 1 position (caution : position is 0-based index; so no need to adjust)
            for (int i = x.NumKeys; i >= position+1; i--)
            {
                x.child[i + 1] = x.child[i];
            }
            //insert link to z to the right of new key
            x.child[position+1] = z;
        }



        public void Insert(Record record)
        {
            if (root.IsFull())
            {
                SimpleBTreeNode newroot = new SimpleBTreeNode(MinDegree, false);
                newroot.child[0] = root;
                root = newroot;
                SplitChild(root, 0, root.child[0]);
                Insert(root, record);
            }
            else
                Insert(root, record);
        }

        public void Insert(SimpleBTreeNode x, Record record)
        {
            int i = x.NumKeys;
            if (x.IsLeaf)
            {                
                while (i >= 0)
                {
                    int cmp = record.GetKey().CompareTo(x.records[i].GetKey());
                    if (cmp > 0)
                        break;
                    x.records[i + 1] = x.records[i];
                    i--;
                }
                x.records[i+1] = record;
                x.NumKeys++;
            }
            else
            {
                while (i >= 0)
                {
                    int cmp = record.GetKey().CompareTo(x.records[i].GetKey());
                    if (cmp > 0)
                        break;
                    i--;
                }
                //the loop would have i point to the first key in the node that is smaller than the new record's key
                //link to the right of this key would point to the node where the new record would have to be inserted
                //so we increment i to point to the appropriate child to traverse to.
                i = i + 1;
                SimpleBTreeNode child = x.child[i];
                if (child.IsFull())
                {
                    SplitChild(x,i,child);
                    //i will now have new key after split
                    int cmp = record.GetKey().CompareTo(x.records[i].GetKey());
                    //if the record's key is more than newly added key in current node,
                    //the new record will have to go to the child to the right of the new key
                    //so increment i by 1
                    if (cmp > 0)
                    {
                        i = i + 1;
                    }
                }
                Insert(x.child[i],record);
            }
        }


        public void Delete(Record record)
        {
            Delete(root, record);
        }

        //recordPosition : optional parameter holding position of record to be deleted
        public void Delete(SimpleBTreeNode x, Record record, int recordPosition=-1)
        {
            int position=(recordPosition==-1)?0:recordPosition;
            int cmp=-1;
            bool RecordPresentInNode = false;
            
            //traverse through all elements in current node until we either find the required record
            // or find a record with key greater than required key (which means the current node
            //does not contain the required key)
            while( (position < x.NumKeys) && (cmp = record.GetKey().CompareTo(x.records[position].GetKey())) <=0 )
            {
                if (cmp == 0)
                {
                    RecordPresentInNode = true;
                    break;
                }
                position++;
            }
            //after execution of the loop, if cmp is 0, it means the required key was found; position variable
            //would contain the position of the required key.

            if (RecordPresentInNode)
            {
                if (x.IsLeaf)
                {
                    DeleteFromLeafNode(x, position);
                }
                else
                {
                    DeleteFromInternalNode(x, position);
                }
            }
            else
            {
                if (x.IsLeaf)
                {
                    //reached the leaf node of the tree and required key was found anwhere in the tree.
                    //record not present in the tree; deletion is a no-op. return
                    return;
                }
                else
                {
                    DeleteFromSubTree(x, record, position);
                }
            }
        }

        // position : position of the node to be deleted
        public void DeleteFromLeafNode(SimpleBTreeNode x, int position)
        {
            for (int i = position; i < x.NumKeys-1; i++)
            {
                x.records[i] = x.records[i + 1];
            }
            x.NumKeys--;       
        }


        // position : position of the node to be deleted
        // left child of the node to be deleted => x.child[position]
        // right child of the node to be deleted => x.child[position+1]
        public void DeleteFromInternalNode(SimpleBTreeNode x, int position)
        {
            SimpleBTreeNode LeftChild = x.child[position];
            SimpleBTreeNode RightChild = x.child[position];

            Record record = x.records[position];

            //if the left child has at least t keys
            if(LeftChild.NumKeys>=MinDegree)
            {
                int PositionOfLastElementOfLeftChild = LeftChild.NumKeys - 1;
                Record LastElementOfLeftChild = LeftChild.records[PositionOfLastElementOfLeftChild];

                //replace record to be deleted with its predecessor from left child,
                //which would be the last element in the left child
                x.records[position] = LastElementOfLeftChild;

                //recursively delete the predecessor from left child
                Delete(LeftChild, LastElementOfLeftChild, PositionOfLastElementOfLeftChild);
            }
            //if the right child has at least t keys
            else if(RightChild.NumKeys>=MinDegree)
            {
                int PositionOfFirstElementOfRightChild = LeftChild.NumKeys - 1;
                Record FirstElementOfRightChild = LeftChild.records[PositionOfFirstElementOfRightChild];

                //replace record to be deleted with its successor from right child,
                //which would be the first element in the right child
                x.records[position] = FirstElementOfRightChild;

                //recursively delete the successor from right child
                Delete(LeftChild, FirstElementOfRightChild, PositionOfFirstElementOfRightChild);

            }
            //if both left and right children have less than t keys
            else
            {


                //move record to be deleted and all records from right child to the left child
                
                //---TODO : to evaluate if this section is needed. y move the record to be deleted to left child? y not directly delete from current node and do aways with it?---
                LeftChild.records[LeftChild.NumKeys] = record;                
                LeftChild.NumKeys++;
                //--------------------------------------------------

                for (int i = 0; i < RightChild.NumKeys; i++)
                {
                    LeftChild.records[LeftChild.NumKeys + i] = RightChild.records[i];
                }

                for (int i = 0; i < RightChild.NumKeys + 1; i++)
                {
                    LeftChild.child[LeftChild.NumKeys + i] = RightChild.child[i];
                }
                LeftChild.NumKeys += RightChild.NumKeys;

                //remove record and right child from current node
                for(int i=position;i<x.NumKeys-1;i++)
                {
                    x.records[i] = x.records[i + 1];
                    x.child[i + 1] = x.child[i + 2];
                }
                x.NumKeys--;

                //recursively delete record from left child
                Delete(LeftChild, record);

            }

        }

        //position : position of the child that would contain the record to be deleted
        //if at all the record is present in the tree
        public void DeleteFromSubTree(SimpleBTreeNode x, Record record, int position)
        {
            SimpleBTreeNode childToDescendTo = x.child[position];

            //if the node to descend to has t-1 children, make adjustments to ensure 
            //it has at least t keys before we descend to it
            if (childToDescendTo.NumKeys == MinDegree - 1)
            {
                SimpleBTreeNode LeftSibling = (position-1>=0)?x.child[position - 1]:null;
                SimpleBTreeNode RightSibling = (position+1<x.NumKeys)?x.child[position + 1]:null;

                //if left sibling has at least t elements
                if( (LeftSibling!=null)  && (LeftSibling.NumKeys >= MinDegree))
                {
                    //get handle of record in between the child and its left sibling
                    Record recordFromParent = x.records[position - 1];

                    //get handle of predecessor of record from parent. this will be moved to parent
                    Record recordFromLeftSibling = LeftSibling.records[LeftSibling.NumKeys - 1];
                    LeftSibling.records[LeftSibling.NumKeys - 1] = null;

                    //get handle of last child of left sibling
                    SimpleBTreeNode LastChildFromLeftSibling = LeftSibling.child[LeftSibling.NumKeys];
                    LeftSibling.child[LeftSibling.NumKeys] = null;

                    for (int i = childToDescendTo.NumKeys; i >0 ; i--)
                    {
                        childToDescendTo.records[i] = childToDescendTo.records[i - 1];
                    }

                    for (int i = childToDescendTo.NumKeys+1; i > 0; i--)
                    {
                        childToDescendTo.child[i + 1] = childToDescendTo.child[i];
                    }

                    childToDescendTo.records[0] = recordFromParent;
                    x.records[position -1]=recordFromLeftSibling;
                    childToDescendTo.child[0] = LastChildFromLeftSibling;
                    

                    childToDescendTo.NumKeys++;
                    LeftSibling.NumKeys--;
                    
                }
                //if right sibling has at least t elements
                else if ( (RightSibling!=null) && (RightSibling.NumKeys >= MinDegree) )
                {

                    //get handle of record in between the child and its right sibling
                    Record recordFromParent = x.records[position + 1];

                    //get handle of successor of record from parent. this will be moved to parent
                    Record recordFromRightSibling = RightSibling.records[0];
                    

                    //get handle of first child of right sibling
                    SimpleBTreeNode FirstChildFromRightSibling = RightSibling.child[0];
                    

                    for (int i = 0; i < childToDescendTo.NumKeys; i++)
                    {
                        RightSibling.records[i] = RightSibling.records[i + 1];
                    }

                    for (int i = 0; i < childToDescendTo.NumKeys + 1; i++)
                    {
                        RightSibling.child[i] = RightSibling.child[i+1];
                    }
                    RightSibling.NumKeys--;

                    childToDescendTo.records[childToDescendTo.NumKeys] = recordFromParent;
                    childToDescendTo.NumKeys++;
                    x.records[position + 1] = recordFromRightSibling;
                    childToDescendTo.child[childToDescendTo.NumKeys+1] = FirstChildFromRightSibling;


                }
                //if neither siblings have at least t elements
                else
                {
                    if (RightSibling != null)
                    {
                        childToDescendTo.records[childToDescendTo.NumKeys] = x.records[position + 1];
                        childToDescendTo.NumKeys++;
                        for (int i = 0; i < RightSibling.NumKeys; i++)
                        {
                            childToDescendTo.records[childToDescendTo.NumKeys + i] = RightSibling.records[i];
                        }

                        for (int i = 0; i < RightSibling.NumKeys + 1; i++)
                        {
                            childToDescendTo.child[childToDescendTo.NumKeys + i] = RightSibling.child[i];
                        }
                        childToDescendTo.NumKeys += RightSibling.NumKeys;

                        for (int i = position; i < x.NumKeys-1; i++)
                        {
                            x.records[i] = x.records[i + 1];
                        }
                        for (int i = position + 1; i < x.NumKeys; i++)
                        {
                            x.child[i] = x.child[i + 1];
                        }
                        x.NumKeys--;
                    }
                    else if (LeftSibling != null)
                    {


                        LeftSibling.records[LeftSibling.NumKeys] = x.records[position - 1];
                        LeftSibling.NumKeys++;
                        for (int i = 0; i < childToDescendTo.NumKeys; i++)
                        {
                            LeftSibling.records[LeftSibling.NumKeys + i] = childToDescendTo.records[i];
                        }

                        for (int i = 0; i < childToDescendTo.NumKeys + 1; i++)
                        {
                            LeftSibling.child[LeftSibling.NumKeys + i] = childToDescendTo.child[i];
                        }
                        LeftSibling.NumKeys += childToDescendTo.NumKeys;

                        for (int i = position-1; i < x.NumKeys - 1; i++)
                        {
                            x.records[i] = x.records[i + 1];
                        }
                        for (int i = position; i < x.NumKeys; i++)
                        {
                            x.child[i] = x.child[i + 1];
                        }
                        x.NumKeys--;

                        childToDescendTo = LeftSibling;
                    }
                }
            }

            Delete(childToDescendTo, record);
        }

    }
}