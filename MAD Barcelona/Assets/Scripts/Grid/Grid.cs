using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Visuals;
public class Grid<TGridObject>
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 100;
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridValueChange;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private Vector3 m_OriginPos;
    private int m_Width;
    private int m_Height;
    private float m_CellSize;
    private TGridObject[,] m_GridArray;
    private TextMesh[,] m_DebugTextArray;

    bool m_ShowDebug = true;
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        m_Width = width;
        m_Height = height;
        m_CellSize = cellSize;
        m_OriginPos = originPosition;

        m_GridArray = new TGridObject[m_Width, m_Height];
        for (int x = 0; x < m_GridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_GridArray.GetLength(1); y++)
            {
                m_GridArray[x, y] = createGridObject(this, x, y);
            }
        }

        if (m_ShowDebug)
        {
            m_DebugTextArray = new TextMesh[m_Width, m_Height];
            for (int x = 0; x < m_GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < m_GridArray.GetLength(1); y++)
                {
                    m_DebugTextArray[x, y] = UtilsClass.CreateWorldText(m_GridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(m_CellSize, m_CellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, m_Height), GetWorldPosition(m_Width, m_Height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(m_Width, 0), GetWorldPosition(m_Width, m_Height), Color.white, 100f);

            OnGridValueChange += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                m_DebugTextArray[eventArgs.x, eventArgs.y].text = m_GridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }
    public int GetWidth()
    {
        return m_Width;
    }
    public int GetHeight()
    {
        return m_Height;
    }
    public float GetCellSize()
    {
        return m_CellSize;
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * m_CellSize + m_OriginPos;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - m_OriginPos).x / m_CellSize);
        y = Mathf.FloorToInt((worldPosition - m_OriginPos).y / m_CellSize);
    }
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x < 0 || y < 0 || x >= m_Width || y >= m_Height)
            return;

        m_GridArray[x, y] = value;
        m_DebugTextArray[x, y].text = m_GridArray[x, y].ToString();

        if (OnGridValueChange != null) OnGridValueChange(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChange != null) OnGridValueChange(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }
    public TGridObject GetGridObject(int x, int y)
    {
        if (x < 0 || y < 0 || x >= m_Width || y >= m_Height)
            return default(TGridObject);

        return m_GridArray[x, y];
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
