using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public int multiplier;
    public bool onlyOnce = false;
    private void OnTriggerEnter(Collider other)
    {
       if (other.tag == "mainStone")
       {
            if (!onlyOnce)
            {
                onlyOnce = true;
                if (tag == "powerUp")
                {
                   BalController.instance.IncreaseSphere(multiplier * 0.3f);
                }
                else
                {
                    BalController.instance.DecreaseSphere(multiplier * 0.3f);
                    //PlayerController.instance.sphere.transform.localScale = new Vector3(PlayerController.instance.sphere.transform.localScale.x - (multiplier * 0.3f), PlayerController.instance.sphere.transform.localScale.y - (multiplier * 0.3f), PlayerController.instance.sphere.transform.localScale.z - (multiplier * 0.3f));
                }
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        //onlyOnce = false;
    }
}
