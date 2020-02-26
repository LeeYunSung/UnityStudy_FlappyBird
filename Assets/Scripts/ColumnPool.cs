using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    [SerializeField] private int columnPoolSize = 5;
    [SerializeField] private GameObject columnPrefabs;
    [SerializeField] private GameObject[] columns;

    private const float COLUMN_MIN = -2f;
    private const float COLUMN_MAX = 2f;

    private float spawnXPosition;
    private float spawnYPosition;

    private int currentColumn = 0;

    [SerializeField] private GameObject columnsss;

    void Start(){
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++){
            RandomY();
            columns[i] = (GameObject)Instantiate(columnPrefabs, new Vector2(spawnXPosition,spawnYPosition), Quaternion.identity);
            columns[i].transform.SetParent(columnsss.transform);
            spawnXPosition += 5f;
        }
    }
    public void Pooling(){
        RandomY();
        columns[currentColumn].transform.position = new Vector2(5f, spawnYPosition);
        currentColumn++;
        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }
    public void RandomY(){
        spawnYPosition = UnityEngine.Random.Range(COLUMN_MIN, COLUMN_MAX);
    }
}
