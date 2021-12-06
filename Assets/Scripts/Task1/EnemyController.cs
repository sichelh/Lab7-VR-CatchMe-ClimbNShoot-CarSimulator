using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.3f;
    public GameObject GameOverUI;
    public Text HP;
    public int hitPoint = 100;
    public int hitDamage = 50;

    public AudioSource bomb;
    public AudioSource enemykill;

    void Start()
    {
        GameOverUI.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(target);

        HP.text = "Enemy HP: " + hitPoint;

        Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(movement);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if(hitPoint == 0)
        {
            Destroy(gameObject);
            enemykill.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            GameOverUI.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            hitPoint -= hitDamage;
            bomb.Play();
        }
    }

}
