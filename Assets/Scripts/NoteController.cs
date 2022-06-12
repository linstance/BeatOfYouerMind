using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteController : MonoBehaviour
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


    //��Ȱ��ȭ Bar
    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;


    //Ȱ��ȭBar
    public GameObject bar1D;
    public GameObject bar2D;
    public GameObject bar3D;
    public GameObject bar4D;


    public GameObject[] Notes;  //��Ʈ�� ����ִ� �迭

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();    //��Ʈ�� ��� �ִ� ����Ʈ
    private float x, z, startY = 8.0f;  //��Ʈ�� ������ ���� ���̰� 
    

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

    private string musicTitle;  //���� ����
    private string musicArtist; //�۰
    private int bpm; //BPM
    private int divider; 
    private float startingPoint;
    private float beatCount; //1�ʸ��� �������� ��Ʈ�� ����
    private float beatInterval;  //��Ʈ�� �������� �ð� ����

    IEnumerator AwaitMakeNote(Note note)
    {
        //1�ʿ� �ѹ��� ��Ʈ�� ����
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(note);
    }

    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // noteObjectPooler �ʱ�ȭ
        //Resources���� Beat �ؽ�Ʈ ������ �ҷ�����.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.SelectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //ù��° �� ���̸� �о� ����
        musicTitle = reader.ReadLine();
        //�ι�° �� ��Ƽ��Ʈ �̸� �о� ����
        musicArtist = reader.ReadLine();
        //����° �ٿ� ���� ��Ʈ ����(BPM, Divider, ���۽ð�)�о����
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint =(float) Convert.ToDouble(beatInformation.Split(' ')[2]);
        //1�ʸ��� �������� ��Ʈ
        beatCount = (float)bpm / divider;
        //��Ʈ�� �������� ���ݽð� ���
        beatInterval = 1 / beatCount;
        //�� ��Ʈ���� �������� ��ġ �� �ð��� ������ �о����
        String line;
        while ((line = reader.ReadLine()) != null)
        {
            Note note = new Note(
                    Convert.ToInt32(line.Split(' ')[0]) + 1,
                    Convert.ToInt32(line.Split(' ')[1])
                ) ;
            notes.Add(note);
        }
        //��� ��Ʈ�� ������ �ð��� ����ϵ��� ����
        for (int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        // ������ ��Ʈ�� �������� ���� ���� �Լ��� �ҷ��ɴϴ�.
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
