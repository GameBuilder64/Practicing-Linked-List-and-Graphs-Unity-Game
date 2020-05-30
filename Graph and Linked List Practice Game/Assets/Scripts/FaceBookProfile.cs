using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using GraphSearching;

// The FacebookBook Profile Class is the the Node in the graph
// It contains the connections and all profile information of the person
public class FaceBookProfile : MonoBehaviour
{

    public Graph FaceBookGraph;

    public string theName;
    public GameObject inputField;
    public GameObject textDisplay;


    public string Facebookname;

    public Text ListOfFriends;





    // Start is called before the first frame update
    void Start()
    {
        FriendsList = new List<FaceBookProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Fields

        FaceBookProfile value;
        List<FaceBookProfile> FriendsList;
        //List<int> weights;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value stored in the node
        /// </summary>
        public FaceBookProfile Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets a read-only list of the neighbors of the node
        /// </summary>
        public IList<FaceBookProfile> AllMyCurrentFriends
        {
            get { return FriendsList.AsReadOnly(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the given node as a neighbor for this node
        /// </summary>
        /// <param name="neighbor">neighbor to add</param>
        /// <param name="weight">weight of edge being added</param>
        /// <returns>true if the neighbor was added, false otherwise</returns>
        public bool AddNeighbor(FaceBookProfile neighbor)
        {
            // don't add duplicate nodes
            if (FriendsList.Contains(neighbor))
            {
                return false;
            }
            else
            {
                FriendsList.Add(neighbor);
                //weights.Add(weight);
                return true;
            }
        }



        /// <summary>
        /// Removes the given node as a neighbor for this node
        /// </summary>
        /// <param name="neighbor">neighbor to remove</param>
        /// <returns>true if the neighbor was removed, false otherwise</returns>
        public bool RemoveNeighbor(FaceBookProfile neighbor)
        {
            // remove weight for neighbor
            int index = FriendsList.IndexOf(neighbor);
            if (index == -1)
            {
                // neighbor not in list
                return false;
            }
            else
            {
                // remove neighbor and edge weight
                FriendsList.RemoveAt(index);
                return true;
            }
        }

        /// <summary>
        /// Removes all the neighbors for the node
        /// </summary>
        /// <returns>true if the neighbors were removed, false otherwise</returns>
        public bool RemoveAllNeighbors()
        {
            for (int i = FriendsList.Count - 1; i >= 0; i--)
            {
                FriendsList.RemoveAt(i);
            }
            
            return true;
        }

        /// <summary>
        /// Converts the node to a string
        /// </summary>
        /// <returns>the string</returns>
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();
            nodeString.Append("[Node Value: " + value +
                " Neighbors: ");
            for (int i = 0; i < FriendsList.Count; i++)
            {
                nodeString.Append(FriendsList[i].Value + " " +
                    "(" + FriendsList[i] + ") ");
            }
            nodeString.Append("]");
            return nodeString.ToString();
        }


    // Calls this function when pressing on the add button
    // First it searchs the entire graph to see if the facebook profile exist
    // Then it adds it and returns true and updates the text
    public void AddFriend()
    {
        FaceBookProfile newFriend;
        bool Results;

        theName = inputField.GetComponent<Text>().text;
        newFriend = FaceBookGraph.FindProfile(theName);
        
        if(newFriend != null)
        {
            Results = FaceBookGraph.AddEdge(this, newFriend);
            if (Results == true)
            {
                textDisplay.GetComponent<Text>().text = theName + ": Was added as your friend";
                ListOfFriends.GetComponent<Text>().text = theName + " Is your friend";
            }
            else if (Results == false)
            {
                textDisplay.GetComponent<Text>().text = theName + ": Is your friend already or does not exist";
            }
        }
        else
        {
            textDisplay.GetComponent<Text>().text = theName + ": Does not exist";
        }

    }

    public void RemoveFriend()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Welcome " + theName + " to the game";
    }

    #endregion

}
