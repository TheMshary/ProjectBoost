using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCheats : MonoBehaviour
{
    
    CollisionHandler collisionHandlerScript;

    void Start() {
        collisionHandlerScript = gameObject.GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            collisionHandlerScript.LoadNextLevel();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            // This doesn't work. No idea why.
            collisionHandlerScript.enabled = !collisionHandlerScript.enabled;
        }
    }
}
