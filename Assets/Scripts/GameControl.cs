using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameControl : MonoBehaviour
{ 
    public static GameControl Instance { private set; get; }

    [SerializeField] private Text scoreText;
    [SerializeField] private Text startText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Bird bird;

    public GameObject gameOverText;
    public bool gameOver;
    public bool isPaused;

    public float scrollSpeed = -1.5f;
    private int score = 0;
    private const int TIME = 3;

    List<ScrollingObject> scrollingObjectList = new List<ScrollingObject>();
    public void AddScrollingObject(ScrollingObject scrollingObject) {
        if (!scrollingObjectList.Contains(scrollingObject)) {
            scrollingObjectList.Add(scrollingObject);
        }
    }
    public void RemoveScrollingObject(ScrollingObject scrollingObject) {
        if (scrollingObjectList.Contains(scrollingObject)) {
            scrollingObjectList.Remove(scrollingObject);
        }
    }
    public void NotifiyScrollingObjectList() {
        foreach (ScrollingObject scrollingObject in scrollingObjectList) {
            scrollingObject.GameOver();
        }
    }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
        StartCoroutine(PrintStartText(1f));
    }
    public void BirdScored() {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }
    public void BirdDied() {
        pauseText.gameObject.SetActive(false);
        gameOverText.SetActive(true);
        gameOver = true;
        NotifiyScrollingObjectList();
    }
    IEnumerator PrintStartText(float waitTime){
        Time.timeScale = 0;
        isPaused = true;
        int time = TIME;
        while (time>0){
            startText.text = time.ToString();
            yield return new WaitForSecondsRealtime(waitTime);
            time--;
        }
        startText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
        gameOver = false;
    }
    public void BackgroundClick(){
        if (isPaused == false && gameOver == false){
            bird.JumpBird();
        }
        else if (gameOver == true){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void IsPause(){
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        pauseText.text = (!isPaused) ? "II" : "►";
        if (!isPaused){
            startText.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(false);
            StartCoroutine(PrintStartText(1f));
        }
    }
}