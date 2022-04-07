using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    float senserange = 6f;
    [SerializeField]
    float energy = 10f;
    [SerializeField]
    float reproductionrate;
    Vector3 startingpos;
    [SerializeField]
    int foodcounter;
    [SerializeField]
    float time = 10f;
    bool timeup = false;
    public float startingspeed;
    public float startingsense;
    public float startingrepro;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        startingspeed = Random.Range(3f, 10f);
        agent.speed = startingspeed;
        startingsense = Random.Range(4f, 8f);
        senserange = startingsense;
        startingrepro = Random.Range(1f, 10f);
        reproductionrate = startingrepro;
        agent.stoppingDistance = 2;
        startingpos = agent.transform.position;
        Wander();
    }

    void Update()
    {
        energy -= (senserange * Mathf.Pow(agent.speed, 2) * Time.deltaTime) / 100f;
        time -= Time.deltaTime;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (agent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    Wander();
                }
            }
        }
        if (energy > 0f)
        {
            InvokeRepeating("FoodSearch", 0f, 0.1f);
        }
        else
        {
            if (timeup == false)
            {
                agent.SetDestination(agent.transform.position);
            }
            if (time <= 0f)
            {
                agent.SetDestination(startingpos);
                timeup = true;
                agent.speed = 5f;
                if (time < -15f)
            {
                timeup = false;
                agent.speed = startingspeed;
                energy = 10f;
                time = 10f;
                Sex();
            }
            } 
            
        }
        
    }

    void FoodSearch()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, senserange);
        GameObject closest = null;
        float closestDist = Mathf.Infinity;
        bool found = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Food"))
            {
                float dist = Vector3.Distance(transform.position, hitCollider.gameObject.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = hitCollider.gameObject;
                    found = true;
                }

            }
        }
        if (found)
        {
            CancelInvoke();
            agent.SetDestination(closest.transform.position);
        }
    }

    void Wander()
    {
        agent.SetDestination(new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
    }

    public void Sex()
    {
        if (foodcounter == 0)
        {
            Destroy(gameObject);
        }
        else if (foodcounter >= 1)
        {
            foodcounter = 0;
            GameObject obj = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            NavMeshAgent objAgent = obj.GetComponent<NavMeshAgent>();
            obj.GetComponent<AgentMove>().startingspeed = Random.Range(startingspeed -.5f, startingspeed + .5f);
            obj.GetComponent<AgentMove>().startingsense = Random.Range(startingsense -.5f, startingsense + .5f);
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && energy > 0f)
        {
            Destroy(other.gameObject);
            foodcounter++;
        }
        

    }
}

