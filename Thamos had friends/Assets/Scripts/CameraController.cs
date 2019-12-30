using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] [Range(5, 15)] float cameraSpeed = 10;

    [SerializeField] Transform firstPosition;
    [SerializeField] Transform characters;

    [SerializeField] Transform bottomLeftCameraLimit;
    [SerializeField] Transform topRightCameraLimit;

    private Transform characterFocused;

    [SerializeField] private float currentCameraSpeed;

    private void Start()
    {
        characterFocused = characters.GetChild(0);
        currentCameraSpeed = cameraSpeed;

        Vector3 firstPos = firstPosition.position;
        firstPos.z = transform.position.z;
        transform.position = firstPos;
    }

    private void FixedUpdate()
    {
        foreach (Transform character in characters)
        {
            if (character.GetComponent<PlayerController>().getIsFocus())
            {
                characterFocused = character;
            }
        }

        if (transform.position.y > topRightCameraLimit.position.y)
            transform.position = new Vector3(transform.position.x, topRightCameraLimit.position.y, transform.position.z);

        if (transform.position.y < bottomLeftCameraLimit.position.y)
            transform.position = new Vector3(transform.position.x, bottomLeftCameraLimit.position.y, transform.position.z);

        if (transform.position.x > topRightCameraLimit.position.x)
            transform.position = new Vector3(topRightCameraLimit.position.x, transform.position.y, transform.position.z);

        if (transform.position.x < bottomLeftCameraLimit.position.x)
            transform.position = new Vector3(bottomLeftCameraLimit.position.x, transform.position.y, transform.position.z);

        trackCharacterFocused();
    }

    private void trackCharacterFocused()
    {
        Vector3 cameraToCharacter = characterFocused.position - transform.position;
        cameraToCharacter.z = 0;
        float distance = cameraToCharacter.magnitude;

        if (distance < 1)
            distance = 1;

        Vector3 toGo = Vector3.MoveTowards(transform.position, characterFocused.position, currentCameraSpeed * Time.fixedDeltaTime * distance);
        toGo.z = transform.position.z;
        transform.position = toGo;
    }

}
