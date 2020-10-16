using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGridBehavior : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float heightFromGround;
    private Vector3 directionToPlayer;
    private float xDirectionSign;
    private float zDirectionSign;
    private int speed = 1;

    public void MonsterMovement()
    {
        //Find vector between player and monster, ask whether x and z directions are each positive or negative.
        //Verticality does not matter, so ignore y component.
        directionToPlayer = (transform.position - player.transform.position).normalized;
        xDirectionSign = directionToPlayer.x / Mathf.Abs(directionToPlayer.x);
        zDirectionSign = directionToPlayer.z / Mathf.Abs(directionToPlayer.z);


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

        setHeightFromGround();
        transform.LookAt(player.transform, Vector3.up);
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
}
