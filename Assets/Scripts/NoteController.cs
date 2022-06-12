using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteController : MonoBehaviour
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


    //비활성화 Bar
    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;


    //활성화Bar
    public GameObject bar1D;
    public GameObject bar2D;
    public GameObject bar3D;
    public GameObject bar4D;


    public GameObject[] Notes;  //노트를 담고있는 배열

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();    //노트를 담고 있는 리스트
    private float x, z, startY = 8.0f;  //노트가 생성될 고정 높이값 
    

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

    private string musicTitle;  //음악 제목
    private string musicArtist; //작곡가
    private int bpm; //BPM
    private int divider; 
    private float startingPoint;
    private float beatCount; //1초마다 떨어지는 비트의 갯수
    private float beatInterval;  //비트가 떨어지는 시간 간격

    IEnumerator AwaitMakeNote(Note note)
    {
        //1초에 한번씩 노트를 생성
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(note);
    }

    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // noteObjectPooler 초기화
        //Resources에서 Beat 텍스트 파일을 불러오기.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.SelectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //첫번째 줄 곡이름 읽어 오기
        musicTitle = reader.ReadLine();
        //두번째 줄 아티스트 이름 읽어 오기
        musicArtist = reader.ReadLine();
        //세번째 줄에 적힌 비트 정보(BPM, Divider, 시작시간)읽어오기
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint =(float) Convert.ToDouble(beatInformation.Split(' ')[2]);
        //1초마다 떨어지는 비트
        beatCount = (float)bpm / divider;
        //비트가 떨어지는 간격시간 계산
        beatInterval = 1 / beatCount;
        //각 비트들이 떨어지는 위치 및 시간을 정보로 읽어오기
        String line;
        while ((line = reader.ReadLine()) != null)
        {
            Note note = new Note(
                    Convert.ToInt32(line.Split(' ')[0]) + 1,
                    Convert.ToInt32(line.Split(' ')[1])
                ) ;
            notes.Add(note);
        }
        //모든 노트를 정해진 시간에 출발하도록 설정
        for (int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        // 마지막 노트를 기준으로 게임 종료 함수를 불러옵니다.
        StartCoroutine(AwaitGameResult(notes[notes.Count - 1].order));
    }

    IEnumerator AwaitGameResult(int order)
    {
        yield return new WaitForSeconds(startingPoint + order * beatInterval + 8.0f);
        GameResult();
    }

    void GameResult()
    {
        PlayerInformation.maxCombo = GameManager.instance.maxCombo;
        PlayerInformation.score = GameManager.instance.score;
        PlayerInformation.musicTitle = musicTitle;
        PlayerInformation.musicArtist = musicArtist;
        SceneManager.LoadScene("GameResultScene");
    }

    // Update is called once per frame
    void Update()
    {
        Bar();
    }


    private void Bar()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            bar1.SetActive(false);
            bar1D.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            bar1.SetActive(true);
            bar1D.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            bar2.SetActive(false);
            bar2D.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            bar2.SetActive(true);
            bar2D.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            bar3.SetActive(false);
            bar3D.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            bar3.SetActive(true);
            bar3D.SetActive(false);
        }

            if (Input.GetKeyDown(KeyCode.K))
            {
                bar4.SetActive(false);
                bar4D.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                bar4.SetActive(true);
                bar4D.SetActive(false);
            }


    }
}
