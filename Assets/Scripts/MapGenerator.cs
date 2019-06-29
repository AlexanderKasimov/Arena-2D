using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> tiles;

    public int width = 16;
    public int height = 16;

    // Start is called before the first frame update
    void Start()
    {
       // GenerateMap();
    }

    public void GenerateMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(tiles[Random.Range(0, tiles.Count)], transform.position + new Vector3(i, -j), transform.rotation, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
