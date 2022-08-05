using UnityEngine;

[CreateAssetMenu(fileName = "Parameters", menuName = "Scriptable Objects/Game Parameters", order = 10)]
public class DayNightParams : ScriptableObject
{
    public bool enableDayAndNightCycle; //enable and disable day night cycle
    public float dayLengthInSeconds; // lenght of one day (seconds)
    public float dayInitialRatio;
}