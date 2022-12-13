using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Visuals;
public class Testing : MonoBehaviour
{
    PathFinding pathFinding;
    TileMap tileMap;
    void Start()
    {
        //pathFinding = new PathFinding(10, 10);
        tileMap = new TileMap(20, 10, 10f, Vector3.zero);
    }

    // Update is called once per frame
    private void Update()
    {
        //TestPathFinding();
    }
    private void TestTileMap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            tileMap.SetTilemapSprite(mouseWorldPosition, TileMap.TileMapObject.TileMapSprite.Ground);
        }
    }
    private void TestPathFinding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathFinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log(i);
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f,
                     new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 100f);
                }
            }
        }
    }
}

