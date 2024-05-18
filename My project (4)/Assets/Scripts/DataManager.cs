using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    private float prevTime;
    private float logInterval=1;
    private float prevSaveTime;
    private float logSaveInterval=5;
    private Positions playerPos;
    private Positions enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        prevTime = Time.realtimeSinceStartup;
        prevSaveTime = prevTime;
        playerPos = new Positions();
        enemyPos = new Positions();

        CharacterPosittion cp = new CharacterPosittion("Prueba", 123123123, Vector3.right);
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterPosittion));
        using (FileStream stream = new FileStream("exampleXML.xml", FileMode.Create))
        {
            serializer.Serialize(stream, playerPos);
        }
        PlayerPrefs.SetString("nombre", "MaxUser");
        Debug.Log(PlayerPrefs.GetString("nombre"));
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (currentTime > prevTime + logInterval)
        {
            prevTime+= logInterval;
            CharacterPosittion cp = new CharacterPosittion(player.name, currentTime, player.transform.position);
            playerPos.positions.Add(cp);
            foreach (GameObject enemy in enemies)
            {
                CharacterPosittion en = new CharacterPosittion(enemy.name, currentTime, enemy.transform.position);
                enemyPos.positions.Add(en);
            }

        }
        if (currentTime < prevSaveTime + logSaveInterval)
        {
            prevSaveTime += logSaveInterval;
            SaveCSVToFile();
            SaveJSONToFile();
            SaveXMLToFile();
        }
    }

    private void SaveCSVToFile()
    {
        string data = "Name; Timestamp; x; y; z\n";
        foreach (CharacterPosittion cp in playerPos.positions)
        {
            data += cp.ToCSV () + "\n";
        }
        foreach (CharacterPosittion cp in enemyPos.positions)
        {
            data += cp.ToCSV() + "\n";
        }
        FileManager.WriteToFile("positions.csv", data);
    }
    private void SaveJSONToFile() {

        string data = "";
         foreach (CharacterPosittion cp in playerPos.positions)
         {
             data += JsonUtility.ToJson(cp) + ",\n";
         }
         foreach (CharacterPosittion cp in enemyPos.positions)
         {
             data += JsonUtility.ToJson(cp) + ",\n";
         }
         data += "]";
         FileManager.WriteToFile("positions.json", data);
         FileManager.WriteToFile("playerPositions.json", JsonUtility.ToJson(playerPos));
         FileManager.WriteToFile("enemyPositions.json", JsonUtility.ToJson(enemyPos));
    }
    public void SaveXMLToFile() {
        XmlSerializer serializer = new XmlSerializer(typeof(Positions));
        using (FileStream stream = new FileStream("playerPositions.xml", FileMode.Create))
        {
            serializer.Serialize(stream, playerPos);
        }
        using (FileStream stream = new FileStream("enemyPositions.xml", FileMode.Create))
        {
            serializer.Serialize(stream, enemyPos);
        }

    }
}
