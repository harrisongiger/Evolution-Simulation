using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public int foodCount;
    public GameObject Food;
    void Start()
    {
        for (var i = 0; i < foodCount; i++) {
            Instantiate(Food, new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f)), Quaternion.identity);
        }
    }
}
