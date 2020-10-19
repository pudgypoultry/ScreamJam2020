using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGridBehavior : MonsterGridBehavior
{
    private GameObject player;
    private Vector3 directionToPlayer;
    private float xDirectionSign;
    private float zDirectionSign;
    private int speed = 1;
    private bool readyToMove = false;

    private void Start()
    {
        player = Camera.main.gameObject;
    }

    public override void MonsterMovement()
    {

        directionToPlayer = (transform.position - player.transform.position).normalized;
        xDirectionSign = directionToPlayer.x / Mathf.Abs(directionToPlayer.x);
        zDirectionSign = directionToPlayer.z / Mathf.Abs(directionToPlayer.z);

        if (readyToMove)
        {
            if (Mathf.Abs(directionToPlayer.x) >= Mathf.Abs(directionToPlayer.z))
            {
                //Move monster one space along grid in the x direction toward player
                transform.position += new Vector3(speed, 0, 0) * -xDirectionSign;
            }
            else
            {
                //Move monster one space along grid in the z direction toward player
                transform.position += new Vector3(0, 0, speed) * -zDirectionSign;
            }

            readyToMove = false;
            Debug.Log("Just moved");
        }
        else
        {
            readyToMove = true;
            Debug.Log("Ready to move");
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(-directionToPlayer.x, 0, -directionToPlayer.z), Vector3.up);

    }

}
