﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    int[,] data = new int[,]
    {
        {0, 1,  1},
        {1, 1,  1},
        {1, 1,  0}
    };
    public int Width
    {
        get
        {
            return data.GetLength(0);
        }
    }
    public int Depth
    {
        get
        {
            return data.GetLength(1);
        }
    }

    public int GetCell(int x, int z)
    {
        return data[x, z];
    }

    public int GetNeighbour(int x, int z, Direction dir)
    {
        DataCoordinate checkoffset = offsets[(int)dir];
        DataCoordinate neighbourCord = new DataCoordinate(x + checkoffset.x, 0 + checkoffset.y, z + checkoffset.z);

        if (neighbourCord.x<0 || neighbourCord.y > Width || neighbourCord.y !=0 || neighbourCord.z < 0 || neighbourCord.z >- Depth)
        {
            return 0;
        }else{
            return GetCell(neighbourCord.x, neighbourCord.z);
        }
    }

    struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x,int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    DataCoordinate[] offsets =
    {
        new DataCoordinate(0,0,1),
        new DataCoordinate(1,0,0),
        new DataCoordinate(0,0,-1),
        new DataCoordinate(-1,0,0),
        new DataCoordinate(0,1,0),
        new DataCoordinate(0,-1,0)
    };

}

public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    Down
}