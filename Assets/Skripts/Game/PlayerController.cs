using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public bool ActiveShot = false;
    public GameObject test;

    [SerializeField] private string TagEnemy = "Enemy";
    [SerializeField] private string TagGameController = "GameController";
    [SerializeField] private Camera CamerePlayer;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Turret turret;

     private bool GunOverload = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CamerePlayer.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if(Physics.Raycast(ray,out Hit, 200, layerMask))
            {
                if(Hit.collider.gameObject.tag == TagGameController) return;
                if(Hit.collider.gameObject.tag == TagEnemy)
                {
                    if(GunOverload)
                    {
                        Debug.Log("dsrfhggggggggggghggggggggggggg");
                        Hit.collider.gameObject.GetComponent<Enemy>().DamageEnemy(100000);
                        GunOverload = false;
                    }    
                    Hit.collider.gameObject.GetComponent<Enemy>().DamageEnemy();
                }
                test.transform.position = Hit.point;
                turret.ShotTurret(Hit.point);
                test.SetActive(true);
            }
        }
        else
        {
            

        }
    }


    public void ActiveGunOverload()
    {
        GunOverload = true;
    }
}
