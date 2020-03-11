using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    [SerializeField] private Image boosterImage;
    [SerializeField] private GameObject boosterText;
    [SerializeField] private Bird bird;

    private const float MAXTIME = 5f;
    private float timeLeft;
    public static bool isBoost = false;

    public void Start() {
        timeLeft = 0;
        boosterText.SetActive(false);
        gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(ProcessTime());
    }
    public void BoosterButton() {
        if (isBoost){
            isBoost = false;
            StartCoroutine(Boost());
        }
        else GameControl.Instance.BackgroundClick();
    }
    IEnumerator ProcessTime(){
        while (timeLeft< MAXTIME){
            boosterImage.gameObject.SetActive(true);
            timeLeft += Time.deltaTime;
            boosterImage.fillAmount = timeLeft / MAXTIME;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isBoost = true;
        boosterText.SetActive(true);
        gameObject.GetComponent<Animator>().enabled = true;
    }
    public void StopBoost(){
        StopCoroutine(ProcessTime());
        //Time.timeScale = 0;
    }
    IEnumerator Boost() {
        Bird.isBoost = true;
        bird.SuperTimeAdd(5f);
        ScrollingObject.SpeedUp();
        yield return new WaitForSecondsRealtime(5f);
        ScrollingObject.SpeedDown();
        Bird.isBoost = false;
        Start();
    }
}