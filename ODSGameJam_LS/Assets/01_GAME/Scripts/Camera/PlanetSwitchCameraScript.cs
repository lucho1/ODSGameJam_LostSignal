using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSwitchCameraScript : MonoBehaviour
{
    // --- Planet Switch Variables ---
    [SerializeField]
    private float TranslationSpeed = 10.0f;
    [SerializeField]
    private float RotationSpeed = 10.0f;

    private Transform m_NewPlanetPosition;
    private bool m_SwitchPlanet = false;
    public bool InterpolationFinished = false;
    // --- ---

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        // --- Planet Switch ---
        if (m_SwitchPlanet)
        {
            if (m_NewPlanetPosition)
            {
                transform.position = Vector3.Lerp(transform.position, m_NewPlanetPosition.position, Time.deltaTime * TranslationSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, m_NewPlanetPosition.rotation, Time.deltaTime * RotationSpeed);

                float pos_d = Vector3.Distance(transform.position, m_NewPlanetPosition.position);
                float angle = Quaternion.Angle(transform.rotation, m_NewPlanetPosition.rotation);

                if (pos_d < 0.1f && Mathf.Approximately(angle, 0.0f))
                {
                    m_SwitchPlanet = false;
                    InterpolationFinished = true;
                    GameManager.PlanetList[GameManager.CurrentPlanetIndex].AttachCamera();
                }
            }
        }


    }

    public void SwitchPlanet(Transform planet_position)
    {
        m_SwitchPlanet = true;
        InterpolationFinished = false;
        m_NewPlanetPosition = planet_position;
    }
}
