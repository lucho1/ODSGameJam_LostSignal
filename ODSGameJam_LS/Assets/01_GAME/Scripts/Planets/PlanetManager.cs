using UnityEngine;
using UnityEngine.Events;

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

    private UnityEvent m_ChangedPlanet = new UnityEvent();
    public static UnityEvent ChangedPlanet {get {return Instance.m_ChangedPlanet;} set {Instance.m_ChangedPlanet = value;}}

    private int m_deadPlanets = 0;
    public static int DeadPlanets { get{ return Instance.m_deadPlanets;} set {Instance.m_deadPlanets = value;}}

    private bool ShowText1 = true, ShowText2 = false;
    private float Text2Time = 0.0f, Text1Time = 0.0f;

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
        if(ShowText1 && Text1Time < 1.5f)
            Text1Time += Time.deltaTime;

        if (ShowText1 && Text1Time > 1.5f)
        {
            DialogWindow.ActivateDialog(DialogWindow.Instance.GameStartText);
            ShowText1 = false;
            ShowText2 = true;
            Text1Time = 0.0f;
        }

        if (ShowText2 && Text2Time < 150)
            Text2Time += Time.deltaTime;

        if (ShowText2 && Text2Time > 150)
        {
            DialogWindow.ActivateDialog(DialogWindow.Instance.GameStartText2);
            ShowText2 = false;
            Text2Time = 0.0f;
        }



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
        ChangedPlanet.Invoke();
    }

    public void OnPlanetDeath() {
        ++DeadPlanets;

        if (DeadPlanets == GameManager.PlanetList.Count)
            GameManager.FinishGame(false);
    }
}
