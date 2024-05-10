using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game_Timer : MonoBehaviour
{
    private bool timerActive = true;
    public TextMeshProUGUI gameTimeText;
    private float elapsedSeconds = 0f;

    void Update()
    {
        if (timerActive == true)
        {
            elapsedSeconds += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            string minutes = time.Minutes.ToString();
            string seconds = time.Seconds.ToString("00");
            string milliseconds = Mathf.FloorToInt(time.Milliseconds / 10f).ToString("00");
            gameTimeText.text = "<mspace=37px>" + minutes + ":" + seconds + ":" + milliseconds;
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public string GetTime()
    {
        return gameTimeText.text;
    }

    public void ResetTimer()
    {
        elapsedSeconds = 0f;
        gameTimeText.text = "<mspace=37px>00:00:00";
    }
}
// public class Game_Timer : MonoBehaviour
// {
//     private bool timerActive = true;
//     public float currentTime = 0;
//     public TextMeshProUGUI gameTimeText;
//     string minutes = "0.0";
//     string seconds = "0.0";
//     string milliseconds = "0.0";
//     string finalTime;
//     void Update()
//     {
//         if(timerActive ==true)
//         {
//             currentTime += Time.deltaTime;
//         }
//         TimeSpan time = TimeSpan.FromSeconds(currentTime);
//         if ( float.Parse(minutes) < 10f)
//         {
//             minutes = "0" + time.Minutes.ToString();
//         }
//         else
//         {
//             minutes = time.Minutes.ToString();
//         }
//         if ( float.Parse(seconds) < 10f)
//         {
//             seconds = "0"+ time.Seconds.ToString();
//         }
//         else
//         {
//             seconds = time.Seconds.ToString();
//         }
//         milliseconds = Mathf.Round(time.Milliseconds / 10f).ToString("F0");
//         if (float.Parse(milliseconds) < 10f)
//         {
//             milliseconds = "0" + milliseconds;
//         }
//         else if (float.Parse(milliseconds) == 100f )
//         {
//             milliseconds = "99";
//         }
//         gameTimeText.text = "<mspace=37px>" + minutes + ":" + seconds + ":" + milliseconds;
//         finalTime = "<mspace=40px>" + minutes + ":" + seconds + ":" + milliseconds;
//     }
//     public void StartTimer()
//     {
//         timerActive = true;
//     }
//     public void StopTimer()
//     { 
//         timerActive = false;
//     }
//     public void GetTime()
//     {

//     }
//     public void ResetTimer()
//     {
        
//     }
// }