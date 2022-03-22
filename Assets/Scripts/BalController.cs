using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalController : MonoBehaviour
{
    public static BalController instance;
    public bool upPlatform = false;
    public GameObject plusOneTxt;
    private void Awake()
    {
        instance = this;
    }
    //private float scaleFactor = 0.3f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "goodSphere")
        {
            Destroy(other.gameObject);
            IncreaseSphere(0.3f);
        }
        else if(other.tag == "badSphere")
        {          
            Destroy(other.gameObject);
            DecreaseSphere(0.3f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag== "Castle")
        {
            //gameObject.SetActive(false);
            //EnemyController.instance.canonFracture();
            collision.gameObject.SetActive(false);
            GameManager.instance.fracturedCastle.SetActive(true);
            GameManager.instance.WinScreen();
            
        }
        else if(collision.collider.tag == "up")
        {
            upPlatform = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "up")
        {
            upPlatform = false;
        }
    }
    private void Update()
    {
    }
    public void IncreaseSphere(float scaleFactor)
    {
        GameObject go = Instantiate(plusOneTxt, new Vector3(PlayerController.instance.sphere.transform.position.x-1, PlayerController.instance.sphere.transform.position.y+1, PlayerController.instance.sphere.transform.position.z+1), Quaternion.identity);
        //go.transform.position = new Vector3(go.transform.position.x - 2, go.transform.position.y, go.transform.position.z);
        PlayerController.instance.sphere.GetComponent<Animator>().Play("blinking");
        PlayerController.instance.sphere.transform.localScale = new Vector3(PlayerController.instance.sphere.transform.localScale.x + scaleFactor, PlayerController.instance.sphere.transform.localScale.y + scaleFactor, PlayerController.instance.sphere.transform.localScale.z + scaleFactor);
    }
    public void DecreaseSphere(float scaleFactor)
    {
        GameObject go = Instantiate(plusOneTxt, PlayerController.instance.sphere.transform.position, Quaternion.identity);
        go.transform.GetComponent<TextMeshPro>().text = "-1";
        go.transform.GetComponent<TextMeshPro>().color = Color.red;
        PlayerController.instance.sphere.GetComponent<Animator>().Play("blinking");
        PlayerController.instance.sphere.transform.localScale = new Vector3(PlayerController.instance.sphere.transform.localScale.x - scaleFactor, PlayerController.instance.sphere.transform.localScale.y - scaleFactor, PlayerController.instance.sphere.transform.localScale.z - scaleFactor);
    }
}
