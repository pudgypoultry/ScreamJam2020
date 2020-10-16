using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{

    [SerializeField] private GameObject player, target;
    [SerializeField] private float gridSize = 1;
    [SerializeField] private float heightFromGround;
    [SerializeField] private float cameraXMin, cameraXMax, cameraYMin, cameraYMax, cameraSpeedX, cameraSpeedY;
    private float cameraX, cameraY;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        GridMovement();
    }

    private void GridMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) && ValidMoveForward())
        {
            player.transform.position += player.transform.forward * gridSize;
            setHeightFromGround();
            MakeMonsterMove();
        }

        if (Input.GetKeyDown(KeyCode.S) && ValidMoveBackward())
        {
            player.transform.position -= player.transform.forward * gridSize;
            setHeightFromGround();
            MakeMonsterMove();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.LookAt(target.transform, Vector3.up);
            player.transform.rotation *= Quaternion.Euler(0, -90, 0);
            cameraXMin -= 90;
            cameraXMax -= 90;
            transform.LookAt(target.transform, Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.rotation *= Quaternion.Euler(0, 90, 0);
            cameraXMin += 90;
            cameraXMax += 90;
            transform.LookAt(target.transform, Vector3.up);
        }
    }

    private bool ValidMoveForward()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        Debug.Log("Something is in front of the player: " + Physics.Raycast(transform.position, transform.forward, gridSize));
        if (Physics.Raycast(player.transform.position, player.transform.forward, gridSize))
        {
            Debug.Log("Not a valid move.");
            return false;
        }
        else
        {
            Debug.Log("Valid move.");
            return true;
        }
    }

    private bool ValidMoveBackward()
    {
        Debug.DrawRay(transform.position, -transform.forward, Color.green);
        Debug.Log("Something is in front of the player: " + Physics.Raycast(transform.position, -transform.forward, gridSize));
        if (Physics.Raycast(player.transform.position, -player.transform.forward, gridSize))
        {
            Debug.Log("Not a valid move.");
            return false;
        }
        else
        {
            Debug.Log("Valid move.");
            return true;
        }
    }

    private void MakeMonsterMove()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Found enemies.");
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.GetComponent<MonsterGridBehavior>().MonsterMovement();
            Debug.Log("Monster moved.");
        }
    }

    private void setHeightFromGround()
    {
        RaycastHit hit;

        Ray heightRay = new Ray(transform.position, Vector3.down);
        
        if(Physics.Raycast(heightRay, out hit))
        {
            float hoverError = heightFromGround - hit.distance;
            player.transform.position += new Vector3(0, hoverError, 0);
        }
    }

    private void CameraRotation()
    {
        cameraX += cameraSpeedX * Input.GetAxis("Mouse X");
        cameraY -= cameraSpeedY * Input.GetAxis("Mouse Y");
        cameraY = Mathf.Clamp(cameraY, cameraYMin, cameraYMax);
        cameraX = Mathf.Clamp(cameraX, cameraXMin, cameraXMax);
        transform.eulerAngles = new Vector3(0, cameraX, 0.0f);
        transform.eulerAngles = new Vector3(cameraY, cameraX, 0);
    }
}
