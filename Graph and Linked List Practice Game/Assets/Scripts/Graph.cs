using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.U2D.Common;
using UnityEngine.UI;

namespace GraphSearching
{
    //This is the graph class
    public class Graph : MonoBehaviour
    {

        public List<FaceBookProfile> nodes = new List<FaceBookProfile>();
        public string BillySteven = "Billy Steven";


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

            FaceBookProfile node1 = FindProfile(value1.Facebookname);
            FaceBookProfile node2 = FindProfile(value2.Facebookname);
            if (node1 == null ||
                node2 == null)
            {
                return false;
            }
            else if (node1.AllMyCurrentFriends.Contains(node2))
            {
                // edge already exists
                return false;
            }
            else
            {
                // undirected graph, so add as neighbors to each other
                node1.AddNeighbor(node2);
                node2.AddNeighbor(node1);

               
                //Loop through the friend that is added and update their friend list text to display
                //The new friend as well
                foreach (FaceBookProfile Friend in node2.FriendsList)
                {
                    if (node2.tempstringholder == null)
                    {
                        node2.tempstringholder = Friend.Facebookname;
                        node2.ListOfFriends.GetComponent<Text>().text = Friend.Facebookname + " Is your friend";
                    }
                    else
                    {
                        node2.tempstringholder = node2.tempstringholder + ", " + Friend.Facebookname;
                        node2.ListOfFriends.GetComponent<Text>().text = node2.tempstringholder + " Are your friend";
                    }
                }



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
            else if (!node1.AllMyCurrentFriends.Contains(node2))
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

        
        //Find if the Facebook profile in the graph if it exist using the actually class value
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
        

        //Find if the Facebook profile in the graph if it exist using string value of the name
        public FaceBookProfile FindProfile(string value)
        {
            foreach (FaceBookProfile node in nodes)
            {
                if (node.Facebookname.Equals(value))
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
