using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceWall : MonoBehaviour
{
    GameObject player;
    public float iceWallStatus; //default = 0 (wall is broken)
    public float usingMicrophone; //default = 0
    public float triggerRange; //good value around 5.5

    
    

    // Start is called before the first frame update
    void Start()
    {
        iceWallStatus = 0;
        player = GameObject.FindWithTag("Player"); //Unity player name must be Player
    }

    // Update is called once per frame
    void Update()
    {
        //if player is detected within range and is using a microphone, wall status will change
        //idea ==> show possibility to use microphone when player is within range

        if (usingMicrophone == 1){

            if (Vector2.Distance(transform.position, player.transform.position) < triggerRange){
                //destroy wall here and enable climbing from playerMovement class
                //Debug.Log("The player is within range of icewall and is using microphone");
                iceWallStatus = 1; //add time limit!
            }
            else
            {
                //wall is not destroyable
            }
        }



        
    }
}
