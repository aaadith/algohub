using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    class ConnectedComponentsFinder
    {
        private Graph graph;
        private HashSet<Node> marked = new HashSet<Node>();
        private Dictionary<Node, int> clusterInfo = new Dictionary<Node, int>();

        public ConnectedComponentsFinder(Graph graph)
        {
            this.graph = graph;
            FindConnectedComponents();
        }

        void FindConnectedComponents()
        {
            int clusterId = 0;
            foreach (var node in graph.nodes)
            {
                if (!clusterInfo.ContainsKey(node))
                {
                    dfs(node, clusterId);
                    clusterId++;
                }
            }
        }

        private void dfs(Node x, int clusterId)
        {
            if (!clusterInfo.ContainsKey(x))
            {
                clusterInfo[x] = clusterId;

                if (x.adj != null)
                {
                    foreach (var node in x.adj)
                    {
                        dfs(node, clusterId);
                    }
                }
            }
        }

        public bool AreConnected(Node x, Node y)
        {
            return clusterInfo[x] == clusterInfo[y];
        }
    }

    class ConnectedComponentsClient
    {

        
        public static void Run()
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

            HashSet<Node> nodes = new HashSet<Node>(){a,b,c,d,e};
            Graph graph = new Graph(nodes);

            ConnectedComponentsFinder connectedComponentsFinder = new ConnectedComponentsFinder(graph);
            Console.WriteLine(connectedComponentsFinder.AreConnected(a,d));

        }
    }
}
