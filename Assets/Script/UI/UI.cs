using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("UI界面")]
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillStreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private UI_FadeSceen fadeScreen;
    [SerializeField] private GameObject deadText;
    [SerializeField] private GameObject endBtn;
    public UI_CraftWindow craftWindow;
    [Header("弹窗口")]
    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;
    [Header("音量控制")]
    public UI_VolumeSlider[] volumeSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        SwitchTo(skillStreeUI);
    }
    void Start()
    {
        SwitchTo(null);
        SwitchTo(inGameUI);
        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        skillToolTip.gameObject.SetActive(false);
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.Fade_In();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            SwithKeyTo(characterUI);
        if (Input.GetKeyDown(KeyCode.B))
            SwithKeyTo(skillStreeUI);
        if (Input.GetKeyDown(KeyCode.K))
            SwithKeyTo(craftUI);
        if (Input.GetKeyDown(KeyCode.O))
            SwithKeyTo(optionUI);

    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScrren = transform.GetChild(i).GetComponent<UI_FadeSceen>() != null;

            if (fadeScrren == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        { 
            _menu.SetActive(true);
            AudioManager.instance.PlaySFX(81, null, false);
        }

        if (inGameUI.activeSelf)
            GameManager.instance.PauseGame(false);
        else if (!inGameUI.activeSelf)
            GameManager.instance.PauseGame(true);
    }

    public void SwithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    public void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeSceen>() == null)
                return;
        }

        SwitchTo(inGameUI);
    }

    public void SwitchOnEndSceen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.Fade_Out();
        StartCoroutine(nameof(DeadTextCoroutine));
    }
    IEnumerator DeadTextCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        deadText.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        endBtn.SetActive(true);

    }

    public void ResetScene()
    {
        GameManager.instance.ResetScene();

    }
    public void ReturnMainManu()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene(0);
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSlider)
        {
            foreach (UI_VolumeSlider slider in volumeSlider)
            {
                if (slider.paramter == pair.Key)
                {
                    slider.LoadeSliderValue(pair.Value);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSlider.Clear();

        foreach (UI_VolumeSlider slider in volumeSlider)
        {
            _data.volumeSlider.Add(slider.paramter, slider.GetComponent<Slider>().value);
        }
    }
    public void CloseBtn()
    {
        Time.timeScale = 1;
    }
}
