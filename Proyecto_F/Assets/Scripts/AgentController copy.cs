// /* Path: http://127.0.0.1:5000/step
// Json Esquema:
// {
//   "Num_Agents": 7,
//   "height": 200,
//   "num_steps": 6,
//   "positions": [
//     {
//       "PositionX: ": 2,
//       "PositionY: ": 30,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 0
//     },
//     {
//       "PositionX: ": 2,
//       "PositionY: ": 27,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 1
//     },
//     {
//       "PositionX: ": 0,
//       "PositionY: ": 24,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 2
//     },
//     {
//       "PositionX: ": 0,
//       "PositionY: ": 12,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 3
//     },
//     {
//       "PositionX: ": 2,
//       "PositionY: ": 9,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 4
//     },
//     {
//       "PositionX: ": 1,
//       "PositionY: ": 0,
//       "choosen": false,
//       "speed": 3,
//       "unique_id": 5
//     }
//   ],
//   "width": 3
// }
// */

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// [System.Serializable]
// public class Position
// {
//     public bool choosen;
//     public int speed;
//     public int unique_id;
//     public int PosX;
//     public int PosY;
// }


// [System.Serializable]
// public class CarsData
// {

//     public int Num_Agents;
//     public int height;
//     public int num_steps;
//     public List<Position> positions;
//     public int width;


// }

// public class AgentController : MonoBehaviour
// {

//     string serverURL = "http://localhost:5000";

//     string stepEndPoint = "/step";


//     CarsData agentData;
//     CarsData agentsData;


//     List<GameObject> cars;
//     List<Vector3> oldPositions;
//     List<Vector3> newPositions;

//     // Pause the simulation while we get the update from the server
//     bool hold = false;
//     int currStep = 1;
//     float timer = 0.0f;
//     float dt = 0.0f;

//     public GameObject[] carPrefab;
//     public int carsNumber, maxSteps;
//     public float timeToUpdate = 1.0f;






//     /* List<List<Vector3>> positions;

//     public GameObject agent1Prefab;
//     public GameObject agent2Prefab;
//     public GameObject agent3Prefab;
//     public GameObject agent4Prefab;
//     public GameObject agent5Prefab;

//     // Using the data in json format to create the agents and update the position of the vehicles in the simulation in Unity
//     public void UpdateAgents(string json)
//     {

//         HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:5000/step");
//         HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//         StreamReader reader = new StreamReader(response.GetResponseStream());

//         string json = reader.ReadToEnd();

//         return JsonUtility.FromJson<CarsData>(json);

//     }
//   */


//     // Start is called before the first frame update
//     void Start()
//     {
//         agentData = new CarsData();
//         oldPositions = new List<Vector3>();
//         newPositions = new List<Vector3>();

//         cars = new List<GameObject>();

//         timer = timeToUpdate;

//         for (int i = 0; i < 1; i++)
//         {
//             GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], Vector3.zero, Quaternion.identity);
//             cars.Add(car);
//             // Save its position in the oldPositions list
//             oldPositions.Add(car.transform.position);

//             // StartCoroutine(SendConfiguration());
//         }

//     }



//     // Update is called once per frame
//     void Update()
//     {
//         if (currStep <= maxSteps || maxSteps == -1)
//         {
//             float t = timer / timeToUpdate;
//             // Smooth out the transition at start and end
//             dt = t * t * (3f - 2f * t);

//             if (timer >= timeToUpdate)
//             {
//                 timer = 0;
//                 hold = true;
//                 StartCoroutine(GetagentData());
//             }

//             if (!hold)
//             {
//                 //for (int s = 0; s < cars.Count; s++)
//                 //{
//                 //   Vector3 interpolated = Vector3.Lerp(oldPositions[s], newPositions[s], dt);
//                 //   cars[s].transform.localPosition = interpolated;

//                 //   Vector3 dir = oldPositions[s] - newPositions[s];
//                 //   cars[s].transform.rotation = Quaternion.LookRotation(-dir);


//                 //}
//                 //Move time from the last frame
//                 timer += Time.deltaTime;
//             }
//         }
//     }

//     //IEnumerator UpdateSimulation()
//     //{
//     //    UnityWebRequest www = UnityWebRequest.Get(serverUrl + updateEndpoint);
//     //    yield return www.SendWebRequest();
 
//     //    if (www.result != UnityWebRequest.Result.Success)
//     //        Debug.Log(www.error);
//     //    else 
//     //    {
//     //        StartCoroutine(GetAgentsData());
//     //    }
//     //}

//     IEnumerator GetagentData()
//     {
//         UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/step");
//         yield return www.SendWebRequest();

//         if (www.result != UnityWebRequest.Result.Success)
//         {
//             Debug.Log(www.error);
//         }
//         else
//         {
//             Debug.Log("Informacion Recibida");
//             Debug.Log(www.downloadHandler.text);

//             agentData = JsonUtility.FromJson<CarsData>(www.downloadHandler.text);
            
//             // Show all the data received form the Console
//             Debug.Log("Numero de Agentes ACtual: " + agentData.Num_Agents);
//             Debug.Log("Height: " + agentData.height);
//             Debug.Log("Num Steps: " + agentData.num_steps);

//             Debug.Log("Posicion en x del segundo carro: " + agentData.positions[0].PosX);
//             Debug.Log("Posicion en y del segundo carro: " + agentData.positions[0].PosY);
//             Debug.Log("Velocidad del segundo carro: " + agentData.positions[0].speed);
//             Debug.Log("ID del segundo carro: " + agentData.positions[0].unique_id);
//             Debug.Log(agentData.positions[1].choosen);
            

//             Debug.Log("Width: " + agentData.width);

//             // Update the positions of the cars
//             // Debug.Log("positions en x:  " + agentData.positions[1].choosen); 
//             // Debug.Log("positions en y:  " + agentData.positions[0]);
//             oldPositions = new List<Vector3>(newPositions);
//             newPositions.Clear();
//             for (int v = 0; v < agentData.positions.Count; v++)
//             {
//                 // newPositions.Add(agentData.positions[v]);
//                 newPositions.Add(new Vector3(agentData.positions[v].PosX, agentData.positions[v].PosY, 0));
//                 if (v > cars.Count)
//                 {
//                     // ! GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], Vector3.zero, Quaternion.identity);
//                     GameObject car = Instantiate(carPrefab[UnityEngine.Random.Range(0, carPrefab.Length)], new Vector3(agentData.positions[v].PosY, 0, agentData.positions[v].PosX), Quaternion.identity);
//                     cars.Add(car);
//                 }
//             }
//             hold = false;
//         }

//     }

//     /* IEnumerator _GetAgentsData() 
//     {
//         UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/step");
//         yield return www.SendWebRequest();
 
//         if (www.result != UnityWebRequest.Result.Success)
//             Debug.Log(www.error);
//         else 
//         {
//             agentsData = JsonUtility.FromJson<AgentsData>(www.downloadHandler.text);

//             foreach(AgentData agent in agentsData.positions)
//             {
//                 Vector3 newAgentPosition = new Vector3(agent.x, agent.y, agent.z);

//                     if(!started)
//                     {
//                         prevPositions[agent.id] = newAgentPosition;
//                         agents[agent.id] = Instantiate(agentPrefab, newAgentPosition, Quaternion.identity);
//                     }
//                     else
//                     {
//                         Vector3 currentPosition = new Vector3();
//                         if(currPositions.TryGetValue(agent.id, out currentPosition))
//                             prevPositions[agent.id] = currentPosition;
//                         currPositions[agent.id] = newAgentPosition;
//                     }
//             }

//             updated = true;
//             if(!started) started = true;
//         }
//     } */

  

// }