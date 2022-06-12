using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController_T : MonoBehaviour
{
    // �ϳ��� ��Ʈ�� ���� ������ ��� ��Ʈ(Note) Ŭ���� ����
    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; }
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }


    public GameObject[] Notes;  //��Ʈ�� ����ִ� �迭

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();    //��Ʈ�� ��� �ִ� ����Ʈ
    private float x, z, startY = 8.0f;  //��Ʈ�� ������ ���� ���̰� 
    private float beatInterval = 1.0f;

    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        //������ ����  �������� ��Ʈ�� ��ġ���� �̵�
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NoteBehavior>().Initialize();// ������ �ٽ� NONE���� �ʱ�ȭ
        obj.SetActive(true);
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        //1�ʿ� �ѹ��� ��Ʈ�� ����
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(order * beatInterval);
        MakeNote(note);
    }
    
    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // noteObjectPooler �ʱ�ȭ
        notes.Add(new Note(1, 1));
        notes.Add(new Note(2, 2));
        notes.Add(new Note(3, 3));
        notes.Add(new Note(4, 4));
        notes.Add(new Note(1, 5));
        notes.Add(new Note(2, 6));
        notes.Add(new Note(3, 7));
        notes.Add(new Note(4, 8));
        //��� ��Ʈ�� ������ �ð��� ����ϵ��� ����
        for(int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
