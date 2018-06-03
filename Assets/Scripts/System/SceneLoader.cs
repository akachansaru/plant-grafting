using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    // UNDONE Make the scene load with a smooth dimming effect for switching scenes
    public void LoadScene(string sceneName) {
        GlobalControl.Instance.Save();
        GlobalControl.Instance.Load(); // TODO: do I need to load here? Added save recently
        SceneManager.LoadScene(sceneName);
    }
}
