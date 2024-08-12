using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Defective.JSON;
using UnityEngine;

public class ApiController
{
    private static readonly HttpClient client = new();

    public async Task SetCheckpointAPI(PostCheckpoint data)
    {
        var url = "http://localhost:5000/api/hospital/player_checkpoint";
        
        JSONObject json = new(JSONObject.Type.Object);
        json.AddField("sessionId", data.sessionId);
        json.AddField("checkpointId", data.checkpointId);
        
        var jsonString = json.ToString();
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            string cleanedResponse = responseData.Replace("[", "").Replace("]", "").Replace("\"", "");
            Debug.Log(cleanedResponse);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Debug.LogError("Error: " + error);
        }
    }

    public async Task<string> CreateSessionId()
    {
        var url = "http://localhost:5000/api/hospital/create_session";
        
        JSONObject json = new(JSONObject.Type.Object);
        
        var jsonString = json.ToString();
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            responseData = responseData.Replace("[", "").Replace("]", "").Replace("\"", "");
            Debug.Log("Response: " + responseData);
            return responseData;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Debug.LogError("Error: " + error);
        }   
        return null;
    }
}
