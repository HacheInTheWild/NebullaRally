using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public Material[] skin1;
    public Material[] skin2;
    public Material[] skin3;
    public Material[] skinDef;

    public GameObject nave;

    public GameObject cam;

    void Start()
    {
        ShowSkin();
    }

    public void ShowSkin()
    {
        if (PlayerPrefs.GetString("Skin", "Default") == "Tiles")
        {
            nave.GetComponent<MeshRenderer>().materials = skin1;
        }
        else if (PlayerPrefs.GetString("Skin", "Default") == "Grass")
        {
            nave.GetComponent<MeshRenderer>().materials = skin2;
        }
        else if (PlayerPrefs.GetString("Skin", "Default") == "Rusty")
        {
            nave.GetComponent<MeshRenderer>().materials = skin3;
        }
        else
        {
            nave.GetComponent<MeshRenderer>().materials = skinDef;
        }
    }

    public void SetSkin(int id)
    {
        switch (id)
        {
            case 1:
                PlayerPrefs.SetString("Skin", "Tiles");
                PlayerPrefs.Save();
                ShowSkin();
                break;

            case 2:
                PlayerPrefs.SetString("Skin", "Grass");
                PlayerPrefs.Save();
                ShowSkin();
                break;

            case 3:
                PlayerPrefs.SetString("Skin", "Rusty");
                PlayerPrefs.Save();
                ShowSkin();
                break;

            default:
                PlayerPrefs.SetString("Skin", "def");
                PlayerPrefs.Save();
                ShowSkin();
                break;
        }
    }

    public void VerSkin()
    {
        var fly = cam.GetComponent<FlyThroughCam>();
        if (fly.enabled == false)
        {
            fly.enabled = true;
        }
        else
        {
            fly.enabled = false;
        }
        
    }
}
