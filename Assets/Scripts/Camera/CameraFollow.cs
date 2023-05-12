using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    //Publics
    [HideInInspector] public Vector3 StartingPosition;
    [HideInInspector] public bool IsFollowing;

    //Privates
    private Transform BirdToFollow;

    //Const
    private const float minCameraX = 0;
    private const float maxCameraX = 13;

	void Awake()
	{
        instance = this;
    }

	void Start()
    {
        StartingPosition = transform.position;
    }

    void Update()
    {
        if (IsFollowing)
            if (BirdToFollow != null)
            {
                var birdPosition = BirdToFollow.transform.position;
                float x = Mathf.Clamp(birdPosition.x, minCameraX, maxCameraX);
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
            }
            else
                IsFollowing = false;
    }

    public void SetTarget(Transform newTarget) 
    {
        BirdToFollow = newTarget;
    }
}
