using UnityEngine;
using static GameManager;
public class StartGate : MonoBehaviour
{
    public static event TimerEvent StartRace;
 public void OnTriggerEnter(Collider other)
 {
     if (other.tag.Equals("Player"))
     {
         StartRace.Invoke();
     }
 }
}
