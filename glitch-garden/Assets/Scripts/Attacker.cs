using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [Range(0f, 5f)] [SerializeField] float baseSpeed = 1.0f;
    [SerializeField] float baseDamage = 1.0f;
    [SerializeField] Vector3 offsetFromSpawn = new Vector3(0f, 0f, 0f);

    [SerializeField] float currentSpeed = 0.0f;
    [SerializeField] float currentDamage = 0.0f;
    GameObject currentTarget;
    Projectile projectile;
    float damageModifier = 0.0f;
    Health health;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();   
    }
    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
        {
            levelController.AttackerKilled();
        }
    }

    void Start()
    {
        currentSpeed = baseSpeed;
        currentDamage = baseDamage;
    }

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }        
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }
    public void ResetMovementSpeed()
    {
        currentSpeed = baseSpeed;
    }
    
    private void ProcessHit()
    {
        if (!projectile)
        {
            return;
        }
        ProcessHealth(gameObject, projectile.DealDamage() + damageModifier);     
    }

    private void ProcessHealth(GameObject target, float damage)
    {
        health = target.GetComponent<Health>();

        if (!health)
        {
            return;
        }
        health.ReceiveDamage(damage);
        health.CheckDeath();
    }

    public Vector3 SendAttackerOffset()
    {
        return offsetFromSpawn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        projectile = other.gameObject.GetComponent<Projectile>();
        ProcessHit();
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }
    public void StrikeCurrentTarget()
    {
        if (!currentTarget)
        {
            //Resume();
            return;
        }
        ProcessHealth(currentTarget, currentDamage);
    }
    public void Resume()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }
    public void DamageModifier(float reduceDamage = 0.0f)
    {
        damageModifier = -reduceDamage;
        
    }

}
