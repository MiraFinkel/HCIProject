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
            cube.transform.localEulerAngles += new Vector3(floatDataArr["xNorm"], floatDataArr["yNorm"], floatDataArr["zNorm"]) * speed * Time.deltaTime;
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
        if (float.TryParse(dataArr[0], out xRaw))
        {
            Debug.Log("xRaw: " + xRaw);
        }
        if (float.TryParse(dataArr[1], out yRaw))
        {
            Debug.Log("yRaw: " + yRaw);
        }
        if (float.TryParse(dataArr[0], out zRaw))
        {
            Debug.Log("zRaw: " + zRaw);
        }
        if (float.TryParse(dataArr[0], out xNorm))
        {
            Debug.Log("xNorm: " + xNorm);
        }
        if (float.TryParse(dataArr[0], out yNorm))
        {
            Debug.Log("yNorm: " + yNorm);
        }
        if (float.TryParse(dataArr[0], out zNorm))
        {
            Debug.Log("zNorm: " + zNorm);
        }
        Dictionary<string, float> floatDataArr = new Dictionary<string, float>();
        floatDataArr.Add("xRaw", xRaw);
        floatDataArr.Add("yRaw", yRaw);
        floatDataArr.Add("zRaw", zRaw);
        floatDataArr.Add("xNorm", xRaw);
        floatDataArr.Add("yNorm", yNorm);
        floatDataArr.Add("zNorm", zNorm);
        return floatDataArr;
    }
}
