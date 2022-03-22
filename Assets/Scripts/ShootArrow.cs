using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject arrowPrefab,deathEffect;
    public Transform arrowOrigin,arrowParent;
    public float shootForce = 40f;
    public static ShootArrow instance;
    bool onlyOnce = false;
    public bool shooting = false;
    private void Update()
    {
        if (tag != "canon")
        {
            Vector3 dir = PlayerController.instance.sphere.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 20).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        /*if (tag == "canon")
        {
            StartCoroutine(AfterDelayShoot());
        }*/
    }
    public IEnumerator AfterDelayShoot()
    {
        while (shooting)
        {
            yield return new WaitForSeconds(2f);
            GenerateArrow();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!onlyOnce)
        {
            if (other.tag == "mainStone")
            {
                BalController.instance.DecreaseSphere(0.3f);
                onlyOnce = true;
                GameObject go = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.transform.gameObject);
                go.transform.position = transform.position;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "castlePiece")
        {
            if (tag != "canon")
            {
                GameObject go = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.transform.gameObject);
                go.transform.position = transform.position;
                go.transform.localScale = new Vector3(4, 4, 4);
            }
            else
            {
                //transform.gameObject.SetActive(false);
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).tag == "fracturedCanon")
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            shooting = false;

        }
    }
    public void GenerateArrow()
    {
        //GameObject go = Instantiate(arrowPrefab, arrowOrigin.transform.position, arrowParent.transform.rotation, arrowParent.transform);
        GameObject go = Instantiate(arrowPrefab,Vector3.zero, Quaternion.identity,arrowParent.transform);
        go.transform.localScale = arrowOrigin.transform.localScale;
        go.transform.localPosition = arrowOrigin.transform.localPosition;
        go.transform.localRotation = arrowOrigin.transform.localRotation;
        // go.transform.localPosition = Vector3.zero;
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if(tag!= "canon")
        rb.velocity = arrowOrigin.transform.forward * shootForce;
        else
            rb.velocity = arrowOrigin.transform.right * shootForce;
        go.transform.parent = null;
    }
}
