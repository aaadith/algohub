
/*
 * Given a node in a graph, finds all the other nodes in the graph that are reachable from the given node
 * 
 */

using System;
using System.Collections.Generic;

namespace Graph
{
    class ConnectionsFinder
    {
        HashSet<Node> visited = new HashSet<Node>(); 
        Dictionary<Node, Node> visitedFrom = new Dictionary<Node, Node>();

        
        private Node node;
        public ConnectionsFinder(Node node)
        {
            this.node = node;
            FindConnections();
        }

        private void FindConnections()
        {
            dfs(node);
        }

        private void dfs(Node node)
        {
            visited.Add(node);
            if (node.adj != null)
            {
                foreach (var reachableNode in node.adj)
                {
                    if (!visited.Contains(reachableNode))
                    {
                        dfs(reachableNode);
                        visitedFrom[reachableNode] = node;
                    }
                }
            }
        }

        public bool isReachable(Node x)
        {
            return visited.Contains(x);
        }

        public IEnumerable<Node> GetPathTo(Node x)
        {
            if(!visited.Contains(x))
                throw new Exception("unreachable node");

            Stack<Node> path = new Stack<Node>();

            for (Node current = x; current!=node; current = visitedFrom[current])
            {
                path.Push(current);
            }
            path.Push(node);
            return path;
        }
 


    }


    class ConnectionsFinderClient
    {

        public static void Run()
        {
            Node a = new Node("a");
            Node b = new Node("b");
            Node c = new Node("c");
            Node d = new Node("d");
            Node e = new Node("e");

            a.adj = new HashSet<Node>() { b, c};
            b.adj.Add(e);
            c.adj.Add(e);
            d.adj = new HashSet<Node>() { b, e };

            ConnectionsFinder connectionsFinder = new ConnectionsFinder(a);
            bool f = connectionsFinder.isReachable(d);
            Console.WriteLine(f);

            Console.WriteLine("path to e:");
            foreach (var node in connectionsFinder.GetPathTo(e))
            {
                Console.WriteLine(node);
            }
            
        }
    }
}
