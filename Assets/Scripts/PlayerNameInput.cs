using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    public InputField inputField;
    private string dbUri = "URI=file:mydb.sqlite";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string playerName = inputField.text;
            GuardarNombreJugador(playerName);
        }
    }

    private void GuardarNombreJugador(string playerName)
    {
        using (IDbConnection conexionBD = new SqliteConnection(dbUri))
        {
            conexionBD.Open();
            using (IDbCommand comandoBD = conexionBD.CreateCommand())
            {
                string consultaSQL = "INSERT INTO PlayerData (PlayerName) VALUES (@PlayerName)";
                comandoBD.CommandText = consultaSQL;
                comandoBD.Parameters.Add(new SqliteParameter("@PlayerName", playerName));
                comandoBD.ExecuteNonQuery();

                Debug.Log("Nombre del jugador guardado en la base de datos: " + playerName);
            }
        }
    }
}
