using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController_T : MonoBehaviour
{
    // 하나의 노트에 대한 정보를 담는 노트(Note) 클래스 정의
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


    public GameObject[] Notes;  //노트를 담고있는 배열

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();    //노트를 담고 있는 리스트
    private float x, z, startY = 8.0f;  //노트가 생성될 고정 높이값 
    private float beatInterval = 1.0f;

    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        //설정된 시작  라인으로 노트의 위치값을 이동
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NoteBehavior>().Initialize();// 판정을 다시 NONE으로 초기화
        obj.SetActive(true);
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        //1초에 한번씩 노트를 생성
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(order * beatInterval);
        MakeNote(note);
    }
    
    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // noteObjectPooler 초기화
        notes.Add(new Note(1, 1));
        notes.Add(new Note(2, 2));
        notes.Add(new Note(3, 3));
        notes.Add(new Note(4, 4));
        notes.Add(new Note(1, 5));
        notes.Add(new Note(2, 6));
        notes.Add(new Note(3, 7));
        notes.Add(new Note(4, 8));
        //모든 노트를 정해진 시간에 출발하도록 설정
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
