using UnityEngine;
using System.Collections;

public class EnemyTargetModule : MonoBehaviour {
	public static bool foundTarget = false;
	public static int targetID = -1; //Target Player
	public static int stopID = -1; //Hex Tile for Enemy Unit to aim for to be adjacent to the target Player
	public static GameObject target;


}
