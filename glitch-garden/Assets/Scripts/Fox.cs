using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    [SerializeField] int maxJumpCount = 1;  
    [SerializeField] bool jumpLimit = true;

    [SerializeField] int currentJumpCount = 0;

    GameObject otherObject;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        otherObject = otherCollider.gameObject;
        
        if(IsWall())
        {
            CheckIfThereIsJumpLimit();
        }
        else if (IsDefender())
        {
            TriggerFoxAttack();
        }

    }

    private void CheckIfThereIsJumpLimit()
    {
        if (jumpLimit)
        {
            CheckJumpLimitToJumpOrAttack();
        }
        else
        {
            TriggerFoxJump();
        }
    }

    private void CheckJumpLimitToJumpOrAttack()
    {
        if (CheckLimit())
        {
            TriggerFoxJump();
        }
        else
        {
            TriggerFoxAttack();
        }
    }

    private bool CheckLimit()
    {
        return (currentJumpCount < maxJumpCount);        
    }

    private bool IsWall()
    {
        return (otherObject.GetComponent<Wall>());        
    }
    private bool IsDefender()
    {
        return (otherObject.GetComponent<Defender>());
    }
    private void TriggerFoxJump()
    {        
        GetComponent<Animator>().SetTrigger("jumpTrigger");
        currentJumpCount++;
    }
    private void TriggerFoxAttack()
    {
         GetComponent<Attacker>().Attack(otherObject);
    }



}
