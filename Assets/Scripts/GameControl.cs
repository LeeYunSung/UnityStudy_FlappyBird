using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance { private set; get; }

    [SerializeField] private Text scoreText;
    [SerializeField] private Text startText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Bird bird;

    public GameObject gameOverText;
    public bool gameOver = true;
    public bool isPaused = true;

    public float scrollSpeed = -1.5f;
    private int score = 0;
    
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
        Time.timeScale = 0;
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    private void Start(){
        StartCoroutine(PrintStartText(1f));
        gameOver = false;
        isPaused = false;
    }
    void Update() {
        if (isPaused==false) {
            var mouseInput = Input.GetMouseButtonDown(0);
            if (mouseInput) {
                if (gameOver == true) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                bird.JumpBird();
            }
        }
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
        for (int i = 3; i > 0; i--){
            startText.text = i.ToString();
            yield return new WaitForSecondsRealtime(waitTime);
        }
        startText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public void IsPause(){
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        pauseText.text = (!isPaused) ? "II" : "►";
    }
}