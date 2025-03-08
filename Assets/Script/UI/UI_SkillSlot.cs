using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,ISaveManager
{
    private UI ui;
    [Header("������Ϣ")]
    public string skillName;
    [TextArea]
    public string skillDescription;
    public bool unlocked;//�����ı��·�

    [Header("�۸�")]
    public int price;

    [Header("��Ҫ������ǰ�ü���")]
    [SerializeField] private UI_SkillSlot[] shoundBeLocked;//�����ü���ǰ��Ӧ�ñ������ļ�����
    [SerializeField] private UI_SkillSlot[] shouldBeUnLocked;//�����ü���ǰ��Ӧ�ñ������ļ�����(����ֻ��ѡһ��)
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
    //��������
    public void UnlockSkillSlot()
    {
        if (!PlayerManager.instance.HanveEnoughCash(price))
            return;

        for (int i = 0; i < shoundBeLocked.Length; i++)
        {
            //����Ƿ���ǰ�ü���δ����
            if (shoundBeLocked[i].unlocked == false)
            {
                Debug.Log("���м���δ����");
                return;
            }
        }

        for (int i = 0; i < shouldBeUnLocked.Length; i++)
        { 
            if (shouldBeUnLocked[i].unlocked == true)
            {
                Debug.Log("ͬ���������н���");
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
        //ͨ��ָ��������ȡֵ
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        //�ֵ�����ķ��������ز���ֵ
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
