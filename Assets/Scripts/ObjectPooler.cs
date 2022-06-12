using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Start is called before the first frame update

    //2�� LIst
    public List<GameObject> Notes;
    private List<List<GameObject>> poolsOfNotes;
    public int noteCount = 10;
    private bool more = true;

    void Start()
    {
        poolsOfNotes = new List<List<GameObject>>();
        for(int i  = 0; i < Notes.Count; i++)   // ��Ʈ�� ������ ����Ʈ ��ŭ �ݺ�
        {
            poolsOfNotes.Add(new List<GameObject>());
            for(int n = 0; n < noteCount; n++)  // 10�� �ݺ�
            {
                GameObject obj = Instantiate(Notes[i]);
                obj.SetActive(false);
                poolsOfNotes[i].Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getObject(int noteType)
    {
        foreach(GameObject obj  in poolsOfNotes[noteType -1])   //��Ʈ ������ �´� �ش� ����Ʈ�� �����ؼ� ��� ������Ʈ äũ
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        if(more)
        {
            GameObject obj = Instantiate(Notes[noteType - 1]);
            poolsOfNotes[noteType - 1].Add(obj);
            return obj;
        }
        return null;
    }

}
