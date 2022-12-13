using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap
{
    private Grid<TileMapObject> grid;
    public TileMap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<TileMapObject>(width, height, cellSize, originPosition, (Grid<TileMapObject> g, int x, int y) => new TileMapObject(g, x, y));
    }
    public void SetTilemapSprite(Vector3 worldPosition, TileMapObject.TileMapSprite tileMapSprite)
    {
        TileMapObject tileMapObject = grid.GetGridObject(worldPosition);
        if (tileMapObject != null)
        {
            tileMapObject.SetTilemapSprite(tileMapSprite);
        }
    }

    public class TileMapObject
    {
        public enum TileMapSprite
        {
            None,
            Ground
        }
        private Grid<TileMapObject> grid;
        public int x;
        public int y;
        private TileMapSprite tileMapSprite;

        public TileMapObject(Grid<TileMapObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        public void SetTilemapSprite(TileMapSprite tileMapSprite)
        {
            this.tileMapSprite = tileMapSprite;
            grid.TriggerGridObjectChanged(x, y);
        }
        public override string ToString()
        {
            return tileMapSprite.ToString();
        }
    }
}
