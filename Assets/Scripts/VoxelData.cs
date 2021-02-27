using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    int[,] data;
    public VoxelData(int numOfsquers)
    {
        data = GetData(numOfsquers);
    }

    public int Width
    {
        get { return data.GetLength(0); }
    }

    public int Depth
    {
        get { return data.GetLength(1); }
    }

    public int GetCell(int x, int z)
    {
        return data[x, z];
    }

    public int GetNeighbor(int x, int z, Direction direction)
    {
        DataCoordinate offsetToCheck = offsets[(int)direction];
        DataCoordinate neighborCord = new DataCoordinate(x + offsetToCheck.x, 0 + offsetToCheck.y, z + offsetToCheck.z);

        if(neighborCord.x < 0 || neighborCord.x >= Width || neighborCord.y != 0 || neighborCord.z < 0 || neighborCord.z >= Depth)
        {
            return 0;
        }
        else
        {
            return GetCell(neighborCord.x, neighborCord.z);
        }
    }

    struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    DataCoordinate[] offsets =
    {
        new DataCoordinate( 0,  0,  1),
        new DataCoordinate( 1,  0,  0),
        new DataCoordinate( 0,  0, -1),
        new DataCoordinate(-1,  0,  0),
        new DataCoordinate( 0,  1,  0),
        new DataCoordinate( 0, -1,  0)
    };

    public int[,] GetData(int numberOfCubes)
    {
        if(numberOfCubes == 3)
        {
            return new int[,] {{ 1, 0, 0 },
                               { 1, 1, 0 }};
        }
        else if (numberOfCubes == 4)
        {
            return new int[,] {{ 1, 1, 0 },
                               { 1, 1, 0 }};
        }
        else if (numberOfCubes == 5)
        {
            return new int[,] {{ 1, 1, 0 },
                               { 0, 1, 0 },
                               { 0, 1, 1 } };
        }
        else if (numberOfCubes == 6)
        {
            return new int[,] {{ 1, 1, 1 },
                               { 1, 0, 1 },
                               { 0, 1, 0 } };

        }
        else if (numberOfCubes == 7)
        {
            return new int[,] {{ 0, 1, 0 },
                               { 1, 1, 1 },
                               { 0, 1, 0 },
                               { 1, 1, 0 }};
        }
        else if (numberOfCubes == 8)
        {
            return new int[,] {{ 0, 1, 0 },
                               { 1, 1, 1 },
                               { 0, 1, 0 },
                               { 0, 1, 0 },
                               { 1, 0, 1 }};
        }
        else if (numberOfCubes == 9)
        {
            return new int[,] {{ 1, 0, 0 },
                               { 0, 1, 0 },
                               { 0, 0, 1 },
                               { 0, 1, 0 },
                               { 1, 0, 0 },
                               { 0, 1, 0 },
                               { 0, 0, 1 },
                               { 0, 1, 0 },
                               { 1, 0, 0 }};
        }
        else if (numberOfCubes == 10)
        {
            return new int[,] {{ 1, 1, 1 },
                               { 1, 0, 1 },
                               { 1, 0, 1 },
                               { 1, 1, 1 } };
        }
        else
        {
            return new int[,] { };
        }
    }
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


