using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public void GameLoaded()
    {
        GameManager.instance.SaveState();
    }
}
