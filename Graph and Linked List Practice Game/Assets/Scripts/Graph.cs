using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.U2D.Common;

namespace GraphSearching
{
    //This is the graph class
    class Graph : MonoBehaviour
    {

        public List<FaceBookProfile> nodes = new List<FaceBookProfile>();

        public Graph()
        {

        }

        //Gets the number of nodes in the graph
        public int Count
        {
            get { return nodes.Count; }
        }

        //Gets a read-only list of the nodes in the graph
        public IList<FaceBookProfile> Nodes
        {
            get { return nodes.AsReadOnly(); }
        }

        public void Clear()
        {
            //remove all the neighbors from each node
            //so nodes can be garbage collected
            foreach(FaceBookProfile node in nodes)
            {
                //node.RemoveAllNeighbors();
            }

            // now remove all the nodes from the graph
            for(int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes.RemoveAt(i);
            }
        }

        // Adds a node with the given value to the graph. If a node
        // with the same value is in the graph, the value
        // isn't added and the method returns false
        public bool AddNode(FaceBookProfile value)
        {
            if(Find(value) != null)
            {
                // duplicate value
                return false;
            }
            else
            {
                nodes.Add(value);
                return true;
            }
        }

        public bool AddEdge(FaceBookProfile value1, FaceBookProfile value2)
        {

            FaceBookProfile node1 = Find(value1);
            FaceBookProfile node2 = Find(value2);
            if (node1 == null ||
                node2 == null)
            {
                return false;
            }
            else if (node1.Neighbors.Contains(node2))
            {
                // edge already exists
                return false;
            }
            else
            {
                // undirected graph, so add as neighbors to each other
                node1.AddNeighbor(node2);
                node2.AddNeighbor(node1);
                return true;
            }
        }

        public bool RemoveNode(FaceBookProfile value)
        {
            FaceBookProfile removeNode = Find(value);
            if (removeNode == null)
            {
                return false;
            }
            else
            {
                // need to remove as neighor for all nodes
                // in graph
                nodes.Remove(removeNode);
                foreach (FaceBookProfile node in nodes)
                {
                    node.RemoveNeighbor(removeNode);
                }
                return true;
            }
        }

        public bool RemoveEdge(FaceBookProfile value1, FaceBookProfile value2)
        {
            FaceBookProfile node1 = Find(value1);
            FaceBookProfile node2 = Find(value2);
            if (node1 == null ||
                node2 == null)
            {
                return false;
            }
            else if (!node1.Neighbors.Contains(node2))
            {
                // edge doesn't exist
                return false;
            }
            else
            {
                // undirected graph, so remove as neighbors to each other
                node1.RemoveNeighbor(node2);
                node2.RemoveNeighbor(node1);
                return true;
            }
        }


        public FaceBookProfile Find(FaceBookProfile value)
        {
            foreach (FaceBookProfile node in nodes)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }
            return null;
        }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                builder.Append(nodes[i].ToString());
                if (i < Count - 1)
                {
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }


    }


}
