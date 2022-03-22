using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public GameObject player,sphere;
    float xPos;
    public Vector3 desiredPos,pos;
    public bool afterClicked = false,kicked = false;
    public static PlayerController instance;
    float Animation;
    Vector3 playerPos;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
    }
    public void PlayerRun()
    {
        if(sphere.gameObject != null && !BalController.instance.upPlatform)
                sphere.transform.position = transform.position + new Vector3(0, (sphere.transform.localScale.x*0.4f), (sphere.transform.localScale.x));
        /*else if(BalController.instance.upPlatform)
            sphere.transform.position = transform.position + new Vector3(0, (sphere.transform.localScale.x * 0.4f), (sphere.transform.localScale.x));*/
        if (Input.GetMouseButtonDown(0))
        {
            if (sphere.gameObject != null)
            {
                afterClicked = true;
                sphere.SetActive(true);
                player.GetComponent<Animator>().SetBool("run", true);
                xPos = Input.mousePosition.x;
                pos = transform.position;
            }
        }
        if (Input.GetMouseButton(0))
        {
            float xPosDiff = (xPos - Input.mousePosition.x) / Screen.width;
            xPosDiff *= -15;
            desiredPos = pos + new Vector3(xPosDiff, 0, 0);
            desiredPos.x = Mathf.Clamp(desiredPos.x, -7f, 0.5f);

        }
        if (afterClicked)
            cc.Move(new Vector3(desiredPos.x - transform.position.x, desiredPos.y - transform.position.y, 8 * Time.deltaTime));
    }
    public void KickBall()
    {
        if (!kicked)
        {
            //Time.timeScale = 0.5f;
            player.transform.GetComponent<Animator>().SetBool("kick", true);
            StartCoroutine(AfterDelayRoll(1.7f));
        }
    }
    IEnumerator AfterDelayRoll(float time)
    {
        sphere.transform.GetComponent<Rigidbody>().mass = 1;
        yield return new WaitForSeconds(time);
        kicked = true;
        //Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.died)
        {
            playerPos = transform.position;
        }
        if (GameManager.instance.died && Animation < 1.9f)
        {
            player.GetComponent<Animator>().SetBool("run",false);
            if (obstacle.instance != null)
            {
                if (obstacle.instance.obstacleCollided)
                {
                    player.GetComponent<Animator>().SetBool("die", true);
                    Animation += Time.deltaTime;
                    Animation = Animation % 2f;
                    transform.position = MathParabola.Parabola(playerPos, playerPos + Vector3.forward * 4, 1, Animation / 2f);
                }
            }
        }
        else if(Animation>1.9f)
        {
            GameManager.instance.LoseScreen();
            
        }
        if(kicked)
        {
            sphere.GetComponent<Rigidbody>().AddForce(transform.forward * 20f);
        }
        if(sphere.transform.localScale.x<= 0.3f)
        {
            sphere.SetActive(false);
            player.GetComponent<Animator>().SetBool("run", false);
            player.GetComponent<Animator>().SetBool("die", true);
            GameManager.instance.died = true;
            GameManager.instance.LoseScreen();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
