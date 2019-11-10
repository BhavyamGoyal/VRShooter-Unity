using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFollow 
{
    NavMeshAgent zombieAgent;
    Animator anim; 
    Transform playerTransform;
    bool alive = true;
    public ZombieFollow(GameObject zombiePrefab, Transform playerTransform,string name)
    {
        this.playerTransform = playerTransform;
        Vector3 pos=new Vector3(Random.Range(-70,70),2,Random.Range(-70,70));
        //new Vector3(38f, 2.81f, 27f)
        zombieAgent = GameObject.Instantiate(zombiePrefab,pos ,Quaternion.identity).GetComponent<NavMeshAgent>();
        zombieAgent.gameObject.name=name;
        anim=zombieAgent.gameObject.GetComponent<Animator>();
        zombieUpdate();
    }
    ~ZombieFollow()
    {
        alive = false;
        Debug.Log("[ZombieFollow]zombieDestroyed");
    }

   public async void zombieUpdate()
    {
        while (alive)
        {
            await Task.Delay(1000);
            Debug.Log("[ZombieFollow]zombieMoving");
            zombieAgent.SetDestination(playerTransform.position);
        }
    }

  
}
