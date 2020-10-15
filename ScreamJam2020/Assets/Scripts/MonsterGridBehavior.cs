using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGridBehavior : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Vector3 directionToPlayer;
    private float xDirectionSign;
    private float zDirectionSign;

    public void MonsterMovement()
    {
        //Find vector between player and monster, ask whether x and z directions are each positive or negative.
        //Verticality does not matter, so ignore y component.
        directionToPlayer = transform.position - player.transform.position;
        xDirectionSign = directionToPlayer.x / Mathf.Abs(directionToPlayer.x);
        zDirectionSign = directionToPlayer.z / Mathf.Abs(directionToPlayer.z);


        if (Mathf.Abs(directionToPlayer.x) >= Mathf.Abs(directionToPlayer.z))
        {
            //Move monster one space along grid in the x direction toward player
            transform.position += new Vector3(1, 0, 0) * -xDirectionSign;
        }
        else
        {
            //Move monster one space along grid in the z direction toward player
            transform.position += new Vector3(0, 0, 1) * -zDirectionSign;
        }
    }
}
