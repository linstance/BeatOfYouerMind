using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelectManager : MonoBehaviour
{

    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 3;
    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        //리소스에서 비트 텍스트 파일을 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        // 첫 번째 줄에 적힌 곡 이름을 읽어서 UI를 업데이트.
        musicTitleUI.text = stringReader.ReadLine();
        //두번째줄은 읽고서 무시
        stringReader.ReadLine();
        //세번째 줄에 적힌 BPM을 읽어 UI 업데이트
        bpmUI.text = "BPM:" + stringReader.ReadLine().Split(' ')[0];
        //리소스에서 Beat음악 파일을 불러와 재생
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        //리소스에서 비트 이미지 파일을 불러오기
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());
    }
    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        {
            UpdateSong(musicIndex);
        }

    }
    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < musicCount) musicIndex = 1;
        {
            UpdateSong(musicIndex);
        }
    }

    public void GameStart()
    {
        PlayerInformation.SelectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("PlayScene");
    }

    private void BackSeane()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void BackBtn()
    {
        Invoke("BackSeane", 0.9f);
    }

    void Start()
    {
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
