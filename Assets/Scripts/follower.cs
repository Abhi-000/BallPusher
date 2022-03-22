using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class follower : MonoBehaviour
{   
    public PathCreator pathCreator;
    public float speed = 5,xPosPath;
    public float distanceTraveled;
    public static follower instance;
    float xPosClamp;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.afterClicked)
        {
            PlayerController.instance.player.GetComponent<Animator>().SetBool("run", true);
            distanceTraveled += speed * Time.deltaTime;
            /*Debug.Log("RightX:"+Vector3.right.x);
            Debug.Log("Right:"+Vector3.right);*/
            
            Debug.Log(PlayerController.instance.desiredPos.x);
            //transform.position = Vector3.Lerp
            transform.Translate(pathCreator.path.GetPointAtDistance(distanceTraveled).x + PlayerController.instance.desiredPos.x, pathCreator.path.GetPointAtDistance(distanceTraveled).y, pathCreator.path.GetPointAtDistance(distanceTraveled).z);
            //transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self); //LEFT
            //transform.position = new Vector3(pathCreator.path.GetPointAtDistance(distanceTraveled).x+PlayerController.instance.desiredPos.x, pathCreator.path.GetPointAtDistance(distanceTraveled).y, pathCreator.path.GetPointAtDistance(distanceTraveled).z);
            //transform.GetChild(0).position =   new Vector3(pathCreator.path.GetPointAtDistance(distanceTraveled).x + PlayerController.instance.desiredPos.x, pathCreator.path.GetPointAtDistance(distanceTraveled).y, pathCreator.path.GetPointAtDistance(distanceTraveled).z);
            //transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
           /* if (PlayerController.instance.desiredPos.x <= 0)
            {
                transform.Translate(Vector3.left * Time.deltaTime * 10f, Space.Self);
            }
            else if (PlayerController.instance.desiredPos.x > 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 10f, Space.Self);
            } */           
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled);
        }
    }
}
