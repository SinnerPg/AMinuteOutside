using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform followTarget;
    public Vector2 xClamp, yClamp;
    public float lerpTime = 3;
    public InputManager inputManager;
    void Update()
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(followTarget.position.x, xClamp.x, xClamp.y);
        position.y = Mathf.Clamp(followTarget.position.y, yClamp.x, yClamp.y);
        position.z = transform.position.z;

        transform.position = position;

        if(inputManager.isAiming)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 240, lerpTime * Time.deltaTime);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 200, lerpTime * Time.deltaTime);
        }
    }
}
