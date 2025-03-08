using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStat playreStat;
    [SerializeField] private Slider healthySlider;
    [SerializeField] private Slider blueSlder;
    [SerializeField] private Text cashText;
    [Header("技能图片")]
    [SerializeField] private Image dashImage;
    [SerializeField] private Image fireImge;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image FlaskImage;

    private float dashCooldown;
    private float fireCooldown;
    private float blackholeCooldown;

    [Header("钱的增长")]
    [SerializeField] float cashAmount;
    [SerializeField] private int increaseSpeed;

    private void OnEnable()
    {
        playreStat.OnHealthChanged += UpdateHealthUI;
    }
    private void Start()
    {
        dashCooldown = SkillManager.instance.dash.cooldown;
        fireCooldown = SkillManager.instance.fire.cooldown;
        blackholeCooldown = SkillManager.instance.blackhole.cooldown;
        
        dashImage.fillAmount = 0;
        fireImge.fillAmount = 0;
        blackholeImage.fillAmount = 0;
        FlaskImage.fillAmount = 0;
    }
    private void Update()
    {
        if (cashAmount < PlayerManager.instance.currentCash)
            cashAmount += Time.deltaTime * increaseSpeed;
        else
            cashAmount = PlayerManager.instance.currentCash;

        cashText.text = ((int)cashAmount).ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.dashLocked)
            SetCooldownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.F) && SkillManager.instance.fire.fireLocked)
            SetCooldownOf(fireImge);

        if (Input.GetKeyDown(KeyCode.R) && SkillManager.instance.blackhole.blackholeLocked)
            SetCooldownOf(blackholeImage);

        Item_Equipment Flask = Inventory.instance.GetEquipmentType(EquipmentType.Flask);

        if (Flask != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetCooldownOf(FlaskImage);
        }

        CheckCooldownOf(dashImage, dashCooldown);
        CheckCooldownOf(fireImge, fireCooldown);
        CheckCooldownOf(blackholeImage, blackholeCooldown);

        if (Flask != null)
        {
            CheckCooldownOf(FlaskImage, Flask.cooldown);
        }


    }
    private void UpdateHealthUI()
    {
        Debug.Log("MAxH:" + playreStat.maxHp.GetValue() + "Cur:" + playreStat.currentHp);
        healthySlider.maxValue = playreStat.maxHp.GetValue();
        healthySlider.value = playreStat.currentHp;
    }

    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;


    }

    public void UpdateBlueUI()
    {
        blueSlder.maxValue = playreStat.GetValueOFBlueBar();
        blueSlder.value = playreStat.currentBlueBbar;
    }
}
