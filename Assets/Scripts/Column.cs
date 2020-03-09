using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {

    [SerializeField] private Collider2D collider1;
    [SerializeField] private Collider2D collider2;

    bool isUsed = false;
    public static List<Column> columnList = new List<Column>();

    public void Init() {
        isUsed = false;
        columnList.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isUsed && collision.GetComponent<Bird>() != null) {
            GameControl.Instance.BirdScored();
            StartCoroutine(Exchange());
            isUsed = true;
        }
    }
    IEnumerator Exchange() {
        if (GameControl.Instance.gameOver != true) {
            GameControl.Instance.GetComponent<ColumnPool>().SpawnColumn();
            yield return new WaitForSeconds(5f);
            columnList.Remove(this);
            GameControl.Instance.GetComponent<ColumnPool>().Despawn(this);
        }
    }
    public static void NotifyTriggerOn(){
        foreach (Column column in columnList){
            column.collider1.isTrigger = true;
            column.collider2.isTrigger = true;
        }
    }
    public static void NotifyTriggerOff(){
        foreach (Column column in columnList){
            column.collider1.isTrigger = false;
            column.collider2.isTrigger = false;
        }
    }
    private void OnDestroy(){
        columnList.Clear();
    }
}