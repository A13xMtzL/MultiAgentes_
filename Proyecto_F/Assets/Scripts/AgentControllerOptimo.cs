using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* This class is used to store the position of the car and the speed of the car. */
[System.Serializable]
public class Position
{
    public bool choosen;
    public int speed;
    public int unique_id;
    public int PosX;
    public int PosY;
}


/* A class that contains the information of the cars. */
[System.Serializable]
public class AgentData
{
    public int Num_Agents;
    public int height;
    public int num_steps;
    public List<Position> positions;
    public int width;
}

public class AgentController : MonoBehaviour
{

    // -------------------
    public GameObject[] carPrefab;
    public int maxSteps;
    public float timeToUpdate = 1.0f;
    private int Posicion_Z = 0;
    // -------------------


    string ServerUrl = "http://localhost:5000";
    string stepEndPoint = "/step2";


    AgentData agentData;

    List<GameObject> cars;
    List<Vector3> OldPositions;
    List<Vector3> NewPositions;

    // Hace una pequeña pausa en la simulación y obtenemos la información actualizada del servidor 
    bool pause = false;
    int CurrentStep = 1;
    float timer = 0.0f;
    float dt = 0.0f;

    


    // Start is called before the first frame update
    void Start()
    {
        agentData = new AgentData();
        OldPositions = new List<Vector3>();
        NewPositions = new List<Vector3>();

        cars = new List<GameObject>();

        timer = timeToUpdate;
        StartCoroutine(InitializeServer());
    }

    /// <summary>
    /// Envía una solicitud al servidor para reiniciarlo y 
    /// obtener el estado inicial del entorno.
    /// </summary>
    IEnumerator InitializeServer()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest www = UnityWebRequest.Get(ServerUrl + "/reset");
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Restarting the server...");
            StartCoroutine(GetagentData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Mueve los autos de la posición anterior a la nueva posición. */        
        if (CurrentStep <= maxSteps || maxSteps == -1)
        {
            float t = timer / timeToUpdate;
            dt = t * t * (3f - 2f * t);

            if (timer >= timeToUpdate)
            {
                timer = 0;
                pause = true;
                StartCoroutine(GetagentData());
            }

            if (!pause)
            {

                for (int s = 0; s < cars.Count; s++)
                {
                    /* Moving the cars from the old position to the new position. */
                    Vector3 interpolate = Vector3.Lerp(OldPositions[s], NewPositions[s], dt);
                    cars[s].transform.localPosition = interpolate;
                    Vector3 dir = OldPositions[s] - NewPositions[s];
                    cars[s].transform.rotation = Quaternion.LookRotation(dir);

                }
                //Move time from the last frame
                timer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Obtiene los datos del servidor y luego instancia un auto nuevo para cada auto nuevo que aparece en
    /// servidor.
    /// </summary>
    IEnumerator GetagentData()
    {
        UnityWebRequest www = UnityWebRequest.Get(ServerUrl + stepEndPoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Informacion Recibida");
            Debug.Log(www.downloadHandler.text);

            agentData = JsonUtility.FromJson<AgentData>(www.downloadHandler.text);

            // Show all the data received from the Console
            Debug.Log("Numero de Agentes Actual: " + agentData.Num_Agents);
            Debug.Log("Height: " + agentData.height);
            Debug.Log("Num Steps: " + agentData.num_steps);



            Debug.Log("Width: " + agentData.width);

            /* Se copian la lista de las nuevas posicones a la lista de las viejas posiciones y 
            luego se vacía la lista de las nuevas posiciones */
            OldPositions = new List<Vector3>(NewPositions);
            NewPositions.Clear();

            /* Crear un auto nuevo por cada auto que se encuentre en el servidor.*/
            for (int v = 0; v < agentData.positions.Count; v++)
            {
                // Correción de la posición en x para aparecer en nuetsra carretera
                if (agentData.positions[v].PosX == 1)
                    Posicion_Z = -8;
                else if (agentData.positions[v].PosX == 2)
                    Posicion_Z = -15;
                else if (agentData.positions[v].PosX == 0)
                    Posicion_Z = 0;

                /* Se añade la nueva posición del carro a la lista de nuevas posicones */
                NewPositions.Add(new Vector3(agentData.positions[v].PosY * 5, 0, Posicion_Z));

                /* Si se genera un nuevo carro en el servidor, se instancia en la simulación */
                if (v > cars.Count)
                {
                    // Correción de la posición en x para aparecer en nuestra carretera
                    if (agentData.positions[v].PosX == 1)
                        Posicion_Z = -8;
                    else if (agentData.positions[v].PosX == 2)
                        Posicion_Z = -15;
                    else if (agentData.positions[v].PosX == 0)
                        Posicion_Z = 0;

                  /* Se instancia un automóvil aleatorio de la lista 
                    carPrefab y se agrega a la lista de automóviles. */
                    GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], new Vector3(-2, 0, Posicion_Z), Quaternion.identity);
                    cars.Add(car);
                }
            }
            pause = false;
        }
    }
}