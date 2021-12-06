using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit; 

public class LocomotionController : MonoBehaviour
{

    public ActionBasedController leftRay;
    public ActionBasedController rightRay;

    public float activationThreshold = 0.1f;
    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;
  
    void Update()
    {
        if(leftRay)
        {
            leftRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftRay));
        }

        if (rightRay)
        {
            rightRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightRay));
        }
    }
    private bool CheckIfActivated(ActionBasedController controller)
    {
        // Check whether the controller’s teleport select (trigger) button has been pressed enough.
        // We could use InputDevice's TryGetFeatureValue() here,
        // but instead we use another way to do it through the controller object
        float result = controller.selectAction.action.ReadValue<float>();
        return (result > activationThreshold);
    }
}
