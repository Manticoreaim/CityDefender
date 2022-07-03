using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform BaseTurretTransform;
    [SerializeField] private Transform TurretTransform;
    [SerializeField] private float SpeedRetyrnTurret = 5;

    [SerializeField] private float recoilAfterShot = 1;
    private float ThisPositionTurretZ = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShotTurret(Vector3 PositionShot)
    {
        BaseTurretTransform.LookAt(PositionShot);
        TurretTransform.localPosition = Vector3.back * recoilAfterShot;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(TurretTransform.localPosition.z != 0)
        {
            TurretTransform.localPosition = Vector3.Lerp(TurretTransform.localPosition, Vector3.zero, SpeedRetyrnTurret * Time.deltaTime);
        }
        
    }
}
