using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private wrmhlRead wrmhl;
    [SerializeField] private GameObject cube;
    [SerializeField] private float speed;


    void Update()
    {
        string data = wrmhl.getData();
        if (data != null)
        {
            Dictionary<string, float> floatDataArr = parseData(data);
            cube.transform.eulerAngles = new Vector3(floatDataArr["zNormAcc"], floatDataArr["xNormAcc"], floatDataArr["yNormAcc"]) * speed * Time.deltaTime;
        }
    }

    Dictionary<string, float> parseData(string data)
    {
        //Debug.Log("string" + data);
        string[] dataArr = data.Split('.');
        float xRaw;
        float yRaw;
        float zRaw;
        float xNorm;
        float yNorm;
        float zNorm;
        float xRawAcc;
        float yRawAcc;
        float zRawAcc;
        float xNormAcc;
        float yNormAcc;
        float zNormAcc;
        if (float.TryParse(dataArr[0], out xRaw))
        {
            Debug.Log("xRaw: " + xRaw);
        }
        if (float.TryParse(dataArr[1], out yRaw))
        {
            Debug.Log("yRaw: " + yRaw);
        }
        if (float.TryParse(dataArr[2], out zRaw))
        {
            Debug.Log("zRaw: " + zRaw);
        }
        if (float.TryParse(dataArr[3], out xNorm))
        {
            Debug.Log("xNorm: " + xNorm);
        }
        if (float.TryParse(dataArr[4], out yNorm))
        {
            Debug.Log("yNorm: " + yNorm);
        }
        if (float.TryParse(dataArr[5], out zNorm))
        {
            Debug.Log("zNorm: " + zNorm);
        }

        if (float.TryParse(dataArr[6], out xRawAcc))
        {
            Debug.Log("xRaw: " + xRawAcc);
        }
        if (float.TryParse(dataArr[7], out yRawAcc))
        {
            Debug.Log("yRaw: " + yRawAcc);
        }
        if (float.TryParse(dataArr[8], out zRawAcc))
        {
            Debug.Log("zRaw: " + zRawAcc);
        }
        if (float.TryParse(dataArr[9], out xNormAcc))
        {
            Debug.Log("xNorm: " + xNormAcc);
        }
        if (float.TryParse(dataArr[10], out yNormAcc))
        {
            Debug.Log("yNorm: " + yNormAcc);
        }
        if (float.TryParse(dataArr[11], out zNormAcc))
        {
            Debug.Log("zNorm: " + zNormAcc);
        }

        Dictionary<string, float> floatDataArr = new Dictionary<string, float>();
        floatDataArr.Add("xRaw", xRaw);
        floatDataArr.Add("yRaw", yRaw);
        floatDataArr.Add("zRaw", zRaw);
        floatDataArr.Add("xNorm", xRaw);
        floatDataArr.Add("yNorm", yNorm);
        floatDataArr.Add("zNorm", zNorm);
        floatDataArr.Add("xRawAcc", xRawAcc);
        floatDataArr.Add("yRawAcc", yRawAcc);
        floatDataArr.Add("zRawAcc", zRawAcc);
        floatDataArr.Add("xNormAcc", xRawAcc);
        floatDataArr.Add("yNormAcc", yNormAcc);
        floatDataArr.Add("zNormAcc", zNormAcc);
        return floatDataArr;
    }

    private void generateObject()
    {
        GameObject shapeGO = Instantiate(Resources.Load("Shape")) as GameObject;
    }
}
