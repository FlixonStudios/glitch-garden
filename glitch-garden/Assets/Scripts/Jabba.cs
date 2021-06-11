using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabba : MonoBehaviour
{
    [SerializeField] float armorValue = 1;
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if (otherObject.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(otherObject);
        }
    }
    private void Start()
    {
        GetComponent<Attacker>().DamageModifier(armorValue);
    }
}
