using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject resetBtn;
    PlayerMove player;

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Reposition();
    }

    public void Reposition()
    {
        player.transform.position = new Vector2(0, 0);
        player.VelocityZero();
    }
}
