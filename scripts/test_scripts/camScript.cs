using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    [Range(-2.5f, 2.5f)] public float scroll;
    public Transform startPos;
    public Transform endPos;
    public Transform camPos;
    public GameObject mainCamera;
    public bool playing;
    public GameObject FirstPSCam;
    public GameObject ThirdPSCam;
    public GameObject TPStoggleCam;
    public Transform forwardPos;
    public bool cam1;
    public bool cam2;
    public bool camToggle;
    public bool joystick;

    //camScript grabCam;
    public GameObject cam;
    bool freecam;
    public Vector3 euler_Look;
    public float mouseSensitivity = 100.0f;

    [Range(50f, 100f)]
    public float joystick_multi = 100.0f;

    public float clampAngle = 60.0f;
    public bool lookmode;
    public float rotY = 0.0f; // rotation around the up/y axis
    public float rotX = 0.0f; // rotation around the right/x axis
    public float stockx;
    public float stocky;
    public Quaternion localRotation2;
    public Quaternion localRotation;

    RaycastHit hit;
    Vector3 dir;

    // Use this for initialization

    void Start()
    {
        FirstPSCam.SetActive(true);
        ThirdPSCam.SetActive(false);
        mainCamera = FirstPSCam;
        cam1 = true;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        //grabCam = gameObject.GetComponent<camScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playing)
        {

        }
        cam = mainCamera;
        if (joystick)
        {
            
            float mouseX = Input.GetAxis("Horizontal2");
            float mouseY = Input.GetAxis("Vertical2");
            rotY += mouseY * mouseSensitivity * joystick_multi * Time.deltaTime;
            rotX += mouseX * mouseSensitivity * joystick_multi * Time.deltaTime;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;
        }


        scroll = Mathf.Clamp(scroll, -1.3f, 2.5f);
        scroll -= Input.GetAxis("Mouse ScrollWheel");
        CamToggle();
        dir = (transform.position - mainCamera.transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, 1.0f))
        {


        }
        else
        {
            //camPos.eulerAngles = new Vector3(scroll * 2.5f, camPos.eulerAngles.y, 0);
            camPos.position = startPos.position + ((endPos.position - startPos.position) * (scroll / 2.5f));
        }


        freeCam();

        //rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle / 5);
        //rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);


        //if holding right click it turns on look mode and turns off camera follow for player angle

        localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        cam.transform.rotation = localRotation;
        //when you let go of right click, it restores the angle back to the original spot when it was first pressed.
        if (Input.GetMouseButtonUp(1))
        {

            rotX = stockx;
            rotY = stocky;
            lookmode = false;
        }
    }
    public GameObject GiveCam()
    {
        return mainCamera;
    }
    public void freeCam()
    {
        if (Input.GetMouseButton(1))
        {
            // stores angle when right click happens
            if (!lookmode)
            {
                stockx = rotX;
                stocky = rotY;
                lookmode = true;
            }


        }
        //camera checks for X,Y axis changes
        //player object checks forY axis changes
        else
        {
            localRotation2 = Quaternion.Euler(0.0f, rotY, 0.0f);
            gameObject.transform.rotation = localRotation2;

        }
    }
    public void CamToggle()
    {
        if (cam1)
        {
            FirstPSCam.SetActive(true);
            ThirdPSCam.SetActive(false);
            mainCamera = FirstPSCam;
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);


        }
        else if (cam2)
        {
            FirstPSCam.SetActive(false);
            ThirdPSCam.SetActive(true);
            mainCamera = ThirdPSCam;
            if (camToggle)
            {
                // ThirdPSCam.SetActive(true);
                mainCamera = TPStoggleCam;
                mainCamera.transform.LookAt(transform.forward);
            }
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);


        }
        else
        {
            cam1 = true;
            FirstPSCam.SetActive(true);
            ThirdPSCam.SetActive(false);
            mainCamera = FirstPSCam;

        }

        if (scroll > 0)
        {
            cam2 = true;
            cam1 = false;
        }
        else if (scroll < -1)
        {
            cam2 = false;
            cam1 = true;
        }
    }
    public Camera GiveCam2()
    {
        if (mainCamera.CompareTag("MainCamera"))
        {
            return mainCamera.GetComponent<Camera>();
        }
        else
        {
            return ThirdPSCam.GetComponent<Camera>();
        }
    }
    public  float giveAxisX()
    {
        return rotX;
    }
    public float giveAxisY()
    {
        return rotY;
    }
    public void setMouseSensi(float mouse_level)
    {
        mouseSensitivity = mouse_level;

    }
}
