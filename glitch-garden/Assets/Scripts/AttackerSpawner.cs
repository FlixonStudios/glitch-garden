using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] bool spawn = true;
    [SerializeField] float initialDelay = 5f;
    
    [SerializeField] float baseSpawnTime = 1f;
    [SerializeField] float adjustedBaseSpawnTime = 1f;
    [SerializeField] float minSpawnTime = 0.5f;    
    
    [SerializeField] int MaxSpawnCount = 100;
    [SerializeField] [Range(0, 3)] float rampRate = 0.1f;

    [Range(0, 5)][SerializeField] float spawnTimeRandomiser = 0f;
    [SerializeField] Attacker[] attackerPrefabArray;
    
    int chosenAttackerIndex = 0;
    [SerializeField] int currentSpawnCount = 0;

    void Start()
    {

        adjustedBaseSpawnTime = baseSpawnTime;
        AdjustForDifficulty();
        StartCoroutine(DelaySpawnForStart());
              
    }
    IEnumerator DelaySpawnForStart()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TriggerSpawning());
    }
    IEnumerator TriggerSpawning()
    {
        for (currentSpawnCount = 0; currentSpawnCount < MaxSpawnCount; currentSpawnCount++)
        {
            if (!spawn)
            {
                break;
            }
            yield return new WaitForSeconds(RandomiseSpawnTime());
            SpawnAttacker();
        }
    }
    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        chosenAttackerIndex = PickRandomAttacker();
        Spawn(chosenAttackerIndex);        
    }

    private int PickRandomAttacker()
    {
        return Random.Range(0, attackerPrefabArray.Length);
    }

    private void Spawn(int attackerIndex)
    {
        Attacker newAttacker = Instantiate(attackerPrefabArray[attackerIndex], 
                                            transform.position, Quaternion.identity) as Attacker;
        newAttacker.transform.parent = transform;
    }

    private float RandomiseSpawnTime()
    {
        float rampedBaseSpawnTime = adjustedBaseSpawnTime;

        rampedBaseSpawnTime = DetermineRampedAdjustedTime(rampedBaseSpawnTime);
        
        float randomSpawnTime = Mathf.Clamp(rampedBaseSpawnTime + UnityEngine.Random.Range(0.0f, spawnTimeRandomiser),
                                rampedBaseSpawnTime, rampedBaseSpawnTime + spawnTimeRandomiser);
        return randomSpawnTime;
    }
    public void AdjustForDifficulty()
    {
        int difficulty = PlayerPrefsController.GetDifficulty();

        adjustedBaseSpawnTime = baseSpawnTime / difficulty;
    }
    private float DetermineRampedAdjustedTime(float adjustedBaseSpawnTime)
    {
        float rampedSpawnTime = adjustedBaseSpawnTime;                      

        if (FindObjectOfType<GameTimer>())
        {
            float timerPercentage = FindObjectOfType<GameTimer>().GetTimerPercentage();
            if (rampRate > 0)
            {
                rampedSpawnTime = rampedSpawnTime * ((1 - timerPercentage) / rampRate);
                rampedSpawnTime = ClampRampSpawnTimeToMin(rampedSpawnTime);
            }
            Debug.Log(rampedSpawnTime + ", " + timerPercentage);
        }        
        return rampedSpawnTime;
    }

    private float ClampRampSpawnTimeToMin(float rampedSpawnTime)
    {
        if (rampedSpawnTime < minSpawnTime)
        {
            rampedSpawnTime = minSpawnTime;
        }
        return rampedSpawnTime;
    }
}
