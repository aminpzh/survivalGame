using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public DayNightParams gameParameters;
    //can delete this
    private void Awake()
    {
        
        GetComponent<DayAndNightCycler>().enabled = gameParameters.enableDayAndNightCycle;
    }
}
