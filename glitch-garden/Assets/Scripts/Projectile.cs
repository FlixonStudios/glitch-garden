using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileVelocity = 1.0f;
    [SerializeField] float projectileDamage = 1.0f;
    [SerializeField] bool projectileRotationOn = false;
    [SerializeField] float projectileRotationSpeed = 60f;

    void Update()
    {
        MoveProjectileRight();
        if (projectileRotationOn)
        {
            RotateProjectileCW();
        }
    }

    private void RotateProjectileCW()
    {
        transform.Rotate(0,0, projectileRotationSpeed * Time.deltaTime,Space.World);
    }

    private void MoveProjectileRight()
    {
        transform.Translate(Vector2.right * projectileVelocity * Time.deltaTime, Space.World);
    }

    public float DealDamage()
    {
        float damageDealt = projectileDamage;

        return damageDealt;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Attacker>())
        {
            Destroy(gameObject);
        }
    }
    
}
