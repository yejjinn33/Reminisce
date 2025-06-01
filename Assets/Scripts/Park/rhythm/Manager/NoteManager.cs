using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [Header("��Ʈ ���� ���� ����")]
    public float minNoteDelay = 0.2f; // �ּ� ���� (��)
    public float maxNoteDelay = 0.8f; // �ִ� ���� (��)

    double currentTime = 0d;
    double nextNoteTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject goNote = null;
    public Transform Center = null;

    TimingManager theTimingManager;
    private bool isRhythmGameStarted = false;

    void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= nextNoteTime)
        {
            // Note ����
            SpawnNote();

            // ���� ��Ʈ ���� ���� ����
            currentTime -= nextNoteTime;
            SetNextNoteTime();
        }

        RemoveMissedNotes();
    }

    // ���� ���� ���� �޼���
    public void StartRhythmGame()
    {
        isRhythmGameStarted = true;
        Debug.Log("���� ���� ����!");
        SpawnNote();  // ù ��° ��Ʈ ���� Ÿ�̹� ����
    }

    void SpawnNote()
    {
        GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);

        t_note.transform.SetParent(this.transform);
        t_note.transform.localPosition = tfNoteAppear.localPosition;
        t_note.transform.localScale = Vector3.one;

        Debug.Log($"Note Spawned: {t_note.name}, Position: {t_note.transform.position}");

        theTimingManager.boxNoteList.Add(t_note);
    }

    void SetNextNoteTime()
    {
        // �ּ�-�ִ� ���� ���̿��� ���� ����
        nextNoteTime = Random.Range(minNoteDelay, maxNoteDelay);
        Debug.Log($"Next Note Time Set: {nextNoteTime} seconds");
    }

    void RemoveMissedNotes()
    {
        for (int i = theTimingManager.boxNoteList.Count - 1; i >= 0; i--)
        {
            GameObject note = theTimingManager.boxNoteList[i];

            // TimingManager���� badRange�� ������ ���
            if (note.transform.position.x > theTimingManager.Center.position.x + theTimingManager.badRange)
            {
                Debug.Log("Missed Note Added");
                theTimingManager.MissNote(note);
                StartCoroutine(DeleteMissedNoteAfterDelay(note, 3.5f));
            }
        }
    }

    IEnumerator DeleteMissedNoteAfterDelay(GameObject note, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (theTimingManager.missNoteList.Contains(note))
        {
            theTimingManager.RemoveMissedNote(note);
        }
    }
}