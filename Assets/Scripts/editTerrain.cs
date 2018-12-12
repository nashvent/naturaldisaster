using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editTerrain : MonoBehaviour {

    // Use this for initialization
    public Terrain terrain;
    public TerrainData terrainData;
    int xResolution;
    int zResolution;
    float[,] heights;

    void Start () {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        xResolution = terrain.terrainData.heightmapWidth;
        zResolution = terrain.terrainData.heightmapHeight;
        heights = terrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
/*
        Vector3 point = Vector3.zero;
        point.x = 63;
        point.y = 10;
        point.z = 54;
        raiseTerrain(point);*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void lowerTerrain()
    {
        int mapWidth = 50;//(int)(terrain.terrainData.size.x);
        int mapHeight = 50;//(int)(terrain.terrainData.size.y);
        float[,] heights = terrainData.GetHeights(50,50, mapWidth, mapHeight);
        
        for(int z = 0; z< mapHeight; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //float cos = Mathf.Cos(x);
                //float sin = -Mathf.Sin(z);
                heights[x, z] = 0;//(cos - sin) / 250;
            }
        }       
        terrainData.SetHeights(50, 50, heights);
    }
    public void raiseTerrain(Vector3 point)
    {
        int terX = (int)((point.x / terrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / terrain.terrainData.size.z) * zResolution);
        float[,] height = terrain.terrainData.GetHeights(terX - 4, terZ - 4, 9, 9);  //new float[1,1];

        for (int tempY = 0; tempY < 9; tempY++)
            for (int tempX = 0; tempX < 9; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - 4f) + Mathf.Abs((float)tempX - 4f);
                float maxDist = 8f;
                float proportion = dist_to_target / maxDist;

                height[tempX, tempY] += 0.1f * (1f - proportion);
                heights[terX - 4 + tempX, terZ - 4 + tempY] += 0.1f * (1f - proportion);
            }

        terrain.terrainData.SetHeights(terX - 4, terZ - 4, height);
    }

    private void lower(Vector3 point)
    {
        int terX = (int)((point.x / terrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / terrain.terrainData.size.z) * zResolution);
        Debug.Log(terX);

        float y = heights[terX, terZ];
        y -= 10f;
        float[,] height = new float[1, 1];
        height[0, 0] = y;
        heights[terX, terZ] = y;
        Debug.Log(height);
        terrain.terrainData.SetHeights(terX, terZ, height);
    }

}
