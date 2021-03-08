using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Range(5, 20)]
    [SerializeField] public float r = 5;
    [Range(0f, 1f)]
    [SerializeField] public float colorNum = 0.2f;
    [Range(1, 36)]
    [SerializeField] public int numOfSquares = 1;
    [Range(0.5f, 2f)]
    [SerializeField] private float respawnTime = 1f;
    [SerializeField] private bool spawn = false;

    [SerializeField] private OrbitMotion orbitMotion;
    [SerializeField] private ShapeGenerator shapeGenerator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Canvas MainCanvas;

    [SerializeField] private GameObject paintCanvas;
    [SerializeField] private GameObject sun;

    private Color color = Color.red;
    private bool endOfGame = false;

    void Start()
    {
        paintCanvas.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKey("space"))
        {
            spawn = false;
            shapeGenerator.spawn = false;
            sun.SetActive(false);
            paintCanvas.SetActive(true);
            ExplodeAndPaint();
        }
        if (!endOfGame)
        {
            float[] data = inputManager.GetData();
            if (data[0] == 1f)
            {
                spawn = true;
            }
            else
            {
                spawn = false;
            }
            shapeGenerator.spawn = spawn;
            if (data[4] == 2f)
            {
                orbitMotion.AxisRotationUpdate(data[4]);
            } else if (data[4] == 3f)
            {
                orbitMotion.AxisRotationUpdate(data[4]);
            }
            else if (data[4] == 4f)
            {
                orbitMotion.AxisRotationUpdate(data[4]);
            }
            else if (data[4] == 5f)
            {
                spawn = false;
                shapeGenerator.spawn = false;
                sun.SetActive(false);
                paintCanvas.SetActive(true);
                ExplodeAndPaint();
                StartCoroutine(cameraManager.ShowCanvas());
            }

                colorNum = data[1] / 360;
            UpdateColor();
            shapeGenerator.color = color;

            numOfSquares = (int)(data[2] / 36) + 1;
            shapeGenerator.respawnTime = (data[3] / 240) + 0.5f;

            //shapeGenerator.respawnTime = respawnTime;

        }
        else
        {
            shapeGenerator.spawn = false;
        }
        
    }

    public void StartGame(int shape)
    {
        MainCanvas.enabled = false;
        if (shape == 1)
        {
            GameObject shapeGO = Instantiate(Resources.Load("Box")) as GameObject;
            shapeGO.transform.position = new Vector3(2.23f, 0.510f, 1.32f);
        }
        else if (shape == 2)
        {
            GameObject shapeGO = Instantiate(Resources.Load("Sphere")) as GameObject;
            shapeGO.transform.position = new Vector3(2.23f, 0.510f, 1.32f);
        }
        else
        {
            GameObject shapeGO = Instantiate(Resources.Load("Cylinder")) as GameObject;
            shapeGO.transform.position = new Vector3(2.23f, 0.510f, 1.32f);
        }
    }

    void UpdateColor()
    {
        color = Color.HSVToRGB(colorNum, 1f, 1f);
    }

    void ExplodeAndPaint()
    {
        GameObject[] allShapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (var shape in allShapes)
        {
            if(shape != null)
            {
                VoxelRenderer vR = shape.GetComponent<VoxelRenderer>();
                vR.theEndOfTheGame = true;
                vR.MoveTowordsTheSun();
            }
        }
    }
}
