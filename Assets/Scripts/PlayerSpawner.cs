using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner shared;

    public GameObject Agent;
    public int AgentCount;
    public int foodCount;
    public GameObject Food;
    public GameObject[] AllAgents;
    float energy = 10f;
    [SerializeField]
    int day = 0;
    
    void Start()
    {
        shared = this;
        AllAgents = new GameObject[AgentCount];
        for (var i = 0; i < AgentCount; i++) {
        float randomnum = Mathf.Floor(Random.Range(0f, 4f));
            if (randomnum == 0)
            {
                GameObject obj = Instantiate(Agent, new Vector3(55f, 1f, Random.Range(-50f, 50f)), Quaternion.identity);
      //          AllAgents[i] = (obj);
                  
            }
            else if (randomnum == 1)
            {

                GameObject obj = Instantiate(Agent, new Vector3(-55f, 1f, Random.Range(-50f, 50f)), Quaternion.identity);
      //          AllAgents[i] = (obj);
            }
            else if (randomnum == 2)
            {
                GameObject obj = Instantiate(Agent, new Vector3(Random.Range(-50f, 50f), 1f, 55f), Quaternion.identity);
      //          AllAgents[i] = (obj);
            }
            else if (randomnum == 3)
            {
                GameObject obj = Instantiate(Agent, new Vector3(Random.Range(-50f, 50f), 1f, -55f), Quaternion.identity);
      //          AllAgents[i] = (obj);
            }
        }

        FoodSpawn();
        
    }

    private void FoodSpawn()
    {
        for (var i = 0; i < foodCount; i++)
        {
            Instantiate(Food, new Vector3(Random.Range(-49f, 49f), 1, Random.Range(-49f, 49f)), Quaternion.identity);
        }
    }

    private void Update()
    {
        energy -= Time.deltaTime;
        if (energy < -15f)
        {
            day++;
            energy = 10f;
            FoodSpawn();
        }
    }
}
