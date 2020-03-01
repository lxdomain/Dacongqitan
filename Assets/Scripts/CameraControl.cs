using UnityEngine;

/// <summary>
/// This script is used to control CameraFOV through Mouse like scene panel.
/// <summary>

public class CameraControl : MonoBehaviour
{
    Vector3 offest;
    Vector3 target;

    Vector3 preV3;

    float x = 0.0f;
    float y = 0.0f;

    float distance = 15.0f;
    readonly float xSpeed = 250.0f;
    readonly float ySpeed = 120.0f;
    readonly float Speed = 40.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        target = new Vector3(0, 0, 0);
    }

    void LateUpdate()
    {
        // Rotate CameraFOV when press and drag left mouse button.(per frame)
        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target;

            transform.rotation = rotation;            
            transform.position = position;
        }
        // Move CameraFOV when press and drag right mouse button.(per frame)
        else if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.Translate(new Vector3(-x, -y, 0) * Time.deltaTime * Speed);
        }

        // Record previous v3 for the first time.  
        if (Input.GetMouseButtonDown(1))
        {
            preV3 = transform.position;
        }
        if (Input.GetMouseButtonUp(1))
        {        
            // Use Ray to detect the collider in the center of the screen. 
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // If true, set it as target.
                if (hit.collider)
                {
                    target = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    distance = (hit.point - transform.position).magnitude;
                    //print(hit.collider.name);
                }
            }
            // If false, use offset to redirect the target.
            else
            {
                offest = transform.position - preV3;
                target += offest;
                distance = (target - transform.position).magnitude;
            }
        }
    }

}
