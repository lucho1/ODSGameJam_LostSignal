using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetManager : Singleton<PlanetManager>
{
    [SerializeField]
    int PosRange = 500;

    [SerializeField]
    private PlanetScript InitialPlanet;

    [SerializeField]
    private GameObject PlanetPrefab;

    [SerializeField]
    private GameObject CurrentCamera;

    private int DeadPlanets = 0;

    // Start is called before the first frame update
    void Start()
    {
        //(GameManager.PlanetList).Add(InitialPlanet); //Sergi: Now done in Start of PlanetScript
        InitialPlanet.AttachCamera();
        InitialPlanet.GetComponent<PlanetScript>().planetDeath.AddListener(Instance.OnPlanetDeath);

    }

    // Update is called once per frame
    void Update()
    {
        // --- Planet Switch ---
        if (GameManager.PlanetList.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SwitchPlanet();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SwitchPlanet(false);
        }
    }

    public static void CreatePlanet()
    {
        float pos_range = Instance.PosRange;
        Vector3 planet_pos = new Vector3(Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range));
        Vector3 planet_rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        foreach(PlanetScript planet in GameManager.PlanetList)
        {
            while(planet.GetComponent<SphereCollider>().bounds.Contains(planet_pos))
                planet_pos = new Vector3(Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range));
        }

        PlanetScript newPlanet = Instantiate(Instance.PlanetPrefab, planet_pos, Quaternion.Euler(planet_rot)).GetComponent<PlanetScript>();
        newPlanet.planetDeath.AddListener(Instance.OnPlanetDeath);
        SwitchPlanet();
    }

    public static void SwitchPlanet(bool forward = true)
    {
        if (GameManager.PlanetList.Count <= 1)
            return;

        SoundsManager.PlaySound(SoundsManager.NewPlanetSound);
        GameManager.PlanetList[GameManager.CurrentPlanetIndex].DetachCamera();
        GameObject next_planet;

        if (forward)
            next_planet = GameManager.NextPlanet().CameraPosition;
        else
            next_planet = GameManager.PreviousPlanet().CameraPosition;

        Instance.CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(next_planet.transform);
    }

    public void OnPlanetDeath() {
        ++Instance.DeadPlanets;

        if (Instance.DeadPlanets == GameManager.PlanetList.Count) {
            //myTODO Lucho: Play lose sound here
            SceneManager.LoadScene("GameOver");
        }
    }
}
