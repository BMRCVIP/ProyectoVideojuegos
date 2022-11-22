using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Nivel2Controller : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public int score = 0;
    public int lives = 3;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        Debug.Log("File.Exists(filePath)" + File.Exists(filePath));

        if (File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Live = lives;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if (File.Exists(filePath))
            file = File.OpenRead(filePath);
        else
        {
            Debug.LogError("No see encontro archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        //usar datos guardados
        score = data.Score;
        lives = data.Live;

        GanarPuntos(0);
    }
    public void ReiniciarSave()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if (File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 0;
        data.Live = 3;
        data.Bonus = false;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Reiniciado");
    }
    
    public void PonerMonedas()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if (File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 50;
        data.Live = 3;
        data.Bonus = false;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Reiniciado");
    }
    public void GanarPuntos(int puntos)
    {
        score += puntos;
        PrintLivesInScreen();
        PrintScoreInScreen();
    }
    public void PerderVida()
    {
        lives -= 1;
        PrintLivesInScreen();
    }
    public void GanarVida()
    {
        lives += 1;
        PrintLivesInScreen();
    }
    public void PrintScoreInScreen()
    {
        scoreText.text = "Puntaje: " + score;
    }
    public void PrintLivesInScreen()
    {
        livesText.text = "Vidas: " + lives;
    }
}

