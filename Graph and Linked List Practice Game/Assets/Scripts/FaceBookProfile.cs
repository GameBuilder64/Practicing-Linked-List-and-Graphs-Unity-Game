using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using GraphSearching;
using UnityEngine.Analytics;

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

    public string tempstringholder = null;
    public string tempstringholder2 = null;



    public string FindThisPerson;

    //This will be the linkedList
    public List<FaceBookProfile> searchListpath;

    public List<FaceBookProfile> AlreadySearch;

    public List<List<FaceBookProfile>> PathSaving;

    // Start is called before the first frame update
    void Start()
    {
        FriendsList = new List<FaceBookProfile>();
        searchListpath = new List<FaceBookProfile>();
        AlreadySearch = new List<FaceBookProfile>();
        PathSaving = new List<List<FaceBookProfile>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Fields

        FaceBookProfile value;
        public List<FaceBookProfile> FriendsList;
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

                //foreach(FaceBookProfile Friend in FriendsList)
                //{ }
                if (tempstringholder == "")
                {
                    tempstringholder = theName;
                    ListOfFriends.GetComponent<Text>().text = theName + " Is your friend";
                }
                else
                {
                    tempstringholder = tempstringholder + ", " + theName ;
                    ListOfFriends.GetComponent<Text>().text = tempstringholder + " Are your friend";
                        
                }

                
                
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
        FaceBookProfile removefriend;
        bool Results;

        theName = inputField.GetComponent<Text>().text;

        AreYouFriends(theName);



    }

    // Finds the friend and loops around your entire friend lists to remove that friend then loops around to update your
    // Display friends text list
    // Also loops that friends and remove your as their friend and loops teir friend list text to correctly display
    public bool AreYouFriends(string IsThisYourFriend)
    {
        foreach (FaceBookProfile Friend in FriendsList)
        {
            if (Friend.Facebookname.Equals(IsThisYourFriend))
            {
                textDisplay.GetComponent<Text>().text = IsThisYourFriend + ": Was removed as your friend";

                // Removes you from that person list and then loops to update their friends list text and friends list
                Friend.FriendsList.Remove(this);
                Friend.tempstringholder = "";

                if (Friend.FriendsList.Count > 0)
                {
                    // Loops through the other persons friends list 
                    foreach (FaceBookProfile OtherPersonsRemainingFriend in Friend.FriendsList)
                    {
                        if (Friend.tempstringholder == "")
                        {
                            Friend.tempstringholder = OtherPersonsRemainingFriend.Facebookname;
                            Friend.ListOfFriends.GetComponent<Text>().text = Friend.tempstringholder + " Is your friend";
                        }
                        else if (OtherPersonsRemainingFriend.Facebookname != "" || OtherPersonsRemainingFriend.theName != null)
                        {
                            Friend.tempstringholder = Friend.tempstringholder + ", " + OtherPersonsRemainingFriend.Facebookname;
                            Friend.ListOfFriends.GetComponent<Text>().text = Friend.tempstringholder + " Are your friend";
                        }
                    }
                }
                if (Friend.FriendsList.Count == 0)
                {
                    Friend.ListOfFriends.GetComponent<Text>().text = "You Current Have No Friends";
                }

                // Remove this friend your edges and neightbors 
                FriendsList.Remove(Friend);
                tempstringholder = "";

                if(FriendsList.Count > 0)
                {
                    foreach (FaceBookProfile RemainingFriend in FriendsList)
                    {
                        if (tempstringholder == "")
                        {
                            tempstringholder = RemainingFriend.Facebookname;
                            ListOfFriends.GetComponent<Text>().text = tempstringholder + " Is your friend";

                        }
                        else if (RemainingFriend.Facebookname != "" || RemainingFriend.theName != null)
                        {
                            tempstringholder = tempstringholder + ", " + RemainingFriend.Facebookname;
                            ListOfFriends.GetComponent<Text>().text = tempstringholder + " Are your friend";

                        }
                    }
                }
                if(FriendsList.Count == 0)
                {
                    ListOfFriends.GetComponent<Text>().text = "You Current Have No Friends";
                }
                return true;
            }
        }
        textDisplay.GetComponent<Text>().text = theName + ": Does not exist";
        return false;
    }


    public void MutualFriendFind()
    {
        FaceBookProfile removefriend;
        bool Results;

        theName = inputField.GetComponent<Text>().text;

        if (this.Facebookname == theName)
        {
            textDisplay.GetComponent<Text>().text = "You Talking About Yourself";
        }
        else
        {
            SearchForMutualFriend(theName);
            foreach (List<FaceBookProfile> PathFound in PathSaving)
            {



                foreach (FaceBookProfile ElementInPathFound in PathFound)
                {
                    tempstringholder2 = tempstringholder2 + "--->" + ElementInPathFound.Facebookname;
                    textDisplay.GetComponent<Text>().text = tempstringholder2;
                }
            }
        }

        AlreadySearch.Clear();
    }
    
    public void SearchForMutualFriend(string FindthisFriend)
    {

        FindThisPerson = FindthisFriend;
        searchListpath.Add(this);
        if (AlreadySearch.Contains(this) == false)
        {
            AlreadySearch.Add(this);
        }

        foreach (FaceBookProfile Friend in this.FriendsList)
        {
            if (AlreadySearch.Contains(Friend))
            {
                continue;
            }
            if (Friend.Facebookname.Equals(FindThisPerson))
            {
                searchListpath.Add(Friend);
                PathSaving.Add(searchListpath);
                searchListpath.Clear();
            }
            else
            {
                searchListpath.Add(Friend);
                LoopThroughFriendsList(Friend);
            }
        }
    }


    public void LoopThroughFriendsList(FaceBookProfile CheckThisFriend)
    {
        if (AlreadySearch.Contains(CheckThisFriend) == false)
        {
            AlreadySearch.Add(CheckThisFriend);
        }

        foreach (FaceBookProfile ThisFriend in CheckThisFriend.FriendsList)
        {
            if (AlreadySearch.Contains(ThisFriend) == true)
            {
                continue;
            }
            if (ThisFriend.Facebookname.Equals(FindThisPerson))
            {
                searchListpath.Add(ThisFriend);
                PathSaving.Add(searchListpath);
                searchListpath.Clear();
                searchListpath.Add(this);
            }
            else
            {
                searchListpath.Add(ThisFriend);
                LoopThroughFriendsList(ThisFriend);
            }
        }
    }

    #endregion
}
