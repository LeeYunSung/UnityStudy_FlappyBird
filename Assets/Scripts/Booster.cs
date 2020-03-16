using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour{
    [SerializeField] private GameObject boosterCover;
    [SerializeField] private Image boosterImage;
    [SerializeField] private GameObject boosterText;
    [SerializeField] private Bird bird;

    private const float MAXTIME = 5f;
    private float timeLeft;

    public void Start() {
        timeLeft = 0;
        boosterText.SetActive(false);
        gameObject.GetComponent<Animator>().enabled = false;
        boosterImage.GetComponent<Outline>().enabled = false;
        StartCoroutine(ProcessTime());
    }
    public void BoosterButton() {
        boosterCover.SetActive(true);
        StartCoroutine(Boost());
    }
    IEnumerator ProcessTime(){
        while (timeLeft< MAXTIME){
            boosterImage.gameObject.SetActive(true);
            timeLeft += Time.deltaTime;
            boosterImage.fillAmount = timeLeft / MAXTIME;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        boosterText.SetActive(true);
        boosterCover.SetActive(false);
        gameObject.GetComponent<Animator>().enabled = true;
        //boosterImage.GetComponent<Outline>().enabled = true;
    }
    public void StopBoost(){
        StopCoroutine(ProcessTime());
        boosterCover.SetActive(true);
        //Time.timeScale = 0;
    }
    IEnumerator Boost(){
        bird.isBoost();
        ScrollingObject.SpeedUp();
        yield return new WaitForSecondsRealtime(5f);
        ScrollingObject.SpeedDown();
        Start();
    }
}