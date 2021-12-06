using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodController : MonoBehaviour
{
    public AudioSource eat;
    private int score;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void SelectExited(SelectExitEventArgs args)
    {
        args.interactable.GetComponent<Rigidbody>().useGravity = true;
    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.CompareTag("Food"))
        {
            Destroy(args.interactable.gameObject);
            eat.Play();
            score++;
        }
    }
}
