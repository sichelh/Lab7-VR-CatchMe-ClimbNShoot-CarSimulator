using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotspeed = 1.0f;
    private Rigidbody rb;

    private bool forward = false;
    private bool backward = false;
    private bool left = false;
    private bool right = false;

    public int MyCarHP = 100;
    public int HPDamage = 25;

    public GameObject GameOverUI;
    public Text HPtext;

    private int score = 0;
    public Text scoretext;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (forward)
            {
                Vector3 movement = transform.localRotation * Vector3.forward;
                gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
            }
            if (backward)
            {
                Vector3 movement = transform.localRotation * Vector3.back;
                gameObject.transform.Translate(0, 0, -speed * Time.deltaTime);
            }
            if (left)
            {
                gameObject.transform.Rotate(new Vector3(0, rotspeed * Time.deltaTime, 0));
            }
            if (right)
            {
                gameObject.transform.Rotate(new Vector3(0, -rotspeed * Time.deltaTime, 0));
            }
        }

        if(MyCarHP <= 0)
        {
            GameOverUI.SetActive(true);
        }

        scoretext.text = "Score: " + score;
        HPtext.text = "MyCarHP: " + MyCarHP;
    }

    public void MaxforwardLever()
    {
        forward = true;
    }
    public void MinbackwardLever()
    {
        backward = true;
    }
    public void MaxleftLever()
    {
        left = true;
    }
    public void MinrightLever()
    {
        right = true;
    }
    public void NofbLever()
    {
        forward = false;
        backward = false;
    }
    public void NolrLever()
    {
        left = false;
        right = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            MyCarHP -= HPDamage;
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            score++;
        }
    }
}
