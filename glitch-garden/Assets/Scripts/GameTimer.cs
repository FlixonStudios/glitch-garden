using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour
{
    [Tooltip("Time in seconds")]
    [SerializeField] float levelTime = 5;
    bool triggeredLevelFinished = false;

    void Update()
    {
        if (triggeredLevelFinished)
        {
            return;
        }
        
        GetComponent<Slider>().value = GetTimerPercentage();
        
        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        
        if (timerFinished)
        {
            FindObjectOfType<LevelController>().LevelTimerFinished();
            triggeredLevelFinished = true;
        }
    }
    public float GetTimerPercentage()
    {
        float percentageOfTimeLeft = Time.timeSinceLevelLoad / levelTime;

        return percentageOfTimeLeft;
    }
}
