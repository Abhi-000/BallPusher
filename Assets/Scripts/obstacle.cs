using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public bool obstacleCollided = false;
    public static obstacle instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "mainStone")
        {
            StartCoroutine(GameManager.instance.AfterDelayDead());
            obstacleCollided = true;
            other.gameObject.SetActive(false);
            GameManager.instance.DestroyBall();
            //GameManager.instance.fracturedSphere.SetActive(true);
            //GameManager.instance.fracturedSphere.transform.parent = null;


        }
    }
}
