using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int[,] map;

    public int width = 256;
    public int height = 256;
    public int randomFillPercent = 50;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        SmoothMap();
    }

    private void RandomFillMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = (Random.Range(0, 100) < randomFillPercent) ? 1 : 0;
            }
        }
    }

    private void SmoothMap()
    {
        int[,] newMap = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int wallCount = GetSurroundingWallCount(i, j);
                if (wallCount > 4)
                    newMap[i, j] = 1;
                else
                    newMap[i, j] = 0;
            }
        }

        map = newMap;
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

        void OnDrawGizmos()
        {
            if (map != null)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Gizmos.color = (map[i, j] == 1) ? Color.black : Color.white;
                        Gizmos.DrawCube(new Vector3(i, j, 0), Vector3.one);
                    }
                }
            }
        }
    }
