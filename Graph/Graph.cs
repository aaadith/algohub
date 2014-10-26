using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    class Node
    {
        private string val;

        //adjacency list
        public HashSet<Node> adj;

        public Node(string val)
        {
            this.val = val;
            adj = new HashSet<Node>();
        }

        public override string ToString()
        {
            return val;
        }
    }

    class Graph
    {
        public HashSet<Node> nodes;

        public Graph(HashSet<Node> nodes)
        {
            this.nodes = nodes;
        }
    }

}
