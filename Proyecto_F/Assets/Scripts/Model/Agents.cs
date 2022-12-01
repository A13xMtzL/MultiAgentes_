/* Path: http://127.0.0.1:5000/step
Json Esquema:
{
  "Numero de Agentes": 8,
  "height": 200,
  "initial_population": 30,
  "num_steps": 6,
  "positions": [
    {
      "position": {
        "Posicion en x: ": 1,
        "Posicion en y: ": 43,
        "choosen": false,
        "speed": 1
      },
      "unique_id": 0
    },
    {
      "position": {
        "Posicion en x: ": 1,
        "Posicion en y: ": 42,
        "choosen": false,
        "speed": 1
      },
      "unique_id": 1
    },
    {
      "position": {
        "Posicion en x: ": 1,
        "Posicion en y: ": 37,
        "choosen": false,
        "speed": 1
      },
      "unique_id": 2
    }
  ],
  "width": 3
} */

using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{

    public int Numero_de_Agentes;
    public int height;
    public int initial_population;
    public int num_steps;
    public List<Vector3> positions;
    public int width;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
