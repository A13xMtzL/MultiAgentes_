public class CarManager : MonoBehaviour
{
    public Transform[] m_SpawPoints;  // Punto de partida del jugador 
    public GameObject[] m_CarPrefab;
    //public int waveCount;
    //public int wave1;
    //public bool spawning;
    private float timer = 0;


    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 3.0f;
    public int enemyCount;
    public GameObject enemy;
    bool waveIsDone = true;


    private void Start()
    {
        SpawnNewCar();
    }

    IEnumerator waveSpawner()
    {

        waveIsDone = false;

        for (int i = 0; i < 30; i++)
        {
            SpawnNewCar();
            yield return new WaitForSeconds(5);

        }
        yield return new WaitForSeconds(5);
        waveIsDone = true;
    }

    public static async void DoActionAfterSecondsAsync(Action action, float seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action?.Invoke();
    }

    void Awake()
    {
        timer = Time.time + 5;
    }



    //------------------------
    void DoDelaySpawn(float delayTime)
    {
        StartCoroutine(DelaySpawn(delayTime));
    }

    IEnumerator DelaySpawn(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        SpawnNewCar();
    }


    //------------------------

    void Update()
    {
        //spawning = true;
        // Activar texto de nueva ronda
        //yield return new WaitForSeconds(7);
        // Desactivar texto de nueva ronda
        //for (int i = 0; i < 10; i++)
        //{
        //await Task.Delay(5000);


        //if (waveIsDone == true)
        //{
        //    SpawnNewCar();
        //}


        //yield return new WaitForSeconds(7);
        //}

    }
    void SpawnNewCar()
    {

        //await Task.Delay(TimeSpan.FromSeconds(5));
        //int RandomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, m_SpawPoints.Length - 1));
        int RandomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0, 3));
        int RandomSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0f, m_CarPrefab.Length - 1));


        Instantiate(m_CarPrefab[RandomSpawn], m_SpawPoints[RandomNumber].transform.position, Quaternion.identity);
        DoDelaySpawn(1);


    }


}
