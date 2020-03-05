using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    [SerializeField] private Bird bird;
    private const float MAXTIME = 10f;
    public Image boosterImage;
    [SerializeField] private GameObject boosterText;
    private float timeLeft;

    public void Start() {
        timeLeft = 0;
        boosterImage.raycastTarget = false;
        boosterImage.gameObject.SetActive(false);
        boosterImage.transform.GetComponent<Outline>().enabled = false;
        StartCoroutine(ProcessTime());
    }
    public void BoosterButton(){
        boosterText.SetActive(false);
        boosterImage.transform.GetComponent<Outline>().enabled = true;
        StartCoroutine(Boost());
    }
    IEnumerator ProcessTime(){
        while (timeLeft<10){
            boosterImage.gameObject.SetActive(true);
            timeLeft += Time.deltaTime;
            boosterImage.fillAmount = timeLeft / MAXTIME;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        boosterText.SetActive(true);
        boosterImage.raycastTarget = true;
    }
    public void StopBoostProcess(){
        boosterImage.raycastTarget = false;
        StopCoroutine(ProcessTime());
    }
    IEnumerator Boost() {
        bird.BirdTriggerOn();
        GameControl.Instance.NotifyBoost();
        yield return new WaitForSeconds(5f);
        GameControl.Instance.NotifyBoostEnd();
        bird.BirdTriggerOff();
        Start();
    }
}
