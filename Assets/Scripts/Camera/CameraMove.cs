using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{
    //Serializeds
    [SerializeField] private SlingShot SlingShot;
    [SerializeField] private float dragSpeed = 0.01f;

    //Privates
    private float timeDragStarted;
    private Vector3 previousPosition = Vector3.zero;

    //Refs
    Vector3 input;

    float deltaX;
    float deltaY;

    float newX;
    float newY;

    void Update()
    {
        if (SlingShot.SlingshotState == SlingshotState.Idle && GameManager.CurrentGameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InitMove();
            }
            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
            {
                Moving();
            }
        }
    }

    void InitMove() 
    {
        timeDragStarted = Time.time;

        previousPosition = Input.mousePosition;
    }

    void Moving() 
    {
        input = Input.mousePosition;

        //Get input value
        deltaX = (previousPosition.x - input.x) * dragSpeed;
        deltaY = (previousPosition.y - input.y) * dragSpeed;

        //Clamp values
        newX = Mathf.Clamp(transform.position.x + deltaX, 0, 13.36336f);
        newY = Mathf.Clamp(transform.position.y + deltaY, 0, 2.715f);

        //Move camera
        transform.position = new Vector3(newX, newY, transform.position.z);

        previousPosition = input;
    }
}
