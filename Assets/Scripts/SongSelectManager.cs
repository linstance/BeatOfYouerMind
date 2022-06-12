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
        //���ҽ����� ��Ʈ �ؽ�Ʈ ������ �ҷ�����
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        // ù ��° �ٿ� ���� �� �̸��� �о UI�� ������Ʈ.
        musicTitleUI.text = stringReader.ReadLine();
        //�ι�°���� �а� ����
        stringReader.ReadLine();
        //����° �ٿ� ���� BPM�� �о� UI ������Ʈ
        bpmUI.text = "BPM:" + stringReader.ReadLine().Split(' ')[0];
        //���ҽ����� Beat���� ������ �ҷ��� ���
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        //���ҽ����� ��Ʈ �̹��� ������ �ҷ�����
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
