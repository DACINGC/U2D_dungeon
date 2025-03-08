using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
    public CheckPoint[] checkPoints;
    public string LastPastCheckPointId;
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(instance);
        }

        instance = this;
        checkPoints = FindObjectsOfType<CheckPoint>();
    }
    private void Start()
    {
        
    }
    public void ResetScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint.iD == pair.Key)
                {
                    checkPoint.isActive = pair.Value;

                    if (checkPoint.isActive)
                        checkPoint.ActiveCheckPont();
                }
            }
        }
        LastPastCheckPointId = _data.LastCheckPointId;
        PlacePlayerOnLastCheckPoint();
    }

    private void PlacePlayerOnLastCheckPoint()
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint.iD == LastPastCheckPointId)
                PlayerManager.instance.player.transform.position = new Vector3(checkPoint.transform.position.x, checkPoint.transform.position.y + 1.4f);
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.checkPoints.Clear();

        foreach (var checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.iD, checkPoint.isActive);
        }

        _data.LastCheckPointId = LastPastCheckPointId;
        //if(FindClosestCheckPoint())
        //    _data.LastCheckPointId = FindClosestCheckPoint().iD;

    }
    //找到离玩家最近的传送点
    public CheckPoint FindClosestCheckPoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckPoint = null;

        foreach (var checkPoint in checkPoints)
        {
            float distanceToCheckPoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkPoint.transform.position);

            if (closestDistance > distanceToCheckPoint && checkPoint.isActive == true)
            { 
                closestDistance = distanceToCheckPoint;
                closestCheckPoint = checkPoint;
            }
        }

        return closestCheckPoint;
    }
    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
