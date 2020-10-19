using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGridBehavior : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float heightFromGround;
    [SerializeField] private float tolerance;
    private Vector3 directionToTarget;
    private GameObject[] listOfSpawners;
    private GameObject target;
    [SerializeField] private GameObject closestSpawner;
    private GameObject currentSpawner;
    private float xDirectionSign;
    private float zDirectionSign;
    private int speed = 1;



    private void Start()
    {
        player = Camera.main.gameObject;
        listOfSpawners = GameObject.FindGameObjectsWithTag("Spawner");
        closestSpawner = listOfSpawners[0];
        currentSpawner = listOfSpawners[0];
        target = player;
    }

    public virtual void MonsterMovement()
    {

        GetClosestSpawner("Spawner");

        Debug.Log("Current position: " + transform.position);
        Debug.Log("Current target position: " + target.transform.position);

        //Find vector between player and monster, ask whether x and z directions are each positive or negative.
        //Verticality does not matter, so ignore y component.
        directionToTarget = (transform.position - target.transform.position).normalized;
        Debug.Log("Direction to target: " + directionToTarget);
        xDirectionSign = directionToTarget.x / Mathf.Abs(directionToTarget.x);
        zDirectionSign = directionToTarget.z / Mathf.Abs(directionToTarget.z);

        if(directionToTarget.x != 0 || directionToTarget.z != 0)
        {
            if (Mathf.Abs(directionToTarget.x) >= Mathf.Abs(directionToTarget.z))
            {
                //Move monster one space along grid in the x direction toward player
                transform.position += new Vector3(speed, 0, 0) * -xDirectionSign;
            }
            else
            {
                //Move monster one space along grid in the z direction toward player
                transform.position += new Vector3(0, 0, speed) * -zDirectionSign;
            }

            if (Vector3.Distance(transform.position, closestSpawner.transform.position) <= tolerance && target == closestSpawner)
            {
                target = player;
                closestSpawner.tag = "Untagged";
                closestSpawner.GetComponent<CoffinBehavior>().SpawnTheThing();
                currentSpawner = listOfSpawners[0];
                closestSpawner = listOfSpawners[0];
            }

            setHeightFromGround();
            transform.rotation = Quaternion.LookRotation(new Vector3(-directionToTarget.x, 0, -directionToTarget.z), Vector3.up);


        }

    }

    private void setHeightFromGround()
    {
        RaycastHit hit;

        Ray heightRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(heightRay, out hit))
        {
            float hoverError = heightFromGround - hit.distance;
            transform.position += new Vector3(0, hoverError, 0);
        }
    }

    private void GetClosestSpawner(string tag)
    {
        listOfSpawners = GameObject.FindGameObjectsWithTag("Spawner");

        if (listOfSpawners.Length > 0)
        {
            for (int i = 0; i < listOfSpawners.Length; i++)
            {
                currentSpawner = listOfSpawners[i];

                if (i == 0)
                {
                    closestSpawner = currentSpawner;
                }
                //fix this here
                if (Vector3.Distance(transform.position, currentSpawner.transform.position) <= Vector3.Distance(transform.position, closestSpawner.transform.position))
                {
                    closestSpawner = currentSpawner;
                }
            }

            Debug.Log("Closest Spawner: " + closestSpawner.transform.position);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= Vector3.Distance(transform.position, closestSpawner.transform.position))
        {
            target = player;
            Debug.Log("Current target:  Player" + player.transform.position);
        }
        else
        {
            target = closestSpawner;
            Debug.Log("Current target:  Spawner" + closestSpawner.transform.position);
        }

    }

}
