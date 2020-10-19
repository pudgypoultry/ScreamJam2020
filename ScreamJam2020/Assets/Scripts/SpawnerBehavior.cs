using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject[] zombies;

    private int listLength;


    private void Start()
    {

    }

    public void SpawnZombie()
    {
        Debug.Log("Made it here");
        listLength = zombies.Length;
        Instantiate(zombies[Random.Range(0, listLength)], transform.position, transform.rotation);
        Debug.Log("Zombie spawned at: " + transform.position);

    }

}
