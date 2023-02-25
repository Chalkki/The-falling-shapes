using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Spawn : MonoBehaviour
{
    public GameObject[] Tetrominos;
    public TextMeshProUGUI nextShape;
    private int currentIdx;
    private int nextIdx;
    private void Start()
    {
        nextIdx = Random.Range(0, Tetrominos.Length);
        NewTetromino();
    }

    public void NewTetromino()
    {
        currentIdx = nextIdx;
        Instantiate(Tetrominos[currentIdx],transform.position, Quaternion.identity);
        nextIdx = Random.Range(0, Tetrominos.Length);
        ShowNext();
    }
    
    private void ShowNext()
    {
        nextShape.text = Tetrominos[nextIdx].gameObject.name;
        nextShape.color = Tetrominos[nextIdx].gameObject.GetComponentInChildren<SpriteRenderer>().color;
    }
}
