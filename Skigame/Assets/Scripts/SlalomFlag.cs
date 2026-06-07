using UnityEngine;
using static GameManager;

public class SlalomFlag : MonoBehaviour
{
    
private enum Direction {Left, Right};
[SerializeField] private Direction flagDirection;
private bool flagPassed = false;
public static event TimerEvent  RacePenalty;
[SerializeField] private Material nice, notnice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerPos != null &&
            PlayerController.playerPos.position.z < transform.position.z && !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerController.playerPos.position.x < transform.position.x)
            {
                passingDirection = Direction.Left;
            }

            MeshRenderer rendered = GetComponent<MeshRenderer>();

            if (passingDirection == flagDirection)
            {
                GetComponent<Renderer>().material = nice;
            }
            else
            {
                GetComponent<Renderer>().material = notnice;
                RacePenalty.Invoke();
            }

        }
    }
}
