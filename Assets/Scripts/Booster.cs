using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    [SerializeField] private Image boosterImage;
    [SerializeField] private GameObject boosterText;
    [SerializeField] private Bird bird;

    private const float MAXTIME = 10f;
    private float timeLeft;

    public void Start() {
        timeLeft = 0;
        boosterText.SetActive(false);
        boosterImage.raycastTarget = false;
        boosterImage.gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(ProcessTime());
    }
    public void BoosterButton() {
        StartCoroutine(Boost());
    }
    IEnumerator ProcessTime(){
        while (timeLeft<10){
            boosterImage.gameObject.SetActive(true);
            timeLeft += Time.deltaTime;
            boosterImage.fillAmount = timeLeft / MAXTIME;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.GetComponent<Animator>().enabled = true;
        boosterText.SetActive(true);
        boosterImage.raycastTarget = true;
    }
    public void StopBoostProcess(){
        boosterImage.raycastTarget = false;
        Time.timeScale = 0;
        //StopCoroutine(ProcessTime());
    }
    IEnumerator Boost() {
        bird.InvinibilityOn();
        GameControl.Instance.NotifyBoost();
        yield return new WaitForSeconds(5f);
        GameControl.Instance.NotifyBoostEnd();
        bird.InvinibilityOff();
        Start();
    }
}
