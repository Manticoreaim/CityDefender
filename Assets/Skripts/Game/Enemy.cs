using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public delegate IEnumerator EnemyReactionToDamage();

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody EnemyRigedbody;
    [SerializeField] private GameObject[] EnemyVisual;
    [SerializeField] private float ForceJampEnemyUp = 10;
    [SerializeField] private float ForceJampEnemyForward = 14;
    [SerializeField] private float TimeSlowdown = 7;

    private EnemyBaseBehavior[] EnemyBehaviors;
    

    
    private bool EnemyIsStanding = true;
    private bool EnemyIsSlowdown = false;
    private bool ActiveUpdete = true;
    private int IndexEnemyBehavior = 0;
    private float Difficulty = 1;
    private float HPEnemy;
    private float HPEnemyBase;



    private EnemyReactionToDamage DelegateReactionToDamage;

    private void Awake()
    {
        InitializationEnemy();
    }
    private void InitializationEnemy()
    {
        EnemyBehaviors = new EnemyBaseBehavior[2];
        EnemyBehaviors[0] = new EnemyTestBehavior();
        EnemyBehaviors[1] = new EnemyHaveBehavior();

        EnemyBehaviors[0].SetSetingsBehavior(EnemyRigedbody);
        EnemyBehaviors[1].SetSetingsBehavior(EnemyRigedbody);

        for(int i = 0; i < EnemyVisual.Length; i++)
        {
            EnemyVisual[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (ActiveUpdete && !EnemyIsSlowdown)
        {
            EnemyBehaviors[IndexEnemyBehavior].UpdeteBehavior((Vector3.up * ForceJampEnemyUp - Vector3.forward * ForceJampEnemyForward * Difficulty));
            StartCoroutine(JampCooldown());
        }
    }

    public void SetPositionEnemy(Vector3 Position)
    {
        this.gameObject.transform.position = Position;
    }

    public void SetSetingsEnemy(int TypeEnemy, float Difficulty)
    { 
        if(TypeEnemy >= 0 && TypeEnemy <= EnemyBehaviors.Length -1)
        {
            this.Difficulty = Difficulty;
            EnableEnemy(TypeEnemy);
        }
        else
        {
            Debug.Log("The enemy has not this index Enemy Behavior ||  TypeEnemy = " + TypeEnemy);
            this.gameObject.SetActive(false);
            return;
        }
        
    }

    public void StopEnemy(float TimeForStop)
    {
    }

    public void SlowdownEnemy()
    {
        StartCoroutine(SlowdownEnemyBoost());
    }
    public float DamageForWall()
    {
        return Difficulty * EnemyBehaviors[IndexEnemyBehavior].FactorDamageWallBehavior();
    }

    public void DamageEnemy(float Damaje = 1f)
    {

        HPEnemy -= Damaje;
        Debug.Log(HPEnemy);
        if (HPEnemy < 0)
        {
            
            DeadEmemy();
            return;
        }
        
        StartEnemyReactionToDamage();

    }






    private void StartEnemyReactionToDamage()
    {
        if(DelegateReactionToDamage != null)
        {
            StartCoroutine(DelegateReactionToDamage());
            //DelegateReactionToDamage.Invoke();
        }
    }

    private void EnableEnemy(int TypeEnemy)
    {
        IndexEnemyBehavior = TypeEnemy;
        EnemyBehaviors[IndexEnemyBehavior].SetSetingsDifficulty(Difficulty);
        EnemyBehaviors[IndexEnemyBehavior].GetReactionToDamage(out DelegateReactionToDamage);
        HPEnemy = EnemyBehaviors[IndexEnemyBehavior].GetHPBehavior() * Difficulty;
        ActiveUpdete = true;
        EnemyIsSlowdown = false;

        EnemyVisual[IndexEnemyBehavior].SetActive(true);
    }

    private void DeadEmemy()
    {
        EnemyVisual[IndexEnemyBehavior].SetActive(false);

        EnemyRigedbody.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    IEnumerator JampCooldown()
    {
        ActiveUpdete = false;
        yield return new WaitForSeconds(1);
        ActiveUpdete = true;
    }

    IEnumerator SlowdownEnemyBoost()
    {
        EnemyIsSlowdown = true;
        yield return new WaitForSeconds(TimeSlowdown);
        EnemyIsSlowdown = false;
    }

}
