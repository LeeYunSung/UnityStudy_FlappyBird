using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
    [SerializeField] private Collider2D collider1;
    [SerializeField] private Collider2D collider2;

    bool isUsed = false;
    public static List<Column> columnList = new List<Column>();//현재 활성화 되어 있는 Column들
    public static Column lastColumn;

    public void Init() {
        isUsed = false;
        columnList.Add(this);
        lastColumn = this;
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
            GameControl.Instance.GetComponent<ColumnPool>().SpawnColumn(lastColumn.transform.position.x);
            yield return new WaitForSeconds(5f);
            columnList.Remove(this);
            GameControl.Instance.GetComponent<ColumnPool>().Despawn(this);
        }
    }
    public static void TriggerOn(){
        foreach (Column column in columnList){
            column.collider1.isTrigger = true;
            column.collider2.isTrigger = true;
        }
    }
    public static void TriggerOff(){
        foreach (Column column in columnList){
            column.collider1.isTrigger = false;
            column.collider2.isTrigger = false;
        }
    }
    private void OnDestroy(){
        columnList.Remove(this);
    }
}