using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTrash : MonoBehaviour
{
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject.Find("XR Rig").GetComponent<Climber>().KillScore++;
            Destroy(gameObject);
        }
    }
}
