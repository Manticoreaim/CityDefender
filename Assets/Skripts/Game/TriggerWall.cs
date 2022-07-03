using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour
{
    [SerializeField] private HPWalls MyHPWall;

    [Tooltip("0 - Левы; 1 - центральный; 2 - правый; ")]
    [SerializeField] private int IndexMyWall;

    [SerializeField] private string TagEnemy = "Enemy";
    [SerializeField] private float TimeFixUpdete = 0.02f;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == TagEnemy)
        {
            MyHPWall.DamageWals(IndexMyWall, other.gameObject.GetComponent<Enemy>().DamageForWall() * TimeFixUpdete);
        }

    }
}
