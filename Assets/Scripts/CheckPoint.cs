using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        GameManager.instance.CK.transform.position = GameObject.Find("Player").transform.position;
    }

    public void ChangeCKPoint()
    {
        GameManager.instance.CK.transform.position = GameObject.Find("Player").transform.position;
    }

    public void ToCheckPoint()
    {
        GameManager.instance.player.transform.position = GameObject.Find("CKPoint").transform.position;
    }
}
