using System.IO;
using System.Net;
using UnityEngine;

public class APIHelper : MonoBehaviour
{


    public static Agents GetAgentData()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:5000/step");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<Agents>(json);
                // return JsonUtility.FromJson<Agents>(json);



    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
