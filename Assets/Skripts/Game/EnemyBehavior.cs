using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceEnemyBehavior
{

    public void SetSetingsBehavior(Rigidbody RigidbodyEnemy);
    public void SetSetingsDifficulty(float Difficulty);
    public T GetReactionToDamage<T> ();
    public void UpdeteBehavior();

}

public abstract class EnemyBaseBehavior
{


    public abstract void SetSetingsBehavior(Rigidbody RigidbodyEnemy);
    public abstract void SetSetingsDifficulty(float Difficulty);
    public abstract void GetReactionToDamage( out EnemyReactionToDamage Delegate);
    public abstract void UpdeteBehavior(Vector3 VectorJamp);
    public abstract float GetHPBehavior();
    public abstract float FactorDamageWallBehavior();
}



public class EnemyTestBehavior : EnemyBaseBehavior
{

    private Rigidbody RigidbodyEnemy;
    private float EnemyHP = 0.1f;
    private float FactorDamageWall = 1;




    public override void SetSetingsBehavior( Rigidbody RigidbodyEnemy)
    {
        this.RigidbodyEnemy = RigidbodyEnemy;
    }
    public override void SetSetingsDifficulty(float Difficulty)
    {
    }

    public override void UpdeteBehavior(Vector3 VectorJamp)
    {
        Jamp(VectorJamp);
    }

    private void Jamp(Vector3 VectorJamp)
    {
        RigidbodyEnemy.AddForce(VectorJamp , ForceMode.Impulse);
    }



    public override void GetReactionToDamage(out EnemyReactionToDamage Delegate)
    {
        EnemyReactionToDamage D = new EnemyReactionToDamage(ReactionToDamage);
        Delegate = D;
    }

    public override float FactorDamageWallBehavior()
    {
        return FactorDamageWall;
    }

    public override float GetHPBehavior()
    {
        return EnemyHP;
    }


    IEnumerator ReactionToDamage()
    {
        Vector3 OldSize = RigidbodyEnemy.transform.localScale;
        for (int i = 0; i < 5; i++)
        {
            RigidbodyEnemy.transform.localScale = RigidbodyEnemy.transform.localScale - Vector3.one * 0.2f;
            yield return new WaitForFixedUpdate();
        }

        RigidbodyEnemy.transform.localScale = OldSize;
    }
}

public class EnemyHaveBehavior : EnemyBaseBehavior
{

    private Rigidbody RigidbodyEnemy;
    private float EnemyHP = 2f;
    private float FactorDamageWall = 3;




    public override void SetSetingsBehavior( Rigidbody RigidbodyEnemy)
    {
        this.RigidbodyEnemy = RigidbodyEnemy;
    }
    public override void SetSetingsDifficulty(float Difficulty)
    {
    }

    public override void UpdeteBehavior(Vector3 VectorJamp)
    {
        Jamp(VectorJamp);
    }

    private void Jamp(Vector3 VectorJamp)
    {
        RigidbodyEnemy.AddForce(VectorJamp / 3, ForceMode.Impulse);
    }

    public override float FactorDamageWallBehavior()
    {
        return FactorDamageWall;
    }



    public override void GetReactionToDamage(out EnemyReactionToDamage Delegate)
    {
        EnemyReactionToDamage D = new EnemyReactionToDamage(ReactionToDamage);
        Delegate = D;
    }

    public override float GetHPBehavior()
    {
        return EnemyHP;
    }


    IEnumerator ReactionToDamage()
    {
        Vector3 OldSize = RigidbodyEnemy.transform.localScale;
        for (int i = 0; i < 5; i++)
        {
            RigidbodyEnemy.transform.localScale = RigidbodyEnemy.transform.localScale - Vector3.one * 0.2f;
            yield return new WaitForFixedUpdate();
        }
        RigidbodyEnemy.transform.localScale = OldSize;
    }
}


