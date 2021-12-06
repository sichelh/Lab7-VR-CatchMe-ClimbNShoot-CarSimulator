using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void CatchMe()
    {
        SceneManager.LoadScene("CatchMe");
    }
    public void ClimbnShot()
    {
        SceneManager.LoadScene("Climb'n'Shot");
    }
    public void RCCar()
    {
        SceneManager.LoadScene("RCCar");
    }
}
