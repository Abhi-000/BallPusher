using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool died = false;
    public bool gameOver = false;
    public static GameManager instance;
    public GameObject fracturedSphere,levelCompleteBar,loseScreen, fracturedCastle,winScreen,finalBall;
    int i = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (!died && !gameOver)
        {
            fracturedSphere.transform.position = PlayerController.instance.sphere.transform.position;
            fracturedSphere.transform.localScale = (PlayerController.instance.sphere.transform.localScale / 2f);
            PlayerController.instance.PlayerRun();
        }
        else if (gameOver)
            PlayerController.instance.KickBall();
        levelCompleteBar.transform.GetComponent<Slider>().value = (PlayerController.instance.player.transform.position.z * 0.005f);
    }
    public void DestroyBall()
    {
        fracturedSphere.SetActive(true);
        fracturedSphere.transform.parent = null;
    }
    public void LoseScreen()
    {
        loseScreen.SetActive(true);
    }
    public void WinScreen()
    {
        Time.timeScale = 1;
        winScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator AfterDelayDead()
    {
        yield return new WaitForSeconds(0.2f);
        died = true;
    }
}
