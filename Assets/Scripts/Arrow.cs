using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "mainStone")
        {             
            BalController.instance.DecreaseSphere(0.25f);
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
   /* private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "mainStone")
        {
            BalController.instance.DecreaseSphere(0.25f);
           
        }
    }*/
}
