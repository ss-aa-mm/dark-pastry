using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AgataMovement : MonoBehaviour
{
    private Transform _tr;
    
    private Vector3Int _pos;

    private float _unit;

    private float _speed=5f;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        _tr = transform;
        _unit = _speed * Time.deltaTime;
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(_unit * Input.GetAxis("Horizontal"), 0f, 0f);
        }

        if (Input.GetButton("Vertical"))
        {
            transform.Translate(0f, _unit * Input.GetAxis("Vertical"), 0f);
        }

        /*if (transform.position.x > 5f || transform.position.x < -5f)
        {
            var newPos = transform.position;
            newPos.x = -Math.Sign(transform.position.x) * 5f;
            transform.position = newPos;
        }
        if (transform.position.y > 5f || transform.position.y < -5f)
        {
            var newPos = transform.position;
            newPos.y = -Math.Sign(transform.position.y) * 5f;
            transform.position = newPos;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            var tm = other.gameObject.GetComponent<Tilemap>();
            if (tm != null)
            {
                Debug.Log("Collided");
            }
            _pos = ComputePos(_tr.position);
            DeleteNearestTile(tm,_pos);
            _speed += 1f;
        }
    }

    private Vector3Int ComputePos(Vector3 vec)
    {
        var x = Convert.ToInt32(Math.Ceiling(vec.x));
        var y = Convert.ToInt32(Math.Ceiling(vec.y));
        return new Vector3Int(x,y,0);
    }

    private void DeleteNearestTile(Tilemap tm, Vector3Int position)
    {
        int i;
        const int minVal = -50;
        const int maxVal = 50;
        double minDistance=2000;
        var delPos=new Vector3Int(0,0,0);
        for(i=minVal;i<maxVal;i++)
        {
            int j;
            for (j = minVal; j < maxVal; j++)
            {
                var tilePos = new Vector3Int(i, j, 0);
                var detectedDistance = ComputeDistance2D(position, tilePos);
                if (tm.GetTile(tilePos) == null || !(detectedDistance < minDistance)) continue;
                minDistance = detectedDistance;
                delPos = tilePos;
            }
        }
        tm.SetTile(delPos,null);
    }

    private double ComputeDistance2D(Vector3Int pos1, Vector3Int pos2)
    {
        return Math.Sqrt(Math.Pow(pos1.x - pos2.x, 2) + Math.Pow(pos1.y - pos2.y, 2));
    }
}
