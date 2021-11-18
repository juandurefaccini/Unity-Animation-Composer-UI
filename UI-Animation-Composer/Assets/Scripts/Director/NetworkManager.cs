using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

// Esta estructura ayuda a crear el objeto Json para ser enviado al servidor rasa
public class PostMessageJson
{
    public string message;

}
/*
[Serializable]
// RootReceiveMessageJson extrae las multiples respuestas que el bot puede dar
// Se usa para extraer multiples objetos json anidados dentro de un valor
public class RootReceiveMessageJson
{
    public ReceiveMessageJson[] messages;
}
*/
[Serializable]
public class Custom
{
    public String text;
}

[Serializable]
// ReceiveMessageJson extrae el valor del objeto json que se recibe del servidor rasa
// Se usa para extraer un solo mensaje devuelto por el bot
public class ReceiveMessageJson
{
    //public Custom custom;
    public string text;
}

public class NetworkManager : MonoBehaviour
{
    //public ChatManager chatManager;
    private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";
    //public AnimationManager animationManager;


    public void SendMessageToRasa(string oracion)
    {
        // Va a ser llamado cuando el usuario presiona el botón de enviar mensaje
        // Creo un JSON para representar el mensaje del usuario
        PostMessageJson postMessage = new PostMessageJson
        {
            message = oracion
        };

        string jsonBody = JsonUtility.ToJson(postMessage);

        // Creo una petición POST con los datos a enviar al servidor Rasa
        StartCoroutine(PostRequest(rasa_url, jsonBody));
    }

    private IEnumerator PostRequest(string url, string jsonBody)
    {
        // Va a crear una petición POST asíncrona al servidor Rasa y obtener la respuesta.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        ReceiveMessageJson recieveMessages = JsonUtility.FromJson<ReceiveMessageJson>("{\"messages\":" + request.downloadHandler.text + "}");

        Debug.Log(recieveMessages.text);

        //animationManager.AnimateCharacter(vector, receiver);
    }
}
