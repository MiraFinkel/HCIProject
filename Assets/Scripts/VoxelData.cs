using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayConvert
{
    public static T[,,] To2DArray<T>(params T[][,] arrays)
    {
        if (arrays == null) throw new ArgumentNullException("arrays");
        foreach (var a in arrays)
        {
            if (a == null) throw new ArgumentException("can not contain null arrays");
            if (a.Length != arrays[0].Length) throw new ArgumentException("input arrays should have the same length");
        }

        var width = arrays.Length;
        var depth = arrays[0].GetLength(0);
        var height = arrays[0].GetLength(1);

        var result = new T[width, height, depth];

        for (int i = 0; i < height; i++)
        { 
            for (int j = 0; j < width; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    result[i, j, k] = arrays[i][j, k];
                }
                
            }
        }
        return result;
    }
}

public class VoxelData
{
    int[,] data;
    int[,,] data3;
    int[,,] sunData;

    [SerializeField] private int dataScale = 3;

    public VoxelData(int numOfsquers, bool isItSun)
    {
        if(isItSun)
        {
            data3 = GetSunData();
        }
        else
        {
            data3 = Get3Data(numOfsquers);
        }
    }

    public int Width
    {
        get { return data3.GetLength(0); }
    }

    public int Height
    {
        get { return data3.GetLength(2); }
    }

    public int Depth
    {
        get { return data3.GetLength(1); }
    }

    public int Get2Cell(int x, int z)
    {
        return data[x, z];
    }

    public int Get3Cell(int x, int y, int z)
    {
        return data3[x, y, z];
    }

    public int Get2Neighbor(int x, int z, Direction direction)
    {
        DataCoordinate offsetToCheck = offsets[(int)direction];
        DataCoordinate neighborCord = new DataCoordinate(x + offsetToCheck.x, 0 + offsetToCheck.y, z + offsetToCheck.z);

        if(neighborCord.x < 0 || neighborCord.x >= Width || neighborCord.y != 0 || neighborCord.z < 0 || neighborCord.z >= Depth)
        {
            return 0;
        }
        else
        {
            return Get2Cell(neighborCord.x, neighborCord.z);
        }
    }
    public int Get3Neighbor(int x, int y, int z, Direction direction)
    {
        DataCoordinate offsetToCheck = offsets[(int)direction];
        DataCoordinate neighborCord = new DataCoordinate(x + offsetToCheck.x, y + offsetToCheck.y, z + offsetToCheck.z);

        if (neighborCord.x < 0 || neighborCord.x >= Width || neighborCord.y < 0 || neighborCord.y >= Height || neighborCord.z < 0 || neighborCord.z >= Depth)
        {
            return 0;
        }
        else
        {
            return Get3Cell(neighborCord.x, neighborCord.y, neighborCord.z);
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

    public int[,] Get2Data(int numberOfCubes)
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
    public int[,,] Get3Data(int numberOfCubes)
    {
        int counter = 0;
        int[,] binaryArr1 = GetBinaryList(numberOfCubes, ref counter);
        int[,] binaryArr2 = GetBinaryList(numberOfCubes, ref counter);
        int[,] binaryArr3 = GetBinaryList(numberOfCubes, ref counter);
        var convertedArray = ArrayConvert.To2DArray(binaryArr1, binaryArr2, binaryArr3);
        return convertedArray;
    }



    int[,] GetBinaryList(int numberOfCubes, ref int counter)
    {
        int[,] binaryArr = new int[dataScale, dataScale];
        for (int i = 0; i < dataScale; i++)
        {
            for (int j = 0; j < dataScale; j++)
            {
                if (counter == numberOfCubes) break;
                int num = UnityEngine.Random.Range(0, 2);
                binaryArr[i, j] = num;
                if (num == 1) counter++;
            }
            if (counter == numberOfCubes) break;
        }
        return binaryArr;
    }
    public int[,,] GetSunData()
    {
        return new int[,,] { { { 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0 } },
                             { { 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 1, 1, 1, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 0, 0, 0, 0 } },
                             { { 0, 0, 1, 0, 0 },
                               { 0, 1, 1, 1, 0 },
                               { 1, 1, 1, 1, 1 },
                               { 0, 1, 1, 1, 0 },
                               { 0, 0, 1, 0, 0 } },
                             { { 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 1, 1, 1, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 0, 0, 0, 0 } },
                             { { 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0 },
                               { 0, 0, 1, 0, 0 },
                               { 0, 0, 0, 0, 0 },
                               { 0, 0, 0, 0, 0 } },
        };
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




