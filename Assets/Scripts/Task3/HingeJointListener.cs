using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HingeJointListener : MonoBehaviour
{
    // how many degrees from the min/max the hinge has to be 
    // before it is detected as min or max. Increase this to 
    // make the min/max trigger easier.
    public float angleThreshold = 1f;

    // State of the hinge joint : either reached min or max or none if in between
    public HingeJointState hingeJointState = HingeJointState.None;

    //Event called on min reached
    public UnityEvent OnMinLimitReached;

    //Event called on max reached
    public UnityEvent OnMaxLimitReached;

    public UnityEvent NoLimitReached;

    public enum HingeJointState { Min, Max, None }

    // hinge joint attached to the game object
    private HingeJoint hinge;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()

    {
        // get the differences between the current angle and min / max angles of the hinge.
        float distanceToMin = Mathf.Abs(hinge.angle - hinge.limits.min);
        float distanceToMax = Mathf.Abs(hinge.angle - hinge.limits.max);

        //Reached Min
        if (distanceToMin < angleThreshold)
        {
            // invoke the UnityEvent and change the hinge joint state. 
            // All methods that have been registered will be called by Unity.
            if (hingeJointState != HingeJointState.Min)
                OnMinLimitReached.Invoke();

            hingeJointState = HingeJointState.Min;
        }

        //Reached Max
        else if (distanceToMax < angleThreshold)
        {
            if (hingeJointState != HingeJointState.Max)
                OnMaxLimitReached.Invoke();

            hingeJointState = HingeJointState.Max;
        }

        //No Limit reached
        else
        {
            if (hingeJointState != HingeJointState.None)
                NoLimitReached.Invoke();
            hingeJointState = HingeJointState.None;

        }
    }
}