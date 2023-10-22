using System;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public LineRenderer lr;
    private Single sr, sc;
    private Int64 rowCount, colCount;
    public Single gridSize;

    public void OnUpdate(Int64 size)
    {
        rowCount = size;
        colCount = size;
        if (rowCount + colCount > 0)
        {
            sr = -(rowCount / 2);
            sc = -(colCount / 2);
            makeGrid(lr, sr, sc, rowCount, colCount);
        }
    }

    void initLineRenderer(LineRenderer lr)
    {
        lr.startWidth = lr.endWidth = 0.1f;
    }

    void makeGrid(LineRenderer lr, Single sr, Single sc, Int64 rowCount, Int64 colCount)
    {
        List<Vector3> gridPos = new List<Vector3>();

        Single ec = sc + colCount * gridSize;

        gridPos.Add(new Vector3(sr, sc, transform.position.z));
        gridPos.Add(new Vector3(sr, ec, transform.position.z));

        Int32 toggle = -1;
        Vector3 currentPos = new Vector3(sr, ec, transform.position.z);
        for (Int32 i = 0; i < rowCount; i++)
        {
            Vector3 nextPos = currentPos;

            nextPos.x += gridSize;
            gridPos.Add(nextPos);

            nextPos.y += (colCount * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        currentPos.x = sr;
        gridPos.Add(currentPos);

        Int32 colToggle = toggle = 1;
        if (currentPos.y == ec) colToggle = -1;

        for (Int32 i = 0; i < colCount; i++)
        {
            Vector3 nextPos = currentPos;

            nextPos.y += (colToggle * gridSize);
            gridPos.Add(nextPos);

            nextPos.x += (rowCount * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        lr.positionCount = gridPos.Count;
        lr.SetPositions(gridPos.ToArray());
    }

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        initLineRenderer(lr);

        makeGrid(lr, sr, sc, rowCount, colCount);
    }
}