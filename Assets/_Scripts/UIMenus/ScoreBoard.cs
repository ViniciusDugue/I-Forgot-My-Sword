using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    //Level Timer: 01:23:24
    //Gold Collected: 0
    // Damage Taken: 432
    //Total Mana Used: 87
    //Close Calls(below 15hp): 3
    // Total Points Earned: 
    public TextMeshProUGUI scoreboardText;
    public string levelTimeString;
    public float totalDamageTaken;
    public float totalMagikaUsed;
    public int closeCalls;
    public float finalLevelScore;

    public int pointsPerSecondTimer;
    public int pointsPerDamageTaken;
    public int pointsPerManaUsed;
    public int pointsPerCloseCall;
    public Game_Timer gameTimer;
    public Unit playerStats;
    // public void OnEnable()
    // {
    //     playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
    // }
    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player")!=null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        }
    }
    public void CalculateScoreBoard()
    {
        levelTimeString = gameTimer.GetTime();
        // List<(string, int)> sceneList = new List<(string, int)>() 
        // {("Level_1", 00000),("Level_2", 30000)};
        int maxLevelPoints = 0;
        // string currentSceneName = SceneManager.GetActiveScene().name;
        // foreach ((string sceneName, int maxPoints) in sceneList)
        // {
        //     if (sceneName == currentSceneName)
        //     {
        //         maxLevelPoints = maxPoints;
        //         break;
        //     }
        // }
        string[] timeComponents = levelTimeString.Substring(13).Split(':');
        int minutes = int.Parse(timeComponents[0]);
        int seconds = int.Parse(timeComponents[1]);
        int milliseconds = int.Parse(timeComponents[2]);
        float levelTime = minutes * 60f + seconds + milliseconds / 600f;
        totalDamageTaken = playerStats.totalDamageTaken;
        totalMagikaUsed = playerStats.totalMagikaUsed;
        closeCalls = playerStats.closeCalls;
        finalLevelScore = Mathf.Floor(maxLevelPoints + (levelTime * pointsPerSecondTimer ) + (totalDamageTaken * pointsPerDamageTaken) + (totalMagikaUsed * pointsPerManaUsed) + (closeCalls * pointsPerCloseCall));
    }
    public void DisplayScoreBoard()
    {
        CalculateScoreBoard();
        string finalTime = levelTimeString + "\n";
        string goldCollected = playerStats.goldCollected.ToString() + "\n";
        string damageTaken = totalDamageTaken.ToString() + "\n";
        string manaUsed = totalMagikaUsed.ToString() + "\n";
        string closeCallsString = closeCalls.ToString() + "\n";
        string totalScore = finalLevelScore.ToString() + "\n";
        scoreboardText.text = finalTime + goldCollected + damageTaken + manaUsed + closeCallsString + totalScore;
    }
}
