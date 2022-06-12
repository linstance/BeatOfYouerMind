using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤으로 작성

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

    public GameObject[] trails; // 키보드 이팩트 효과를 담고있는 배열
    private SpriteRenderer[] trailSpriteRenderers; //키보드 이팩트 스프라이트 랜더러를 담고 있는 배열

    //음악변수
    private AudioSource audioSource;
 

    //자동 모드가 활성화 상태인지 판단하는 변수
    public bool autoPerfact;

    //음악을 실행하는 함수
    void MusicStart()
    {
        //Resources에서 비트(Beat) 음악 파일을 불러와 재생합니다.
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

        //판정결과를 보여주는 이미지 초기화
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
        //사용자가 입력한 키의 해당하는 라인을 빛나게 처리
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);

        //한번 빛나게 된 라인을 반복적으로 어둡게 처리
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

    //노트 판정이후 판정결과를 화면에 출력
    void showJudgement()
    {
        //점수 이미지 출력
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);

        //판정 이미지를 출력
        judgementSpriteAnimator.SetTrigger("Show");
        // 콤보가 0이상일때 콤보 이미지를 출력
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

    //노트 판정을 진행하는 함수
    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;

        //miss 판정일때 콤보를 종료하고 점수를 많이 깍는다
          if(judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }
        //Bad 판정일때 콤보를 증가시키고 점수를 조금 깍는다
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo += 1;
            if (score >= 5) score -= 5;
        }
          //PERFECT 혹은 GOOD판정일때 콤보 및 점수를 상승시킨다
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
