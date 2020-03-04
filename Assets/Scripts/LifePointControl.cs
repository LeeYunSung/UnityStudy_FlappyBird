using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointControl : MonoBehaviour
{
    public GameObject life1, life2, life3;
    public static LifePointControl Instance { private set; get; }

    //public Stack<GameObject> lifeObjectStack;
    int lifeCount = 3;

    void Awake(){
        if (Instance == null){
            Instance = this;
        }
    }
    void Start(){
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);
    }
    public void deleteLifePoint(){
        lifeCount--;
        switch (lifeCount){
            case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                break;
            case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                break;
            case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                GameControl.Instance.BirdDied();
                break;
        }
    }
}
