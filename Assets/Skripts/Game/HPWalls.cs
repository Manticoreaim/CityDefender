using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPWalls : MonoBehaviour
{

    [SerializeField] private float StartHPWalls = 1000;

    [SerializeField] private float MaxWallHeight = 10;
    [SerializeField] private float OffSetWallHeight = 5;

    [Header("0 - Левы; 1 - центральный; 2 - правый; ")]
    [SerializeField] private Transform[] WallsVisual;
    [Tooltip("0 - Левы; 1 - центральный; 2 - правый; ")]
    [SerializeField] private GameObject[] WallsCollider;

    [SerializeField] private GameManeger GM;


    private float[] HPWallsArrey;


    private void Awake()
    {
        // 0 - Левы; 1 - центральный; 2 - правый; 
        HPWallsArrey = new float[3] { StartHPWalls, StartHPWalls, StartHPWalls };
    }



    public void DamageWals(int IndexWall, float DamageOfWall)
    {
        HPWallsArrey[IndexWall] = HPWallsArrey[IndexWall] - DamageOfWall;
        MoveWall(IndexWall);
        if (HPWallsArrey[IndexWall] < 0)
        {
            EndGame();
            WallsCollider[IndexWall].SetActive(false);
        }
    }
    
    public void AddHPWalls(int IndexWall, float HPForWall)
    {
        HPWallsArrey[IndexWall] += HPForWall;
        if (HPWallsArrey[IndexWall] > StartHPWalls) HPWallsArrey[IndexWall] = StartHPWalls;
        MoveWall(IndexWall);
    }
    
    
    public void NewGame()
    {
        HPWallsArrey[0] = StartHPWalls;
        HPWallsArrey[1] = StartHPWalls;
        HPWallsArrey[2] = StartHPWalls;

        MoveWall(0);
        MoveWall(1);
        MoveWall(2);

        WallsCollider[0].SetActive(true);
        WallsCollider[1].SetActive(true);
        WallsCollider[2].SetActive(true);
    }


    private void MoveWall(int IndexWall)
    {
        Debug.Log("Move Wall index " + IndexWall);
        float Xposition = WallsVisual[IndexWall].position.x;
        float Yposition = (MaxWallHeight / StartHPWalls) * HPWallsArrey[IndexWall];
        float Zposition = WallsVisual[IndexWall].position.z;

        WallsVisual[IndexWall].position = new Vector3(Xposition, Yposition - OffSetWallHeight, Zposition);
    }

    public void StartGame()
    {
        for(int i = 0; i < HPWallsArrey.Length; i++)
        {
            HPWallsArrey[i] = StartHPWalls;
            MoveWall(i);
            WallsCollider[i].SetActive(true);
        }
    }

    private void EndGame()
    {
        GM.WallsDead();
    }
}
