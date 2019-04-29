using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconTrigger : MonoBehaviour
{
    public AreaCondition area;
    Beacon beacon;

    void Start()
    {
        beacon = GetComponent<Beacon>();
        area.EnterArea += ActivateBeacon;
        area.ExitArea += DeactivateBeacon;
    }

    void ActivateBeacon()
    {
        beacon.ActivateBeacon();
    }

    void DeactivateBeacon()
    {
        beacon.DeactivateBeacon();
    }
}
