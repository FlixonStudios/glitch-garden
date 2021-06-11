using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] int numberOfAttackers = 0;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject levelLossAudio;
    [SerializeField] float levelCompleteDelay = 5;

    bool levelComplete = false;
    bool levelTimerFinished = false;
    bool allAttackersKilled = false;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }
    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }
    public void AttackerKilled()
    {
        numberOfAttackers--;
        AllAttackersKilled();
        
    }

    private void CheckLevelEnd()
    {
        if (allAttackersKilled && levelTimerFinished)
        {
            Debug.Log("end level now");
            
            StartCoroutine(HandleWinCondition());
        }
    }

    public void AllAttackersKilled()
    {
        if (numberOfAttackers <= 0)
        {
            allAttackersKilled = true;
        }
        CheckLevelEnd();
    }
    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
        CheckLevelEnd();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        if (!levelComplete)
        {
            PlayLevelEndAudio();
            levelComplete = true;
        }        
        yield return new WaitForSeconds(levelCompleteDelay);
        LoadNextLevel();
    }
    public void HandleLoseCondition()
    {
        if(!levelComplete)
        {
            loseLabel.SetActive(true);
            PlayLevelLossAudio();
            levelComplete = true;
        }
        
        Time.timeScale = 0;
    }

    private static void LoadNextLevel()
    {
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        if (!levelLoader)
        {
            return;
        }
        levelLoader.LoadNextScene();
    }

    private void PlayLevelEndAudio()
    {
        GetComponent<AudioSource>().Play();
    }
    private void PlayLevelLossAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
