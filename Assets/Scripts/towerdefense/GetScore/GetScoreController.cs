using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;


namespace towerdefense
{
    public class GetScoreController : MonoBehaviour
    {

        public void Execute(string username, Action<ScoreModel> OnCallback)
        {
            StartCoroutine(SendRequest(username, OnCallback));
        }

        private IEnumerator SendRequest(string username,  Action<ScoreModel> OnCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username); 

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Examen-promedio-3/get_score.php", form))
            {
                yield return www.SendWebRequest();

                if(www.result==UnityWebRequest.Result.ProtocolError 
                    || www.result==UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Error!");
                }
                else
                {
                    OnCallback?.Invoke(JsonUtility.FromJson<ScoreModel>(www.downloadHandler.text));
                }
            }
        }

    }

}