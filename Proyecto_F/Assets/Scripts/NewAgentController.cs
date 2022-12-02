using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Positions
{
    public bool choosen;
    public int speed;
    public int unique_id;
    public int PosX;
    public int PosY;
}

[Serializable]
public class AgentsData
{
    public int Num_Agents;
    public int height;
    public int num_steps;
    public List<Positions> positions;
    public int width;

    public AgentsData() => this.positions = new List<Positions>();
}

public class NewAgentController : MonoBehaviour
{
    string serverUrl = "http://localhost:5000";
    // string updateEndpoint = "/update";


    AgentsData agentsData;
    Dictionary<int, GameObject> agents;
    Dictionary<int, Vector3> OldPositions, NewPositions;

    bool updated = false, started = false;

    public int uniqueId;
    public GameObject[] agentPrefab;
    // public int NAgents, width, height;
    public float timeToUpdate = 5.0f;
    private float timer, dt;

    void Start()
    {
        agentsData = new AgentsData();


        OldPositions = new Dictionary<int, Vector3>();
        NewPositions = new Dictionary<int, Vector3>();

        agents = new Dictionary<int, GameObject>();



        timer = timeToUpdate;

        for (int i = 0; i < 1; i++)
        {

            GameObject car = Instantiate(agentPrefab[UnityEngine.Random.Range(0, agentPrefab.Length)], Vector3.zero, Quaternion.identity);
            //cars.Add(car);
            // Save its position in the oldPositions list
            //oldPositions.Add(car.transform.position);

            // StartCoroutine(SendConfiguration());
        }


    }

    private void Update()
    {
        if (timer < 0)
        {
            timer = timeToUpdate;
            updated = false;
        }
        StartCoroutine(GetAgentsData());

        if (updated)
        {
            timer -= Time.deltaTime;
            dt = 1.0f - (timer / timeToUpdate);

            //foreach (var agent in NewPositions)
            //{
            //    Vector3 currentPosition = agent.Value;
            //    Vector3 previousPosition = OldPositions[agent.Key];

            //    Vector3 interpolated = Vector3.Lerp(previousPosition, currentPosition, dt);
            //    Vector3 direction = currentPosition - interpolated;

            //    agents[agent.Key].transform.localPosition = interpolated;
            //    if (direction != Vector3.zero) agents[agent.Key].transform.rotation = Quaternion.LookRotation(direction);
            //}

            float t = (timer / timeToUpdate);
            dt = t * t * (3f - 2f * t);
        }
    }

    //IEnumerator UpdateSimulation()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get(serverUrl + updateEndpoint);
    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //        Debug.Log(www.error);
    //    else
    //    {
    //        StartCoroutine(GetAgentsData());
    //    }
    //}


    IEnumerator GetAgentsData()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/step");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
        {
            Debug.Log("Informacion Recibida");
            Debug.Log(www.downloadHandler.text);
            agentsData = JsonUtility.FromJson<AgentsData>(www.downloadHandler.text);

            Debug.Log("Numero de Agentes Actual: " + agentsData.Num_Agents);
            Debug.Log("Height: " + agentsData.height);
            Debug.Log("Num Steps: " + agentsData.num_steps);

            //Debug.Log("Posicion en x del segundo carro: " + agentsData.positions[1].PosX);
            //Debug.Log("Posicion en y del segundo carro: " + agentsData.positions[1].PosY);
            //Debug.Log("Velocidad del segundo carro: " + agentsData.positions[1].speed);
            //Debug.Log("ID del segundo carro: " + agentsData.positions[1].unique_id);
            //Debug.Log(agentsData.positions[1].choosen);

            //foreach (Positions agent in agentsData.positions)
            //{
            //   if (!agents.ContainsKey(agent.unique_id))
            //   {
            //       GameObject newAgent = Instantiate(agentPrefab[UnityEngine.Random.Range(0, agentPrefab.Length)], new Vector3(agent.PosY, 0, agent.PosX), Quaternion.identity);
            //       agents.Add(agent.unique_id, newAgent);
            //   }
            //   else
            //   {
            //       OldPositions[agent.unique_id] = NewPositions[agent.unique_id];
            //       NewPositions[agent.unique_id] = new Vector3(agent.PosX, 0, agent.PosY);
            //   }
            //}

            foreach (Positions agent in agentsData.positions)
            {
                // new Vector3(agent.x, agent.y, agent.z);
                // new Vector3(agent.PosY, 0, agent.PosX)
                Vector3 newAgentPosition = new Vector3(agent.PosY, 0, agent.PosX);

                if (!started)
                {
                    OldPositions[agent.unique_id] = newAgentPosition;
                    agents[agent.unique_id] = Instantiate(agentPrefab[UnityEngine.Random.Range(0, agentPrefab.Length)], newAgentPosition, Quaternion.identity);
                }
                else
                {
                    Vector3 currentPosition = new Vector3();
                    if (NewPositions.TryGetValue(agent.unique_id, out currentPosition))
                        OldPositions[agent.unique_id] = currentPosition;
                    NewPositions[agent.unique_id] = newAgentPosition;
                }
            }


            //for (int v = 0; agentsData.positions.Count > 0; v++)
            //{
            //    Vector3 position = new Vector3(agentsData.positions[v].PosY, 0, agentsData.positions[v].PosX);

            //    if (!started)
            //    {
            //        OldPositions[agentsData.positions[v].unique_id] = position;
            //        agents[agentsData.positions[v].unique_id] = Instantiate(agentPrefab[UnityEngine.Random.Range(0, agentPrefab.Length)], position, Quaternion.identity);
            //    }
            //    else
            //    {
            //        Vector3 currentPosition = new Vector3();
            //        if (NewPositions.TryGetValue(agentsData.positions[v].unique_id, out currentPosition))
            //            OldPositions[agentsData.positions[v].unique_id] = currentPosition;
            //        NewPositions[agentsData.positions[v].unique_id] = position;
            //    }



            //}

            updated = true;
            if (!started) started = true;
        }
    }
}
