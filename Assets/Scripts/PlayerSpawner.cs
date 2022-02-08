using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Agent;
    public int AgentCount;

    void Start()
    {
        for (var i = 0; i < AgentCount; i++) {
        float randomnum = Mathf.Floor(Random.Range(0f, 4f));
        switch (randomnum) {
            case 0: 
            Instantiate(Agent, new Vector3(50f, 1f, Random.Range(-50f, 50f)), Quaternion.identity);
            break;
            case 1: 
            Instantiate(Agent, new Vector3(-50f, 1f, Random.Range(-50f, 50f)), Quaternion.identity);
            break;
            case 2: 
            Instantiate(Agent, new Vector3(Random.Range(-50f, 50f), 1f, 50f), Quaternion.identity);
            break;
            case 3: 
            Instantiate(Agent, new Vector3(Random.Range(-50f, 50f), 1f, -50f), Quaternion.identity);
            break;
        }

        }
    }
}
