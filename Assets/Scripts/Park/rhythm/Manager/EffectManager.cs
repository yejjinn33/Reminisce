using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;


    public void JudgementEffect(int p_num)
    {
        if (judgementAnimator.runtimeAnimatorController == null)
        {
            Debug.LogError("AnimatorController is missing on judgementAnimator");
            return;
        }

        // p_num ���� ��ȿ���� Ȯ�� (Miss ����)
        if (p_num < 0 || p_num >= judgementSprite.Length)
        {
            Debug.LogError($"Invalid p_num: {p_num}. judgementSprite �迭�� ������ ������ϴ�.");
            return;
        }

        // ��������Ʈ ���� �� �ִϸ��̼� Ʈ����
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }
    public void NoteHitEffect()
    {
        noteHitAnimator.gameObject.SetActive(true);
        noteHitAnimator.SetTrigger(hit);
    }

    // Start is called before the first frame update
    void Start()
    {
        noteHitAnimator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
