using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Tools
{
    private const string URL = "https://localhost:7083/";
    private const string ENDPOINT = "message";
    private const string NO_MESSAGE_TEXT = "No message";

    private string _urlToken;

    public void OpenUrl()
    {
        SetToken();
        Application.OpenURL(URL+_urlToken);
    }
    
    public async Task<string> TryGetMessage()
    {
        var message = NO_MESSAGE_TEXT;
        while (message.Equals(NO_MESSAGE_TEXT))
        {
            await Task.Delay(30);
            message = await Get(URL + ENDPOINT + _urlToken);
        }

        return message;
    }

    private void SetToken()
        => _urlToken = $"?token={GenerateUniqueValue()}";

    private async Task<string> Get(string endpoint)
    {
        var getRequest = CreateGetRequest(endpoint);
        getRequest.SendWebRequest();

        while (!getRequest.isDone) await Task.Delay(10);
        return getRequest.downloadHandler.text;
    }
    
    private UnityWebRequest CreateGetRequest(string path, object data = null)
    {
        var request = new UnityWebRequest(path, "GET");

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }
    
    private string GenerateUniqueValue()
    {
        var result = default(byte[]);

        using (var stream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(DateTime.Now.Ticks);
            }

            stream.Position = 0;

            using (var hash = SHA256.Create())
            {
                result = hash.ComputeHash(stream);
            }
        }

        var text = new string[20];

        for (var i = 0; i < text.Length; i++)
        {
            text[i] = result[i].ToString("x2");
        }

        return string.Concat(text);
    }
}
