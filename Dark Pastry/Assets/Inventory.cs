using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    
    public enum ItemList
    {
       Mushroom,
       Skull,
       Empty
    }

    private static int _itemsCollected;

    private static Dictionary<string, TileBase> _items = new Dictionary<string, TileBase>();

    public static void PickItem(TileBase item, Tilemap tm)
    {
        if (_itemsCollected < 5)
        {
            _items.Add(_itemsCollected.ToString(),item);
            Debug.Log("Picked up");
            ScreenItemsUpdate(_itemsCollected, item, tm);
            _itemsCollected++;
        }
        else
        {
            Debug.Log("Cannot collect object");
        }
    }

    public static void RemoveItem(Tilemap tm)
    {
        if(_itemsCollected==0) return;
        _items.Remove((_itemsCollected - 1).ToString());
        ScreenItemsUpdate(_itemsCollected-1, null, tm);
        _itemsCollected--;
    }

    private static void ScreenItemsUpdate(int position, TileBase tile, Tilemap tm)
    {
       tm.SetTile(new Vector3Int(3+position,-1,0), tile );
    }

    public static int GetCount()
    {
        return _itemsCollected;
    }
}
