using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,ISaveManager
{
    private UI ui;
    [Header("技能信息")]
    public string skillName;
    [TextArea]
    public string skillDescription;
    public bool unlocked;//解锁的标致符

    [Header("价格")]
    public int price;

    [Header("需要解锁的前置技能")]
    [SerializeField] private UI_SkillSlot[] shoundBeLocked;//解锁该技能前，应该被解锁的技能组
    [SerializeField] private UI_SkillSlot[] shouldBeUnLocked;//解锁该技能前，应该被锁定的技能组(技能只能选一个)
    private Image skillImage;
    private void OnValidate()
    {
        transform.name = "Skill Name: " + skillName;

    }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());

    }
    private void Start()
    {
        ui = GetComponentInParent<UI>();
        skillImage = GetComponent<Image>();
        skillImage.color = Color.red;

        if (unlocked)
            skillImage.color = Color.white;
    }
    //解锁技能
    public void UnlockSkillSlot()
    {
        if (!PlayerManager.instance.HanveEnoughCash(price))
            return;

        for (int i = 0; i < shoundBeLocked.Length; i++)
        {
            //检查是否还有前置技能未解锁
            if (shoundBeLocked[i].unlocked == false)
            {
                Debug.Log("还有技能未解锁");
                return;
            }
        }

        for (int i = 0; i < shouldBeUnLocked.Length; i++)
        { 
            if (shouldBeUnLocked[i].unlocked == true)
            {
                Debug.Log("同级技能已有解锁");
                return;
            }
        }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowSkillToolTip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideSkillToolTip();
    }

    public void LoadData(GameData _data)
    {
        //通过指定键，获取值
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        //字典检索的方法，返回布尔值
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
        {
            _data.skillTree.Add(skillName, unlocked);
        }
    }
}
