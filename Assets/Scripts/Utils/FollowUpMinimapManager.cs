using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpMinimapManager : MonoBehaviour
{
    public Transform player;
    public Vector2 xClamp, yClamp;
    void LateUpdate()
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(player.position.x, xClamp.x, xClamp.y);
        position.y = Mathf.Clamp(player.position.y, yClamp.x, yClamp.y);
        position.z = transform.position.z;

        transform.position = position;
    }
}
