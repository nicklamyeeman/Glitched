using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Completed 
{

public class EnemyTerritory : MonoBehaviour
{
    public GameObject player;
    public BasicEnemy basicenemy;
    bool playerInTerritory = false;

    public void test()
    {

    }

    void Update()
    {
        if ((player.transform.position - transform.position).sqrMagnitude < 4) {
            basicenemy.MoveToPlayer();
        }
        else {
            basicenemy.Rest();
        }
    }
}
}