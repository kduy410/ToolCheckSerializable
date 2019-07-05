using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppKTTXD
{
    public class Graph
    {
        private int v;
        public LinkedList<int>[] adj;
        public Graph(int v)
        {
            this.v = v;
            adj = new LinkedList<int>[v]; //max = v; số đỉnh tối đa = v
            for (int i = 0; i < v; ++i)
            {
                adj[i] = new LinkedList<int>();
            }
        }
        internal void addEdge(int v, int w) //thêm cạnh
        {
            adj[v].AddFirst(w);
            //g.addEdge(0, 1);
            //g.addEdge(0, 2);
            //g.addEdge(1, 2);
            //g.addEdge(2, 0);
            //g.addEdge(2, 3);
            //g.addEdge(3, 3);
        }
        internal bool isCyclic() //kt có chu trình hay k
        {
            bool[] visited = new bool[v];
            bool[] recStack = new bool[v];
            for (int i = 0; i < v; i++)
            {
                if (isCyclicUtil(i, visited, recStack))
                    return true;
            }
            return false;
        }

        private bool isCyclicUtil(int ver, bool[] visited, bool[] recStack)
        {
            visited[ver] = true;
            recStack[ver] = true;
            LinkedList<int> lnk = adj[ver];

            foreach (var item in lnk)
            {
                if (visited[item] == false && isCyclicUtil(item, visited, recStack))
                    return true;
                else if (recStack[item])
                    return true;
            }
            recStack[ver] = false;
            return false;
        }
    }
}

