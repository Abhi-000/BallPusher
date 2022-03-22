using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public static bool died = false;
    bool onlyOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        if (tag != "breakable" && tag != "End") //for unbreakable wall
        {
            if (other.tag == "Player")
            {
                GameManager.instance.died = true;
                died = true;
                other.transform.GetChild(0).transform.GetComponent<Animator>().SetBool("crashAndFall", true);
                other.transform.GetChild(0).transform.GetComponent<Animator>().SetBool("run", false);
            }
        }
        else if (tag == "End") { 
            if (other.tag == "Player")
            {
                GameManager.instance.finalBall.transform.GetComponent<BalController>().enabled = true;
                other.transform.GetChild(0).transform.GetComponent<Animator>().SetBool("run", false);
                GameManager.instance.gameOver = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<BoxCollider>().isTrigger = true;
        /*if (tag == "Castle")
        {            
            transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else*/
        if (tag != "breakable") //for obstacle
        {
            if (collision.collider.tag == "mainStone")
            {
                if (PlayerController.instance.sphere.gameObject != null)
                {
                    PlayerController.instance.sphere.SetActive(false);
                    GameManager.instance.DestroyBall();
                    GameManager.instance.LoseScreen();
                }
            }
           
        }
        else
        {
            if (!onlyOnce)
            {
                onlyOnce = true;
                BalController.instance.DecreaseSphere(0.3f);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
