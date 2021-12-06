using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnSide : MonoBehaviour
{

    // the target gameobject to follow (i.e. the player)
    public Transform target;

    // offset from the player, so it is not on top of the player
    public Vector3 offset;

    // We use FixedUpdate because it is called at fixed frame rate,
    // and therefore provide more consistent result when following the player.
    void FixedUpdate()
    {
        // move the game object to the target's position, with an offset.
        // This is done according to where the user is looking at.
        transform.position = target.position + Vector3.up * offset.y  // vertical offset is done by using the world's y offset

           // here we use ProjectOnPlane to project the target.right and target.forward vectors horizontally, 
           // and normalize to get a value between 0 and 1.
           + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x  // horizontal offset is done by using the target's own x axis. 
           + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z; // horizontal offset is done by using the target's own z axis.  
    }
}
