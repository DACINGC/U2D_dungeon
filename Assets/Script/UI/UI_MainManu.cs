using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainManu : MonoBehaviour
{
    [SerializeField] private GameObject newGameBtn;
    [SerializeField] private UI_FadeSceen fadeSceeen;
    private void Start()
    {
        fadeSceeen.gameObject.SetActive(true);
        fadeSceeen.Fade_In();
        if (SaveManager.instance.HaveSaveData() == false)
            newGameBtn.SetActive(false);
    }


    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Debug.Log("ÍË³öÓÎÏ·");
        Application.Quit();
    }

    IEnumerator LoadSceneWhithFade(float _deley)
    {
        fadeSceeen.Fade_Out();
        yield return new WaitForSeconds(_deley);
        SceneManager.LoadScene("MainScene");
    }
}