using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{

    [SerializeField] private float gridSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GridMovement();
    }

    private void GridMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) && ValidMoveForward())
        {
            transform.position += transform.forward * gridSize;
        }

        if (Input.GetKeyDown(KeyCode.S) && ValidMoveBackward())
        {
            transform.position -= transform.forward * gridSize;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation *= Quaternion.Euler(0, -90, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
    }

    private bool ValidMoveForward()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        Debug.Log("Something is in front of the player: " + Physics.Raycast(transform.position, transform.forward, gridSize));
        if (Physics.Raycast(transform.position, transform.forward, gridSize))
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
        if (Physics.Raycast(transform.position, -transform.forward, gridSize))
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
}
