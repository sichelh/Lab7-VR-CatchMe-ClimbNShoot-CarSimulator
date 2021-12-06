using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Climber : MonoBehaviour
{

    private CharacterController characterController;

    private ActionBasedContinuousMoveProvider continuousMovement;

    private List<ActionBasedController> climbingHands = new List<ActionBasedController>();

    private Dictionary<ActionBasedController, Vector3> previousPositions = new Dictionary<ActionBasedController, Vector3>();

    private Vector3 currentVelocity;

    private float timer = 0;
    public bool timerIsRunning = false;
    public Text timeText;

    private bool isCliming = true;

    private int FinalScore = 0;
    public int KillScore = 0;
    public float ClimbScore = 100;
    public Text killscoreText;

    public GameObject GoalUI;
    public Text scoreText;
    public Text climbscoreText;

    public GameObject DieUI;
    private bool isTooHigh = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
        GoalUI.SetActive(false);
        DieUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        foreach(ActionBasedController hand in climbingHands)
        {
            if(hand)
            {
                continuousMovement.enabled = false;

                Climb(hand);
            }
        }
        if (isCliming)
        {
            if (climbingHands.Count == 0)
            {
                continuousMovement.enabled = true;
                
                if (this.gameObject.transform.position.y > 5)
                {
                    isTooHigh = true;
                }
                else if (this.gameObject.transform.position.y <= 1)
                {
                    timer = 0;
                }
            }
            else if (climbingHands.Count > 0) 
            {
                timerIsRunning = true;
            }
        }
        if (isTooHigh) //Kill the player if player falls from a too high place
        {
            if (this.gameObject.transform.position.y <= 1)
            {
                DieUI.SetActive(true);
            }
        }

    }

    private void Update()
    {
        killscoreText.text = "killscore: " + KillScore;
        climbscoreText.text = "climbscore: " + Mathf.Round(ClimbScore);

        //timer
        if (timerIsRunning)
        {
            ClimbScore -= Time.deltaTime;
            DisplayTime(timer);
            timer += Time.deltaTime;
        }
        
    }

    void DisplayTime(float timeToDisplay) //display timer
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeToDisplay += 1;
    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactor is XRDirectInteractor)
        {
            ActionBasedController hand = args.interactor.gameObject.GetComponent<ActionBasedController>();

            
            climbingHands.Add(hand);

            previousPositions.Add(hand, hand.positionAction.action.ReadValue<Vector3>());

            if (args.interactable.CompareTag("Goal")) //when player goal, show score
            {
                isCliming = false;
                timerIsRunning = false;
                FinalScore = (int)(ClimbScore) + KillScore;
                GoalUI.SetActive(true);
                scoreText.text = "Score: " + FinalScore;
            }
        }
    }
    public void SelectExited(SelectExitEventArgs args)
    {
        if (args.interactor is XRDirectInteractor)
        {
            var hand = climbingHands.Find(x => x.name == args.interactor.name);

            if (hand)
            {
                climbingHands.Remove(hand);
                previousPositions.Remove(hand);
            }

        }
    }

    private void Climb(ActionBasedController hand)
    {
        if(previousPositions.TryGetValue(hand, out Vector3 previousPos))
        {
            currentVelocity = (hand.positionAction.action.ReadValue<Vector3>() - previousPos) / Time.fixedDeltaTime;

            characterController.Move(transform.rotation * -currentVelocity * Time.fixedDeltaTime);

            previousPositions[hand] = hand.positionAction.action.ReadValue<Vector3>();
        }
      

    }

}
