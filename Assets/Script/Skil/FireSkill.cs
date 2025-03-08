using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSkill : Skill
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float burstTime;
    [SerializeField] private float desappearTime;

    [Header("Ä§·¨×Óµ¯")]
    public bool fireLocked;
    [SerializeField] UI_SkillSlot fireLockedButton;
    public override void Start()
    {
        base.Start();
        fireLockedButton.GetComponent<Button>().onClick.AddListener(UnLockedFire);
    }
    protected override void UnLockedSkill()
    {
        base.UnLockedSkill();
        UnLockedFire();
    }
    private void UnLockedFire()
    {
        if (fireLockedButton.unlocked)
            fireLocked = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);
        if (!canUse)
            return;

        Player player = PlayerManager.instance.player;

        GameObject newFire = Instantiate(firePrefab, player.transform.position + new Vector3(1.5f * player.facingDir, 0), Quaternion.identity);
        FireControlloer fire = newFire.GetComponent<FireControlloer>();
        fire.SetUpFire(moveSpeed, desappearTime, burstTime, player.facingDir);
    }
}
