using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public Transform character1Transform;
    public Transform character2Transform;
    public Transform manualMovementTransform;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);

        cameraFollow.SetGetCameraFollowPositionFunc(() => playerTransform.position);
    }
}

