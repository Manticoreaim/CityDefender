using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnPoint : MonoBehaviour
{
    private bool ActiveSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveSpawn = true;
    }

    private void OnTriggerStay(Collider other)
    {
        ActiveSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        ActiveSpawn = true;
    }

    public bool SpawnPointEnemy( out Vector3 PositionSpawn)
    {
        PositionSpawn = this.gameObject.transform.position;
        return ActiveSpawn;

    }

}
