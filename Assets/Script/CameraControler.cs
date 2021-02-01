﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    //Fare için
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;

    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        //Hakeret kısıtlaması
        if (Input.GetKeyDown(KeyCode.M))
            doMovement = !doMovement;
        if (!doMovement)
            return;
        // w s a d tuşları ve hareketleri
        if(Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") /*|| Input.mousePosition.y <= Screen.height - panBorderThickness*/)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") /*|| Input.mousePosition.x <= Screen.height - panBorderThickness*/)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        //mouse scroll ile zoom in ve zoom out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 *scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;


    }
}
