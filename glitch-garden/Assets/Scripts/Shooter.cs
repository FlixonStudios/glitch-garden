using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile, gun;
    GameObject projectileParent;
    List<AttackerSpawner> myLaneSpawner = new List<AttackerSpawner>();
    Animator animator;
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }
    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }
    private void Update()
    {
        if (IsAttackerInLane())
        {            
            animator.SetBool("isAttacking", true);            
        }
        else
        {            
            animator.SetBool("isAttacking", false);
        }
    }

    private bool IsAttackerInLane()
    {
        bool attackerIsInLane = false;
        foreach (AttackerSpawner validSpawner in myLaneSpawner)
        {
            if (validSpawner.transform.childCount > 0)
            {
                attackerIsInLane = true;                
            }
        }
        return attackerIsInLane;
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, transform.rotation) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
    }
    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        //int validSpawner = 0;
        foreach (AttackerSpawner spawner in spawners)
        {
            bool IsCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            if (IsCloseEnough)
            {
                myLaneSpawner.Add(spawner);
                //validSpawner++;
            }
        }
    }
}
