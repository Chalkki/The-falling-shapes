using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousFallTime;
    public float fallingTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    // Start is called before the first frame update
    void Start()
    {
        previousFallTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1,0,0);
            if (!AbleMove())
            {
               transform.position += new Vector3(1, 0, 0);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!AbleMove())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!AbleMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        if (Time.time - previousFallTime > (Input.GetKey(KeyCode.DownArrow) ? fallingTime / 10f : fallingTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousFallTime = Time.time;
            if (!AbleMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                FindObjectOfType<Spawn>().NewTetromino();
                this.enabled = false;
            }
        }
    }

    private void CheckLines()
    {
        for (int i = 0; i < height; i++)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                MoveRowDown(i);
            }
        }
    }

    private bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j,i] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;
        }
    }

    private void MoveRowDown(int i)
    {
        for(int y = i; y<height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y-1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j, y-1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }
    private void AddToGrid()
    {
        foreach (Transform trans in transform)
        {
            int roundX = Mathf.RoundToInt(trans.position.x);
            int roundY = Mathf.RoundToInt(trans.position.y);
            grid[roundX,roundY] = trans;
        }
    }
    private bool AbleMove()
    {
        foreach(Transform trans in transform)
        {
            int roundX = Mathf.RoundToInt(trans.position.x);
            int roundY = Mathf.RoundToInt(trans.position.y);
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (grid[roundX,roundY] != null)
            {
                if (roundY == 18)
                {
                    EndGame();
                }
                return false;
            }
        }
        return true;
    }
    private void EndGame()
    {
        Debug.Log("Game Over");
        this.enabled= false;
    }
}
