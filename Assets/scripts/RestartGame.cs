using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// I don't know what to explain about this, it is just to restart the scene/game.
/// </summary>
public class RestartGame : MonoBehaviour {
    private void OnMouseDown()
    {
        Debug.Log("Test");
        SceneManager.LoadScene("Main");
    }
}
