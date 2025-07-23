using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Achivement : MonoBehaviour
{
    public bool Smisi1, Smisi2, Smisi3, Smisi4, Smisi5, Smisi6, Smisi7, Smisi8, Smisi9, Smisi10, Smisi11, Smisi12, Smisi13, Smisi14 = false;
    public int pMisi1, pMisi2, pMisi3, pMisi4, pMisi5, pMisi6, pMisi7, pMisi8, pMisi9, pMisi10, pMisi11, pMisi12;
    public List<GameObject> button;
    public List<Text> buttonT;

    // Refrences
    public List<Text> progresT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Achivement instance;

    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += LoadState;
    }

    // Update is called once per frame
    void Update()
    {
        reloadProgres();
        reload1();
        reload2();
        reload3();
        reload4();
        reload5();
        reload6();
        reload7();
        reload8();
        reload9();
        reload10();
        reload11();
        reload12();
        reload13();
        reload14();
        SaveState();
    }

    // System

    public void reloadProgres()
    {
        progresT[0].text = "10 / " + pMisi1;
        progresT[1].text = "10 / " + pMisi2;
        progresT[2].text = "10 / " + pMisi3;
        progresT[3].text = "10 / " + pMisi4;
        progresT[4].text = "10 / " + pMisi5;
        progresT[5].text = "10 / " + pMisi6;
        progresT[6].text = "10 / " + pMisi7;
        progresT[7].text = "10 / " + pMisi8;
        progresT[8].text = "5 / " + pMisi9;
        progresT[9].text = "5 / " + pMisi10;
        progresT[10].text = "5 / " + pMisi11;
        progresT[11].text = "5 / " + pMisi12;
        progresT[12].text = "50 / " + pMisi1;
        progresT[13].text = "50 / " + pMisi2;
    }

    public void reload1()
    {
        if(Smisi1 == true)
        {
            button[0].SetActive(false);
        }
        else
        {
            if(pMisi1 >= 10)
            {
                buttonT[0].text = "Klaim Now!";
            }
            else
            {
                buttonT[0].text = "On Progres";
            }
        }
    }
    public void reload2()
    {
        if(Smisi2 == true)
        {
            button[1].SetActive(false);
        }
        else
        {
            if(pMisi2 >= 10)
            {
                buttonT[1].text = "Klaim Now!";
            }
            else
            {
                buttonT[1].text = "On Progres";
            }
        }
    }
    public void reload3()
    {
        if(Smisi3 == true)
        {
            button[2].SetActive(false);
        }
        else
        {
            if(pMisi3 >= 10)
            {
                buttonT[2].text = "Klaim Now!";
            }
            else
            {
                buttonT[2].text = "On Progres";
            }
        }
    }
    public void reload4()
    {
        if(Smisi4 == true)
        {
            button[3].SetActive(false);
        }
        else
        {
            if(pMisi4 >= 10)
            {
                buttonT[3].text = "Klaim Now!";
            }
            else
            {
                buttonT[3].text = "On Progres";
            }
        }
    }
    public void reload5()
    {
        if(Smisi5 == true)
        {
            button[4].SetActive(false);
        }
        else
        {
            if(pMisi5 >= 10)
            {
                buttonT[4].text = "Klaim Now!";
            }
            else
            {
                buttonT[4].text = "On Progres";
            }
        }
    }
    public void reload6()
    {
        if(Smisi6 == true)
        {
            button[5].SetActive(false);
        }
        else
        {
            if(pMisi6 >= 10)
            {
                buttonT[5].text = "Klaim Now!";
            }
            else
            {
                buttonT[5].text = "On Progres";
            }
        }
    }
    public void reload7()
    {
        if(Smisi7 == true)
        {
            button[6].SetActive(false);
        }
        else
        {
            if(pMisi7 >= 10)
            {
                buttonT[6].text = "Klaim Now!";
            }
            else
            {
                buttonT[6].text = "On Progres";
            }
        }
    }
    public void reload8()
    {
        if(Smisi8 == true)
        {
            button[7].SetActive(false);
        }
        else
        {
            if(pMisi8 >= 5)
            {
                buttonT[7].text = "Klaim Now!";
            }
            else
            {
                buttonT[7].text = "On Progres";
            }
        }
    }
    public void reload9()
    {
        if(Smisi9 == true)
        {
            button[8].SetActive(false);
        }
        else
        {
            if(pMisi9 >= 5)
            {
                buttonT[8].text = "Klaim Now!";
            }
            else
            {
                buttonT[8].text = "On Progres";
            }
        }
    }
    public void reload10()
    {
        if(Smisi10 == true)
        {
            button[9].SetActive(false);
        }
        else
        {
            if(pMisi10 >= 5)
            {
                buttonT[9].text = "Klaim Now!";
            }
            else
            {
                buttonT[9].text = "On Progres";
            }
        }
    }
    public void reload11()
    {
        if(Smisi11 == true)
        {
            button[10].SetActive(false);
        }
        else
        {
            if(pMisi11 >= 5)
            {
                buttonT[10].text = "Klaim Now!";
            }
            else
            {
                buttonT[10].text = "On Progres";
            }
        }
    }
    public void reload12()
    {
        if(Smisi12 == true)
        {
            button[11].SetActive(false);
        }
        else
        {
            if(pMisi12 >= 1)
            {
                buttonT[11].text = "Klaim Now!";
            }
            else
            {
                buttonT[11].text = "On Progres";
            }
        }
    }
    public void reload13()
    {
        if(Smisi13 == true)
        {
            button[12].SetActive(false);
        }
        else
        {
            if(pMisi1 >= 50)
            {
                buttonT[12].text = "Klaim Now!";
            }
            else
            {
                buttonT[12].text = "On Progres";
            }
        }
    }
    public void reload14()
    {
        if(Smisi14 == true)
        {
            button[13].SetActive(false);
        }
        else
        {
            if(pMisi2 >= 50)
            {
                buttonT[13].text = "Klaim Now!";
            }
            else
            {
                buttonT[13].text = "On Progres";
            }
        }
    }
    
    public void upProgres(string nm)
    {
        Debug.Log(nm);
        if(nm == "skeleton")
        {
            pMisi1+=1;
        }else if(nm == "hightBone")
        {
            pMisi2 += 1;
        }else if(nm == "coruptedBone")
        {
            pMisi3++;
        }else if(nm == "orgeWarior")
        {
            pMisi4 += 1;
        }else if(nm == "orgeChampione")
        {
            pMisi5 += 1;
        }else if(nm == "curseOrge")
        {
            pMisi6 += 1;
        }else if(nm == "assasinOrge")
        {
            pMisi7 += 1;
        }else if(nm == "skullOrge")
        {
            pMisi8 += 1;
        }else if(nm == "redOrge")
        {
            pMisi9 += 1;
        }else if(nm == "redHunter")
        {
            pMisi10 += 1;
        }else if(nm == "orgeEater")
        {
            pMisi11 += 1;
        }else if(nm == "madDemon")
        {
            pMisi12 += 1;
        }
    }

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pMisi1.ToString() + "|";
        s += pMisi2.ToString() + "|";
        s += pMisi3.ToString() + "|";
        s += pMisi4.ToString() + "|";
        s += pMisi5.ToString() + "|";
        s += pMisi6.ToString() + "|";
        s += pMisi7.ToString() + "|";
        s += pMisi8.ToString() + "|";
        s += pMisi9.ToString() + "|";
        s += pMisi10.ToString() + "|";
        s += pMisi11.ToString() + "|";
        s += pMisi12.ToString() + "|";
        s += Smisi1.ToString() + "|";
        s += Smisi2.ToString() + "|";
        s += Smisi3.ToString() + "|";
        s += Smisi4.ToString() + "|";
        s += Smisi5.ToString() + "|";
        s += Smisi6.ToString() + "|";
        s += Smisi7.ToString() + "|";
        s += Smisi8.ToString() + "|";
        s += Smisi9.ToString() + "|";
        s += Smisi10.ToString() + "|";
        s += Smisi11.ToString() + "|";
        s += Smisi12.ToString() + "|";
        s += Smisi13.ToString() + "|";
        s += Smisi14.ToString() + "|";

        PlayerPrefs.SetString("Achivement", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("Achivement"))
            return;

        string[] data = PlayerPrefs.GetString("Achivement").Split('|');

        // Load Data
        pMisi1 = int.Parse(data[1]);
        pMisi2 = int.Parse(data[2]);
        pMisi3 = int.Parse(data[3]);
        pMisi4 = int.Parse(data[4]);
        pMisi5 = int.Parse(data[5]);
        pMisi6 = int.Parse(data[6]);
        pMisi7 = int.Parse(data[7]);
        pMisi8 = int.Parse(data[8]);
        pMisi9 = int.Parse(data[9]);
        pMisi10 = int.Parse(data[10]);
        pMisi11 = int.Parse(data[11]);
        pMisi12 = int.Parse(data[12]);
        Smisi1 = bool.Parse(data[13]);
        Smisi2 = bool.Parse(data[14]);
        Smisi3 = bool.Parse(data[15]);
        Smisi4 = bool.Parse(data[16]);
        Smisi5 = bool.Parse(data[17]);
        Smisi6 = bool.Parse(data[18]);
        Smisi7 = bool.Parse(data[19]);
        Smisi8 = bool.Parse(data[20]);
        Smisi9 = bool.Parse(data[21]);
        Smisi10 = bool.Parse(data[22]);
        Smisi11 = bool.Parse(data[23]);
        Smisi12 = bool.Parse(data[24]);
        Smisi13 = bool.Parse(data[25]);
        Smisi14 = bool.Parse(data[26]);
    }
}
