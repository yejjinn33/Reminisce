using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();
    public List<GameObject> missNoteList = new List<GameObject>();

    public Transform Center = null;

    [SerializeField] private float perfectRange = 1.0f;
    [SerializeField] private float coolRange = 5.0f;
    [SerializeField] private float goodRange = 15.0f;
    public float badRange = 30.0f;

    //public int score = 0;
    //public int perfectScore = 50;
    //public int coolScore = 25;
    //public int goodScore = 15;
    //public int badScore = -5;
    //public int missScore = -20;

    public string isgame = "";
    public string isnotice = "";
    //public int perfectCount = 0;
    //public int coolCount = 0;
    //public int goodCount = 0;
    //public int badCount = 0;
    //public int missCount = 0;

    //[SerializeField] private RectTransform perfectRect; // Inspector�� ǥ��
    //[SerializeField] private RectTransform coolRect;    // Inspector�� ǥ��
    //[SerializeField] private RectTransform goodRect;    // Inspector�� ǥ��
    //[SerializeField] private RectTransform badRect;

    //[SerializeField] private TMP_Text scoreText;
    [SerializeField] private EffectManager effectManager;
    public bool isMusicStarted = false; // �뷡�� ����Ǿ����� Ȯ��

    EffectManager theEffect;
    private ScoreManager scoreManager;
    private RhythmGameUI rhythmGameUI;

    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        Debug.Log("TimingManager Initialized");
        //UpdateTimingRectVisuals();
        rhythmGameUI = FindObjectOfType<RhythmGameUI>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    //void updatetimingrectvisuals()
    //{
    //    if (perfectrect != null)
    //        perfectrect.sizedelta = new vector2(perfectrange * 2, perfectrect.sizedelta.y);

    //    if (coolrect != null)
    //        coolrect.sizedelta = new vector2(coolrange * 2, coolrect.sizedelta.y);

    //    if (goodrect != null)
    //        goodrect.sizedelta = new vector2(goodrange * 2, goodrect.sizedelta.y);

    //    if (badrect != null)
    //        badrect.sizedelta = new vector2(badrange * 2, badrect.sizedelta.y);

    //    debug.log("timing rect visuals updated");
    //}
    void Update()
    {
        if (GameTimerIsOver())
        {
            Debug.Log("end");
            EndGame(); // ���� ���� üũ
        }
    }



    private bool GameTimerIsOver()
    {
        return TimeUI.timer <= 0; // Ÿ�̸Ӱ� 0�̸� ���� ����
    }

    public void EndGame()
    {
        int finalScore = scoreManager.Score;
        if (finalScore >= 1500)
        {
            Debug.Log("sucess");
            isgame = "SUCCESS!!";
            isnotice = "R��ư�� ���� �������ּ���";
        }
        else
        {
            Debug.Log("fail");
            isgame = "GAME OVER";
            isnotice = "R��ư�� ���� �ٽ� �������ּ���";
        }
        rhythmGameUI.ActivateEndRhythmUI(this, scoreManager);
    }


    public void CheckTiming()
    {
        if (boxNoteList.Count == 0)
        {
            Debug.Log("Miss: No Notes");
            MissNote(null); // MissNote�� ȣ���Ͽ� ó��
            return;
        }

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float distance = Mathf.Abs(boxNoteList[i].transform.position.x - Center.position.x);

            if (distance <= perfectRange)
            {
                //perfectCount++;
                //score += perfectScore;
                scoreManager.AddPerfect();
                effectManager.NoteHitEffect();
                effectManager.JudgementEffect(0);
                ProcessNoteAt(i);
                return;
            }
            else if (distance <= coolRange)
            {
                //coolCount++;
                //score += coolScore;
                scoreManager.AddCool();
                effectManager.NoteHitEffect();
                effectManager.JudgementEffect(1);
                ProcessNoteAt(i);
                return;
            }
            else if (distance <= goodRange)
            {
                //goodCount++;
                //score += goodScore;
                scoreManager.AddGood();
                effectManager.NoteHitEffect();
                effectManager.JudgementEffect(2);
                ProcessNoteAt(i);
                return;
            }
            else if (distance <= badRange)
            {
                //badCount++;
                //score += badScore;
                scoreManager.AddBad();
                effectManager.JudgementEffect(3);
                ProcessNoteAt(i);
                return;
            }
        }

        Debug.Log("Miss");
        MissNote(null); // MissNote�� ȣ���Ͽ� ó��
    }

    public void MissNote(GameObject missedNote)
    {
        //missCount++;
        //score += missScore;
        //if (score < 0) score = 0;
        scoreManager.AddMiss();

        if (missedNote != null)
        {
            missNoteList.Add(missedNote);
            boxNoteList.Remove(missedNote);

            // 2�� �Ŀ� Miss�� ��Ʈ�� ����
            StartCoroutine(DeleteMissedNoteAfterDelay(missedNote, 2f));
        }

        effectManager.JudgementEffect(4);
        //UpdateScoreText();
    }

    IEnumerator DeleteMissedNoteAfterDelay(GameObject note, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (missNoteList.Contains(note))
        {
            missNoteList.Remove(note);
            Destroy(note);
        }
    }

    private void ProcessNoteAt(int index)
    {
        GameObject note = boxNoteList[index];

        if (index == 0 && !isMusicStarted)
        {
            note.GetComponent<Note>().HideNote();
            StartMusic();
        }
        else
        {
            boxNoteList.RemoveAt(index);
            Destroy(note);
        }

        //UpdateScoreText();
    }

    public void RemoveMissedNote(GameObject note)
    {
        if (missNoteList.Contains(note))
        {
            missNoteList.Remove(note); // ����Ʈ���� ����
            Destroy(note); // GameObject ����
        }
    }


    public void StartMusic()
    {
        if (!isMusicStarted)
        {
            Debug.Log("Music Started");
            isMusicStarted = true;
            // AudioSource.Play(); // �뷡 ���
        }
    }

    //void UpdateScoreText()
    //{
    //    scoreText.text = $"Perfect: {perfectCount}\nCool: {coolCount}\nGood: {goodCount}\nBad: {badCount}\nMiss: {missCount}\nScore: {score}";
    //}


}
