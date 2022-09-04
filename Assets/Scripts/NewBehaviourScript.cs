using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{public GameObject username,password,error;
public GameObject  btn;
    // Start is called before the first frame update
    
    void Start()
    {Button b = btn.GetComponent<Button>();
        b.onClick.AddListener(delegate() {
            string usernameT = username.GetComponent<TMP_InputField>().text;
            string passwordT = password.GetComponent<TMP_InputField>().text;
             StartCoroutine(GetText(usernameT,passwordT));
             btn.SetActive(false);
         }); 
    }

    IEnumerator GetText(string usernames, string passwords) {
        UnityWebRequest www = UnityWebRequest.Get("https://anfaasqureshi.tech/unity/users.php?username="+usernames+"&password="+passwords);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            string text = www.downloadHandler.text;
            if(text=="inserted"){
             PlayerPrefs.SetString("username", usernames);
             SceneManager.LoadScene(1);
            }
            else if(text=="duplicate"){
               error.SetActive(true); 
               btn.SetActive(true);
            }
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
