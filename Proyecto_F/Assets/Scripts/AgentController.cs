using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Position
{
    public bool choosen;
    public int speed;
    public int unique_id;
    public int PosX;
    public int PosY;
}


[System.Serializable]
public class CarsData
{

    public int Num_Agents;
    public int height;
    public int num_steps;
    public List<Position> positions;
    public int width;


}

public class AgentController : MonoBehaviour
{

    string serverURL = "http://localhost:5000";
    string initialEndpoint = "/init";

    string stepEndPoint = "/step";


    CarsData agentData;
    CarsData agentsData;

    private int height = 200;
    private int width = 3;


    List<GameObject> cars;
    List<Vector3> oldPositions;
    List<Vector3> newPositions;

    // Pause the simulation while we get the update from the server
    bool hold = false;
    int currStep = 1;
    float timer = 0.0f;
    float dt = 0.0f;

    public GameObject[] carPrefab;
    public int carsNumber, maxSteps;
    public float timeToUpdate = 1.0f;
    private int Posicion_Z = 0;


    // Start is called before the first frame update
    void Start()
    {
        agentData = new CarsData();
        oldPositions = new List<Vector3>();
        newPositions = new List<Vector3>();

        cars = new List<GameObject>();

        timer = timeToUpdate;


        /* for (int i = 0; i < 1; i++)
        {
            GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], Vector3.zero, Quaternion.identity);
            cars.Add(car);
            // Save its position in the oldPositions list
            //oldPositions.Add(car.transform.position);
            //newPositions.Add(car.transform.position);

        } */

        // StartCoroutine(InitializeServer());
    }



  IEnumerator InitializeServer(){
    WWWForm form = new WWWForm();

        form.AddField("width", width.ToString());
        form.AddField("height", height.ToString());

        UnityWebRequest www = UnityWebRequest.Post(serverURL + initialEndpoint,form);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Configuration upload complete!");
            StartCoroutine(GetagentData());
        }
  }

    // Update is called once per frame
    void Update()
    {
        if (currStep <= maxSteps || maxSteps == -1)
        {
            float t = timer / timeToUpdate;
            // Smooth out the transition at start and end
            dt = t * t * (3f - 2f * t);

            if (timer >= timeToUpdate)
            {
                timer = 0;
                hold = true;
                StartCoroutine(GetagentData());
            }

            if (!hold)
            {
                //Debug.Log("Old " + oldPositions.Count);
                //Debug.Log("New" + newPositions.Count);
                for (int s = 0; s < cars.Count; s++)
                {
                    /* Moving the cars from the old position to the new position. */
                    Vector3 interpolate = Vector3.Lerp(oldPositions[s], newPositions[s], dt);
                    cars[s].transform.localPosition = interpolate;
                    Vector3 dir = oldPositions[s] - newPositions[s];

                }
                //Move time from the last frame
                timer += Time.deltaTime;
            }
        }
    }

    IEnumerator GetagentData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/step");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Informacion Recibida");
            Debug.Log(www.downloadHandler.text);

            agentData = JsonUtility.FromJson<CarsData>(www.downloadHandler.text);

            // Show all the data received form the Console
            Debug.Log("Numero de Agentes Actual: " + agentData.Num_Agents);
            Debug.Log("Height: " + agentData.height);
            Debug.Log("Num Steps: " + agentData.num_steps);

            // Debug.Log("Posicion en x del segundo carro: " + agentData.positions[0].PosX);
            // Debug.Log("Posicion en y del segundo carro: " + agentData.positions[0].PosY);
            // Debug.Log("Velocidad del segundo carro: " + agentData.positions[0].speed);
            // Debug.Log("ID del segundo carro: " + agentData.positions[0].unique_id);
            // Debug.Log(agentData.positions[1].choosen);


            Debug.Log("Width: " + agentData.width);

            // Update the positions of the cars
            // Debug.Log("positions en x:  " + agentData.positions[1].choosen); 
            // Debug.Log("positions en y:  " + agentData.positions[0]);
            oldPositions = new List<Vector3>(newPositions);
            newPositions.Clear();

            for (int v = 0; v < agentData.positions.Count; v++)
            {
                // newPositions.Add(agentData.positions[v]);

                // Correción de la posición en x para aparecer en nuetsra carretera
                if (agentData.positions[v].PosX == 1)
                    Posicion_Z = -8;
                else if (agentData.positions[v].PosX == 2)
                    Posicion_Z = -15;
                else if (agentData.positions[v].PosX == 0)
                    Posicion_Z = 0;


                newPositions.Add(new Vector3(agentData.positions[v].PosY *5, 0, Posicion_Z));

                if (v > cars.Count)
                {

                    // GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], Vector3.zero, Quaternion.identity);

                // Correción de la posición en x para aparecer en nuetsra carretera
                    if (agentData.positions[v].PosX == 1)
                        Posicion_Z = -8;
                    else if (agentData.positions[v].PosX == 2)
                        Posicion_Z = -15;
                    else if (agentData.positions[v].PosX == 0)
                        Posicion_Z = 0;

                    // GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], new Vector3(agentData.positions[v].PosY *5 , 0, Posicion_Z), Quaternion.identity);
                    GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], new Vector3(-1 , 0, Posicion_Z), Quaternion.identity);
                    cars.Add(car);
                }
            }
            hold = false;
        }

    }


}