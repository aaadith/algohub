using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{

    
    class TopologicalSorter
    {
        private Graph graph;

        public TopologicalSorter(Graph graph)
        {
            this.graph = graph;
        }


        HashSet<Node> visited = new HashSet<Node>(); 
        Stack<Node> postOrder  = new Stack<Node>();
        public Stack<Node> Sort()
        {
            foreach (var node in graph.nodes)
            {
                if (!visited.Contains(node))
                {
                    dfs(node);
                }
            }
            return postOrder;
        }

        private void dfs(Node node)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                if (node.adj != null)
                {
                    foreach (var adjacentNode in node.adj)
                    {
                        dfs(adjacentNode);
                    }
                    postOrder.Push(node);
                }
                
            }
            
        }
    }

    class TopologicalSorterClient
    {
        static Graph getGraph()
        {
            Node a = new Node("a");
            Node b = new Node("b");
            Node c = new Node("c");
            Node d = new Node("d");
            Node e = new Node("e");

            a.adj = new HashSet<Node>() { b, c };
            b.adj.Add(e);
            c.adj.Add(e);
            d.adj = new HashSet<Node>() { b, e };

            HashSet<Node> nodes = new HashSet<Node>() { a, b, c, d, e };
            Graph graph = new Graph(nodes);
            return graph;
        }

        public static void Run()
        {
            Graph graph = getGraph();
            TopologicalSorter topologicalSorter = new TopologicalSorter(graph);
            Stack<Node> nodes = topologicalSorter.Sort();
            while (nodes.Count>0)
            {
                Console.WriteLine(nodes.Pop());
            }
            
        }

        
    }
}
