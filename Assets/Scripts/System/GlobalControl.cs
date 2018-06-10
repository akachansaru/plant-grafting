using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Analytics;

// Add this class to a Game Object to save and load data specified in SaveValues.cs
public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;

    public SaveValues savedValues;

    string savePath;

    void Awake() {
        if (Instance == null) {
            Debug.Log("Creating new GlobalControl");
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Debug.Log("Updating GlobalControl");
            Destroy(gameObject);
        }
        savePath = Application.persistentDataPath + "/saveValues.sheep";
        Load();
    }

    public void Update() {
        // TODO Maybe check for escape button inside SceneLoader.cs
        //if (Input.GetKey(KeyCode.Escape)) {
        //    if (SceneManager.GetActiveScene().name.Equals("MainMenu")) {
        //        // UNDONE Ask if user wants to exit app
        //        AskToQuit();
        //    } else if (SceneManager.GetActiveScene().name.Equals("LevelSelection")) {
        //        SceneManager.LoadScene("MainMenu");
        //    } else if (SceneManager.GetActiveScene().name.Equals("Customization")) {
        //        GlobalControl.Instance.Save(); // Saves the items placed on the pig
        //        SceneManager.LoadScene("LevelSelection");
        //    } else if (SceneManager.GetActiveScene().name.Equals("ItemCreation")) {
        //        SceneManager.LoadScene("Customization");
        //    } else {
        //        // UNDONE Ask if user wants to return to quit level
        //        SceneManager.LoadScene("LevelSelection");
        //    }
        //}
    }

    void AskToQuit() {
        Application.Quit();
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, savedValues);
        file.Close();
        Debug.Log("Saved to " + savePath);
    }

    public void Load() {
        if (File.Exists(savePath)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            savedValues = (SaveValues)bf.Deserialize(file);
            file.Close();
            Debug.Log("musicMuted : " + savedValues.musicMuted);
            Debug.Log("Plants saved " + savedValues.availablePlants.Count);
        } else {
            CreateNewGame();
        }
    }

    /// <summary>
    ///  Initialize save values when starting a new game.
    /// </summary>
    private void CreateNewGame() {
        // TODO Implement multiple save files
        savedValues.musicMuted = false;
        savedValues.musicVolume = 1f; // Full volume
        savedValues.availablePlants = new List<Plant>();
        Debug.Log("New game");
    }
}
