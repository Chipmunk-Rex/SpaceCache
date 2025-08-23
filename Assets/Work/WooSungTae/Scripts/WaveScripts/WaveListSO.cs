using UnityEngine;

[CreateAssetMenu(fileName = "WaveListSO", menuName = "Scriptable Objects/WaveListSO")]
public class WaveListSO : ScriptableObject
{
    public WaveSO[] waves;
}
