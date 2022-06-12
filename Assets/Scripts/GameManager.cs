using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱������� �ۼ�

    public static GameManager instance { get; set; }
    public void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;


    public GameObject scoreUI;
    public float score;
    private Text scoreText;

    public GameObject comboUI;
    private int combo;
    private Text comboText;
    private Animator comboAnimator;
    public int maxCombo;

    public enum judges { NONE = 0, BAD,  GOOD, PERFECT , MISS }; //BAD : 1, GOOD : 2, PERFECT : 3, MiSS : 4
    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    public GameObject[] trails; // Ű���� ����Ʈ ȿ���� ����ִ� �迭
    private SpriteRenderer[] trailSpriteRenderers; //Ű���� ����Ʈ ��������Ʈ �������� ��� �ִ� �迭

    //���Ǻ���
    private AudioSource audioSource;
 

    //�ڵ� ��尡 Ȱ��ȭ �������� �Ǵ��ϴ� ����
    public bool autoPerfact;

    //������ �����ϴ� �Լ�
    void MusicStart()
    {
        //Resources���� ��Ʈ(Beat) ���� ������ �ҷ��� ����մϴ�.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.SelectedMusic);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Start()
    {
        Invoke("MusicStart", 2);
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        //��������� �����ִ� �̹��� �ʱ�ȭ
        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");


        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for(int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //����ڰ� �Է��� Ű�� �ش��ϴ� ������ ������ ó��
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);

        //�ѹ� ������ �� ������ �ݺ������� ��Ӱ� ó��
        for(int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -=0.01f;
            trailSpriteRenderers[i].color = color;
        }
    }

    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    //��Ʈ �������� ��������� ȭ�鿡 ���
    void showJudgement()
    {
        //���� �̹��� ���
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);

        //���� �̹����� ���
        judgementSpriteAnimator.SetTrigger("Show");
        // �޺��� 0�̻��϶� �޺� �̹����� ���
        if(combo >= 0)
        {
            comboText.text = combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
        if(maxCombo < combo)
        {
            maxCombo = combo;
        }
    }

    //��Ʈ ������ �����ϴ� �Լ�
    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;

        //miss �����϶� �޺��� �����ϰ� ������ ���� ��´�
          if(judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }
        //Bad �����϶� �޺��� ������Ű�� ������ ���� ��´�
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo += 1;
            if (score >= 5) score -= 5;
        }
          //PERFECT Ȥ�� GOOD�����϶� �޺� �� ������ ��½�Ų��
        else
        {
            if(judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 20;
            }
            else if (judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        showJudgement();
    }
}
