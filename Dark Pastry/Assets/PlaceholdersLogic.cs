using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceholdersLogic : MonoBehaviour
{
    private static int _litFlames;

    public static void LightUp(float x, float y,Tilemap tm,TileBase noPlace,TileBase place,Tilemap screen,Tilemap wall)
    {
        if(Inventory.GetCount()==0) return;
        var loweredX = (int) Math.Floor(x);
        var incrementedX = (int) Math.Ceiling(x);
        var loweredY = (int) Math.Floor(y);
        var incrementedY =(int) Math.Ceiling(y);
        Vector3Int[] possiblePositions =
        {
            new Vector3Int(loweredX,loweredY,0),
            new Vector3Int(incrementedX,loweredY,0),
            new Vector3Int(loweredX,incrementedY,0),
            new Vector3Int(incrementedX,incrementedY,0) 
        };
        foreach (var position in possiblePositions)
        {
            if(tm.GetTile(position)!=noPlace) continue;
            tm.SetTile(position,place);
            Inventory.RemoveItem(screen);
            _litFlames++;
        }

        if (_litFlames == 4) OpenWall(wall);
    }

    private static void OpenWall(Tilemap wall)
    {
        wall.SetTile(new Vector3Int(-1,18,0), null);
        wall.SetTile(new Vector3Int(0, 18, 0), null);
        wall.SetTile(new Vector3Int(1,18,0), null );
    }
}
