using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostDeadEnemy : MonoBehaviour
{
    [SerializeField] private string TagEnemy = "Enemy";
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag == TagEnemy)
        {
            other.gameObject.GetComponent<Enemy>().DamageEnemy(10000);
        }
    }

   
}
