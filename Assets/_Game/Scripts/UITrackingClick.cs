using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class UITrackingClick : MonoBehaviour
{

    public static float time = 10f;
    public Transform timer;
    public static float timeBonusLimit = 0f;

    public static int rateBonusScore = 0;
    public Transform bonusX2;
    public Transform bonusX4;
    public Transform bonusX6;
    public Transform bonusX8;
    public Transform bonusX10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
            timer.GetComponent<TextMesh>().text = time.ToString("F2");

        }

        if (timeBonusLimit > 0) {
            timeBonusLimit -= Time.deltaTime;
            // Debug.Log(timeBonusLimit);
        }

        if (timeBonusLimit <= 0f)
        {
            rateBonusScore = 0;
            // Debug.Log("Reset bonus score!");
        }

        if (rateBonusScore >= 2)
        {
            bonusX2.GetComponent<TextMesh>().text = "x2";
        }

        if (rateBonusScore >= 4)
        {
            bonusX4.GetComponent<TextMesh>().text = "x4";
        }

        if (rateBonusScore >= 6)
        {
            bonusX6.GetComponent<TextMesh>().text = "x6";
        }

        if (rateBonusScore >= 8)
        {
            bonusX8.GetComponent<TextMesh>().text = "x8";
        }

        if (rateBonusScore == 10)
        {
            bonusX10.GetComponent<TextMesh>().text = "x10";
        }

        if (rateBonusScore < 2)
        {
            bonusX2.GetComponent<TextMesh>().text = "";
            bonusX4.GetComponent<TextMesh>().text = "";
            bonusX6.GetComponent<TextMesh>().text = "";
            bonusX8.GetComponent<TextMesh>().text = "";
            bonusX10.GetComponent<TextMesh>().text = "";
        }



    }
}
