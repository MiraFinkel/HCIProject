﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    int[,] data;
    int[,,] data3;

    [SerializeField] private int dataScale = 9;
    [SerializeField] private int span = 1;
    [SerializeField] private double prob = 0.5;

    public VoxelData(int numOfsquers, bool isItSun, bool isItSpaceship)
    {
        if(isItSun)
        {
            data3 = GetSunData();
        }
        else if(isItSpaceship)
        {
            data3 = GetSpaceShipData();
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
        System.Random r = new System.Random();
        int counter = 0;
        int[,,] binaryArr = new int[dataScale, dataScale, dataScale];

        int center = (int)(dataScale / 2);
        for (int m = 0; m < (int)(1 / prob); m++)
        {
            for (int k = 0; k < (dataScale - center); k++)
            {
                for (int i = (center - k); i <= (center + k); i += span)
                {
                    for (int j = (center - k); j <= (center + k); j += span)
                    {
                        for (int l = (center - k); l <= (center + k); l += span)
                        {
                            if (counter == numberOfCubes) break;
                            if (binaryArr[i, j, l] == 1) break;
                            int num = GetProbSample(numberOfCubes, r);
                            binaryArr[i, j, l] = num;
                            if (num == 1) counter++;
                        }
                    }
                }
            }
        }
        return binaryArr;
    }

    int GetProbSample(int numberOfCubes, System.Random r)
    {
        //double prob = numberOfCubes / (Math.Pow(dataScale, 3));
        double num = r.NextDouble();
        if (num < prob)
        {
            return 1;
        }
        return 0;
    }
    public int[,,] GetSunData()
    {
        return new int[,,] { { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
                               { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} },
                             { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                               { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0} }


        };
    }

    public int[,,] GetSpaceShipData()
    {
        return new int[,,]
        {
            { {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0} },
            { {0, 0, 0, 0, 0},
              {0, 0, 1, 0, 0},
              {0, 0, 1, 0, 0},
              {0, 0, 1, 0, 0},
              {0, 0, 1, 0, 0} },
            { {0, 0, 1, 0, 0},
              {0, 1, 1, 1, 0},
              {1, 1, 1, 1, 1},
              {1, 0, 0, 0, 1},
              {1, 0, 0, 0, 1} },
            { {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 1, 0, 1, 0},
              {0, 1, 0, 1, 0},
              {0, 0, 0, 0, 0} },
            { {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 0, 0, 0} },
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




