using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMe : MonoBehaviour
{
    // How to play the game

    // When you look at the tabs, each tab has the name of the profile of the person

    // By clicking on the tabs you switch around to that persons profile

    // The profiles are the nodes within the graph

    // The Graph has already added all the nodes in the graph when profiles of the person were created making it easier

    /// How to add a friend
    // Press on any of the tabs
    // Type out the exact friend you want to add into the input text box, The text is case sensetive, so make sure to type exactly
    // Click on the add button

    // The text you input is saved and then loop through graph to see if this profile exist
    // If the profile is found both the profile you are on and that persons profile is added as an edge to connect the profiles

    // Your friends list should update and that other profiles friend list is updated to display that you guys are friends

    ///How to remove a friend
    // Type the exact friend you want to remove, Text is case sensetive, so make sure to type the exact name
    // Press remove
    // It will loop through that profiles friend list to find if you have that friend then disconnect the nodes from each other
    // It will go to that persons nodes list and also diconnect the node and the edge is removed or vertices 
    // It will update the friends list text on both the profile you on and the profile that the person is on too

    // Search
    // Type the friend you want to find and it will generate a path if there is one to that person

}

