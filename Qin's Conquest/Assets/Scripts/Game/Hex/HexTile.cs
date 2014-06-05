using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile : MonoBehaviour {

	public bool taken = false;

	public Field field;
	public int iD;

	//Static array table for picking directions
	//must add one to array index since this assumes negative indices
	static int[,] DIRS = new int[3,4]{ {-1,-1,-1,-1},{ -1,3, 4,2 }, {-1,5,1,0} }; 
	//static int[,] DIRS = new int[3,4]{ {1,1,1,1},{ 1,3, 4,2 }, {1,5,1,0} }; 

	public SpriteRenderer movementHighlight;
	public SpriteRenderer attackHighlight;

	//public GameObject movementHighlight;
	//public GameObject attackhHighlight;
	private GameObject mHighlight;
	private GameObject aHighlight;


	// public List<GameObject> neighbors = new List<GameObject>();
	public List<int> neighbors = new List<int>();
	
	public bool inRange = false;
	public bool inAtkRange = false;
	public GameObject takenBy;
	
	public int moveDecrement = 1;
	public int atkDecrement = 1;
	
	public void moveOn(GameObject byObject) {
		taken = true;
		takenBy = byObject;
	}
	
	public void moveOff() {
		taken = false;
		takenBy = null;
	}
	
	public void getMovementByRange(int step) {
		if (step > 0) {
			HexTile temp;
			
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				
				temp = field.map[nei].GetComponent<HexTile>();
				if (!temp.taken) {
					temp.switchNeighborsOn(step);
				} 
			}
		}
	}
	
	public void getAtkByRange(int atkStep) {
		if (atkStep > 0) {
			HexTile temp;
			
			foreach(int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				
				temp = field.map[nei].GetComponent<HexTile>();
				if (temp.taken && temp.takenBy.tag == "Player") {
					temp.switchAttackOnAlly(atkStep);
				} else {
					temp.switchAttackOn(atkStep);
				}
			}
		}
	}
	
	public void cancelMovement(int step) {
		if (step > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchNeighborsOff(step);
			}
		}
	}
	
	public void cancelAttack(int atkStep) {
		if (atkStep > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchAttackOff(atkStep);
			}
		}
	}
	
	protected virtual void switchAttackOn(int atkStep) {
		inAtkRange = true;
		attackHighlight.enabled = true;
		atkStep -= atkDecrement;
		
		if (atkStep > 0) {
			HexTile temp;
			
			foreach(int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				
				temp = field.map[nei].GetComponent<HexTile>();
				if (temp.taken && temp.takenBy.tag == "Player") {
					temp.switchAttackOnAlly(atkStep);
				} else {
					temp.switchAttackOn(atkStep);
				}
			}
		}
	}
	
	protected virtual void switchAttackOnAlly(int atkStep) {
		atkStep -= atkDecrement;
		
		if (atkStep > 0) {
			HexTile temp;
			
			foreach(int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				
				temp = field.map[nei].GetComponent<HexTile>();
				if (temp.taken && temp.takenBy.tag == "Player") {
					temp.switchAttackOnAlly(atkStep);
				} else {
					temp.switchAttackOn(atkStep);
				}
			}
		}
	}
	
	protected virtual void switchNeighborsOn(int step) {
		inRange = true;
		step -= moveDecrement;
		movementHighlight.enabled = true;

		// GetComponent<SpriteRenderer>().color = Color.yellow;
		
		if (step > 0) {
			HexTile temp;
			
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				
				temp = field.map[nei].GetComponent<HexTile>();
				if (!temp.taken) {
					temp.switchNeighborsOn(step);
				}
			}
		}
	}
	
	protected virtual void switchNeighborsOff(int step) {
		inRange = false;
		step -= moveDecrement;
		
		movementHighlight.enabled = false;
		
		//GetComponent<SpriteRenderer>().color = originalColor;
		if (step > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchNeighborsOff(step);
			}
		}
	}
	
	protected virtual void switchAttackOff(int atkStep) {
		inAtkRange = false;
		atkStep -= atkDecrement;
		
		attackHighlight.enabled = false;
		
		if (atkStep > 0) {
			foreach (int nei in neighbors) {
				if (nei == -1) continue;
				if (field.map[nei].GetComponent<BlankTile>() != null) continue;
				field.map[nei].GetComponent<HexTile>().switchAttackOff(atkStep);
			}
		}
	}
	
	//Enemy unit function. Should not use for Players
	public void getAttackRangeEnemy(int step) {
		if (!EnemyTargetModule.foundTarget) {
			
			if (step > 0) {
				HexTile temp;
				
				foreach (int nei in neighbors) {
					if (nei == -1)
						continue;
					if (field.map [nei].GetComponent<BlankTile> () != null)
						continue;
					temp = field.map [nei].GetComponent<HexTile> ();
					if (!temp.taken) {
						temp.switchNeighborsOnEnemy (step);
					} else if (temp.name != "Hex Blank" && temp.takenBy.tag == "Player") {
						// temp.enemyOnTile (); This was removes, think of something else for it if needed
						EnemyTargetModule.target = temp.takenBy;
						EnemyTargetModule.targetID = temp.iD;
						EnemyTargetModule.stopID = iD;
						EnemyTargetModule.foundTarget = true;
					}
				}
			}
		}
	}
	
	//EnemyUnit function. Should not use for Players
	protected virtual void switchNeighborsOnEnemy(int step) {
		if (!EnemyTargetModule.foundTarget) {
			
			inRange = true;
			step -= moveDecrement;
			
			// GetComponent<SpriteRenderer> ().color = Color.yellow;
			
			if (step > 0) {
				HexTile temp;
				foreach (int nei in neighbors) {
					if (nei == -1)
						continue;
					if (field.map [nei].GetComponent<BlankTile> () != null)
						continue;
					temp = field.map [nei].GetComponent<HexTile> ();
					if (!temp.taken) {
						temp.switchNeighborsOnEnemy (step);
					} else if (temp.takenBy.tag == "Player") {
						// temp.enemyOnTile (); This was removes, think of something else for it if needed
						EnemyTargetModule.targetID = temp.iD;
						EnemyTargetModule.stopID = iD;
						EnemyTargetModule.foundTarget = true;
					}
				}
			}
		}
		
	}
	
	//What is this function for?
	public HexTile[] lineTiles(HexTile to){

		HexTile[] inLine = new HexTile[1];
		HexTile currentTile = gameObject.GetComponent<HexTile>(); //probably needs to be gameobject, with that script to object converter jasmine found
		inLine [0] = currentTile;

		//----Initializing initial hextile array (trivial one of only this tile)

		//to check angle between objects, and decide whether to use horizontal or vertical
		Vector3 angleBetween = gameObject.transform.position - currentTile.transform.position;
		float angle = Mathf.Atan2(angleBetween.y, angleBetween.x) * Mathf.Rad2Deg;

		//to get width/height of tile in cartesian coordinate system
		Renderer hexrenderer = gameObject.GetComponent<Renderer> ();
		float tilewidth = hexrenderer.bounds.size.x;
		float tileheight = hexrenderer.bounds.size.y;


		//going to need to add a horizontal/vertical case checker here
		//1. 2DXold = get doubledeltaX between end hex and start hex
		int i1 = Mathf.RoundToInt(to.transform.position.x/tilewidth);
		int j1 = Mathf.RoundToInt(to.transform.position.y/tileheight);
		int i0 = Mathf.RoundToInt(this.transform.position.x/tilewidth);
		int j0 = Mathf.RoundToInt(this.transform.position.y/tileheight);
		int dDX = Mathf.RoundToInt(2*(i1-i0)+Mathf.Abs(j1 % 2) - Mathf.Abs(j0 % 2));
		
		//2. DYold = get Y delta between end hex and start hex
		int DY = j1 - j0;
		
		//3. Xsign = sign(DXold) //-1 or positive 1
		int Xsign;
		if(i1>=i0)
			Xsign = 1;
		else
			Xsign = -1;
		
		//4. Ysign = sign(DYold) //-1 or +1
		int Ysign;
		if(j1>=j0)
			Ysign = 1;
		else
			Ysign = -1;
		
		//5. dY = 3*abs(DYold)
		int dY = 3*Mathf.Abs (DY); //this is fishy, not sure if this is the right scaling
		
		//6. dX = 3*abs(dDXold)
		int dX = 3*Mathf.Abs (dDX); //this is fishy, not sure if this is the right scaling
		
		//7. current hex = start hex
		//Done above, currentTile = gameObject
		
		//8. color current hext (add current hex to array)
		//also done above, added to inLine
		
		//9. Epsilon = 0
		//passing 0 to e parameter of lineTilesRecursive

		inLine = lineTilesRecursive (currentTile, inLine, to,0,dX, dDX,dY,Xsign,Ysign);


		return inLine;
	}

	protected HexTile[] lineTilesRecursive(HexTile currentTile, HexTile[] inLine, HexTile to,int e, int dX,int dDX, int dY, int Xsign, int Ysign)
	{
		//PROCEDURE
		//update current tile
		//via the line algorithm
		//create new array of size+1, and copy old array to it
		//add current tile to this array
		//if current tile == to tile
		//return the array
		//otherwise, get the next tile via recursion


		//Bresenham's Line Algorithm (horizontal and vertical cases)
		//first check whether to use horizontal or vertical via definition horizontal: greaterthan or equal to 0, strictly less than 60 (transform.pos is a vector, use that one page)

		/*

		Vector3 angleBetween = gameObject.transform.position - currentTile.transform.position;
		float angle = Mathf.Atan2(angleBetween.y, angleBetween.x) * Mathf.Rad2Deg;

		Renderer hexrenderer = gameObject.GetComponent<Renderer> ();
		int tilewidth = hexrenderer.bounds.size.x;
		int tileheight = hexrenderer.bounds.size.y;

		*/

			//10. epsilon = epsilon + dY
			e = e + dY;
			//11. if epsilon > abs(dDXold) then
			//current hex = get Direction[x sign][y sign] (Up/Down and Left/right, going horiz so only 4 casses)
			//epsilon = epsilon - dX
			//    else
			//current hex = get Direction[0][Xsign] (see above)
			//epsilon = epsilon + dY
			//endif

			if (e > Mathf.Abs(dDX))
			{
				//current hex = pick the right one
			print("DIR: " + DIRS[Ysign+1,Xsign+1]);
			print("NEIGHBOR: " + currentTile.neighbors [DIRS[Ysign+1,Xsign+1]]);
			print("MAP: " + field.map[currentTile.neighbors [DIRS[Ysign+1,Xsign+1]]]);
			currentTile = field.map[currentTile.neighbors [DIRS[Ysign+1,Xsign+1]]].GetComponent<HexTile>();
				e = e - dX;
			}else
			{
			print("DIR: " + DIRS[Ysign+1,0+1]);
			print("NEIGHBOR: " + currentTile.neighbors [DIRS[Ysign+1,0+1]]);
			print("MAP: " + field.map[currentTile.neighbors [DIRS[Ysign+1,0+1]]]);
			currentTile = field.map[currentTile.neighbors [DIRS[Ysign+1,0+1]]].GetComponent<HexTile>();
				e = e + dY;
			}

			//12. color current hex (add to array)
			//create new array of size+1, copy old array
			HexTile[] newinLine = new HexTile[inLine.Length + 1];
			inLine.CopyTo (newinLine, 0); //this should work, but if it doesn't just use a for loop with the indexes matching up while < inLine's length
			//add current tile to array
			newinLine [newinLine.Length - 1] = currentTile;

			//Check if done?
			//if ==, return
			if (currentTile == to)
			return newinLine;
		
		
			//otherwise...recursion
			return lineTilesRecursive (currentTile,newinLine,to,e,dX, dDX,dY,Xsign,Ysign);




//vertical algorithm
		//1. 2DXold=get doubledeltaX between end hex and start hex (^same)
		//2. DYold = get Y delta between end hex and start hex (^same)
		//3. Xsign = sign(DXold) (-1 or +1) (^same)
		//4. Ysign = sign(DYold) (-1 or +1) (^same)
		//5. dY = abs(DYold) (NOT SAME, *3)
		//6. dX = abs(2*dDXold) (NOT SAME, *3)
		//7. current hex = start hex
		//8. color current hext (add current hex to array)
		//9. epsilon = 0
		//----------------All non-iterative again.  But remember that there's difference in the wrapper fxn too!!!  Will need more args for dY dX etc...
		//While current hex != end hex (recursion inc)
		//10. epsilon = epsilon + dX (instead of dY)
		//11. if epsilon > 0 (instead of absdDXold)
					//current hex = get Dirs [X sign][Y sign]
					//epsilon = epsilon - dY (instead of dX)
		//		else
					//current hex = get Dirs [-Xsign[Y sign] (NEGATIVE X rather than Y=0!!)
					//epsilon = epsilon + dY
		//    endif
		//12. color current hex (add to array)


		//to say FOR SURE which one is lower (the origin), might wanna do a comparison to decide which gets assigned to x and y




		//update current tile
		//via the line algorithm





	}
}
