using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinBehavior : MonoBehaviour
{

    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject coffinBody;
    [SerializeField] private GameObject player;
    [SerializeField] private int delay;
    private int firstCount = 10000000;
    private bool readyToSpawn;
    private bool alreadySpawned;
    private int finalCount = 10000000;

    private void Start()
    {
        coffinBody.SetActive(false);
        player = Camera.main.gameObject;
        readyToSpawn = false;
    }

    private void Update()
    {
        Debug.Log(firstCount + ", " + finalCount);
        finalCount = firstCount + delay;

        if(player.GetComponent<GridBehavior>().counter == finalCount && !alreadySpawned)
        {
            alreadySpawned = true;
            spawner.GetComponent<SpawnerBehavior>().SpawnZombie();
        }

    }

    public void SpawnTheThing()
    {
        coffinBody.SetActive(true);
        firstCount = player.GetComponent<GridBehavior>().counter;
    }

}
