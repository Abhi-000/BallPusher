using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    int i = 0;
    bool onlyOnce = false;
    public static EnemyController instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!onlyOnce)
        {
            onlyOnce = true;
            StartCoroutine(StartShooting());
        }
        //ShootArrow.instance.GenerateArrow();
    }
    public void canonFracture()
    {
       for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).transform.tag == "canon")
            {
                Debug.Log("CANON FOUND!!");
                transform.GetChild(i).gameObject.SetActive(false);
                for(int j=0;j< transform.GetChild(i).transform.childCount;j++)
                {
                    if(transform.GetChild(i).transform.GetChild(j).transform.tag == "fracturedCanon")
                    {
                        transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    IEnumerator StartShooting()
    {
        while (i < transform.childCount)
        {
            yield return new WaitForSeconds(0.4f);
            if (transform.GetChild(i).transform != null)
            {
                if (transform.GetChild(i).tag != "canon")
                {
                    transform.GetChild(i).transform.GetComponent<Animator>().SetBool("shooting", true);
                }
                else
                {
                    transform.GetChild(i).transform.GetComponent<ShootArrow>().shooting = true;
                    StartCoroutine(transform.GetChild(i).transform.GetComponent<ShootArrow>().AfterDelayShoot());
                }
                i++;
            }
        }
    }
}
