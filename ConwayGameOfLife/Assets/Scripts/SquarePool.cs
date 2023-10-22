using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePool : ObjectPool
{
    public List<GameObject> SpawnedSquare = new List<GameObject>();
    private Vector3 initialMousePos = Vector3.zero;
    private Vector3 initialCameraPos = Vector3.zero;
    private Single mouseSpeed;

    public void OnUpdate(Single speed)
    {
        mouseSpeed = speed;
    }
    private void Update()
    {
        GetInput(); //#todo : 실행되면 입력 못받게 하기
        if(initialMousePos != Vector3.zero)
        {
            Vector3 dir = Camera.main.ScreenToViewportPoint(Input.mousePosition) - initialMousePos;
            dir.z = 10;
            Camera.main.transform.position = initialCameraPos - (dir * mouseSpeed);
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(1))
            RightMouseDown();
        else if (Input.GetMouseButtonDown(0))
            LeftMouseDown();
        else if (Input.GetMouseButtonDown(2))
            initialMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        else if (Input.GetMouseButtonUp(2))
        {
            initialMousePos = Vector3.zero;
            initialCameraPos = Camera.main.transform.position;
        }
    }
    private void LeftMouseDown()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.5f, 0);
        SpawnedSquare.Add(SpawnObject("Square", pos));
    }
    private void RightMouseDown()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.5f, 0);
        for(Int32 i = 0; i < SpawnedSquare.Count; i++)
        {
            if (SpawnedSquare[i].transform.position == pos)
            {
                SpawnedSquare[i].SetActive(false);
                SpawnedSquare.RemoveAt(i);
                break;
            }
        }
    }
}
