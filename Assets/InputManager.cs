using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Linq;

public class InputManager : MonoBehaviour
{
    SerialPort sp;
    private float spawn = 0f;
    private float x = 0f;
    private float y = 0f;
    private float z = 0f;


    void Start()
    {
        //sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One); //Replace "COM4" with whatever port your Arduino is on.
        //sp.DtrEnable = false; //Prevent the Arduino from rebooting once we connect to it. 
        //                      //A 10 uF cap across RST and GND will prevent this. Remove cap when programming.
        //sp.ReadTimeout = 1; //Shortest possible read time out.
        //sp.WriteTimeout = 1; //Shortest possible write time out.
        //sp.Open();
        //if (sp.IsOpen)
        //    sp.Write("Hello World");
        //else
        //    Debug.LogError("Serial port: " + sp.PortName + " is unavailable");
        //Removed the sp.Close line since we're now polling data.
    }

    void Update()
    {
        CheckForRecievedData();

        if (Input.GetKeyDown(KeyCode.Escape) && sp.IsOpen)
            sp.Close();
    }

    public void CheckForRecievedData()
    {
        try //Sometimes malformed serial commands come through. We can ignore these with a try/catch.
        {
            string inData = sp.ReadLine();
            // int inSize = inData.Count();
            string[] angles = inData.Split(',');
            Debug.Log("angles: " + angles[0] + ", " + angles[1] + ", " + angles[2] + ", " + angles[3]);
            spawn = float.Parse(angles[0]);
            x = float.Parse(angles[1]);
            z = float.Parse(angles[2]);
            y = float.Parse(angles[3]);
            //Debug.Log("spawn:" + spawn);
            //Debug.Log("x:" + x);
            //Debug.Log("y:" + y);
            //Debug.Log("z:" + z);

            //cube.transform.eulerAngles += new Vector3(x, z, y) * 10f * Time.deltaTime;
            //if (inData.Equals("PRESSED"))
            //{
            //Debug.Log("ARDUINO->|| " + inData + " ||MSG SIZE:" + inSize.ToString());
            //}
            //Got the data. Flush the in-buffer to speed reads up.
            //inSize = 0;
            sp.BaseStream.Flush();
            sp.DiscardInBuffer();
            //return new Vector4(spawn, x, z, y);
        }
        catch { /*return new Vector4(0f, 0f, 0f, 0f)*/; }
    }

    public Vector4 GetData()
    {
        return new Vector4(spawn, x, y, z);
    }
}