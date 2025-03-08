using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class EntityFX : MonoBehaviour
{
    [Header("¹¥»÷ÉÁ°×")]
    private Material orMaterial;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float hitDuration;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject criticalHitPrefab;
    [Header("³å´Ì²ÐÓ°")]
    [SerializeField] private GameObject dashClone;
    [SerializeField] private float dashCreateRate;
    [SerializeField] private float colorLoosingRate;
    [Header("ÆÁÄ»¶¶¶¯")]
    [SerializeField] private float shakeMutiply;
    public Vector3 damageShake;
    public Vector3 heavyDamageShake;
    private CinemachineImpulseSource screenShake;
    [Header("µ¯³öÎÄ±¾")]
    [SerializeField] private GameObject popTextPrefab;
    private GameObject myHealthyBar;
    private Player player;
    private float dashCloneDuration;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        screenShake = GetComponent<CinemachineImpulseSource>();
        myHealthyBar = GetComponentInChildren<UI_HealthyBar>().gameObject;
    }
    private void Start()
    {
        orMaterial = sr.material;
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        dashCloneDuration -= Time.deltaTime;
    }


    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            myHealthyBar.gameObject.SetActive(false);
            //sr.color = Color.clear;
        }
        else
        { 
            myHealthyBar.gameObject.SetActive(true);
            //sr.color = Color.white;
        }
    }
    public void PopText(string _text, Transform _target)
    {
        GameObject newText = Instantiate(popTextPrefab, _target.transform.position, Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = _text;
    }

    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y);
        screenShake.GenerateImpulse();
        
    }
    public void CreateDashClone()
    {
        if (dashCloneDuration < 0)
        {
            dashCloneDuration = dashCreateRate;
            GameObject newDash = Instantiate(dashClone, new Vector3(transform.position.x, transform.position.y - 1.55f), transform.rotation);

            newDash.GetComponent<CreateDashClone>().SetUpDashClone(colorLoosingRate, sr.sprite);
        }
    }
    public IEnumerator HitFx()
    {
        sr.material = hitMaterial;
        yield return new WaitForSeconds(hitDuration);
        sr.material = orMaterial;
    }

    public void CreateHitAnim(Transform _target, bool critical)
    {
        GameObject newHitPrefab = null;
        newHitPrefab = hitPrefab;
        if (critical)
        {
            newHitPrefab = criticalHitPrefab;
        }
        GameObject newHit = Instantiate(newHitPrefab, _target.position, Quaternion.identity);

        if (GetComponent<Entity>().facingDir == -1)
            newHit.transform.Rotate(0, 180, 0);
            

        Destroy(newHit, 0.5f);
    }

}
