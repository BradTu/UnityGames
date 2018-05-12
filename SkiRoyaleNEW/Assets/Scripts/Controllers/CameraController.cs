///<summary>
///Brad Tully
///11 October 2017
///This controls the position of the camera it implements camera shake for when the player gets hit by objects/ poles
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The player.
    public GameObject thePlayer;
    public Player aPlayer;
    // The player sprite.
    public GameObject thePlayerSprite;
    // Keep track of sprite coordinates
    private float spriteX, spriteY;
    // Determine wheter or not camera is shaking
    public bool isShaking;
    // Using these for easing the camera left and right
    private Vector3 spritePosition, spriteRight, spriteLeft;
    // Used for easing
    private float step;
    // Used for camera shake
    public float shakeTimer, shakeAmount;

    // get sprite coord and set shaking to false
    void Start()
    {
        spriteX = thePlayerSprite.transform.position.x;
        spriteY = thePlayerSprite.transform.position.y;
        isShaking = false;
        aPlayer = thePlayer.GetComponent<Player>();
    }

    // Call all of the camera functions during updates
    void Update()
    {
        updateSpriteCoord();
        cameraShakeUpdate();
    }

    //This will allow the camera to shake for some sweet sweet juice
    public void cameraShakeUpdate()
    {
        if (isShaking == true)
        {
            //run this for x amount of time
            if (shakeTimer >= 0)
            {
                //Find position within circle, transform the position of camera, decrement the time
                Vector2 ShakePosition = Random.insideUnitCircle * shakeAmount;
                transform.position = new Vector3(transform.position.x + ShakePosition.x, transform.position.y + ShakePosition.y, -10f);
                shakeTimer -= Time.deltaTime;
            }
            //Reset bool
            else
            {
                isShaking = false;
            }
        }
    }

    //Initializes shake values and starts the shaking
    public void startShake(float shakePower, float shakeDuration)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeDuration;
        isShaking = true;
    }

    //Update the sprite coordinates just for ease of use and to have a smaller variable name
    public void updateSpriteCoord()
    {
            spriteX = thePlayerSprite.transform.position.x;
            spriteY = thePlayerSprite.transform.position.y;
            spritePosition = new Vector3(spriteX, spriteY + 7f, -10f);
            step = Mathf.Abs(aPlayer.direction) * Time.deltaTime;
            gameObject.transform.position = spritePosition;
    }

}
