using UnityEngine;
using System.Collections;

public class CoordinatorScript : MonoBehaviour {
	public GameObject stem;
	public GameObject leaf;
	public GameObject containerOfContainers;
	public int treeSize = 20;
	public int forestSize = 4;

	private GameObject[] containers;

	protected ArrayList forest;

	void Start () {
		containers = new GameObject[forestSize * forestSize];
		forest = new ArrayList ();
		for (int i = 0; i < forestSize * forestSize; i++) {
			containers[i] = new GameObject();
			containers[i].name = "Parent "+i;
			containers[i].transform.parent = containerOfContainers.transform;
			float x = (i/forestSize) * treeSize/2 + treeSize/4 * Random.value;
			float z = (i%forestSize) * treeSize/2 + treeSize/4 * Random.value;
			containers[i].transform.position = new Vector3(x,0,z);
			int type = (int)(Random.value * 3);
			if(type == 0)
				forest.Add(new TemperateTreeRule (treeSize));
			else if (type == 1)
				forest.Add(new CoconutTreeRule (treeSize));
			else
				forest.Add(new TundraTreeRule (treeSize));
		}
		StartCoroutine ("Step");
	}

	IEnumerator Step() {
		for (int i = 0; i < 100; i++) {
			int count = 0;
			foreach(Rule tree in forest){
				ArrayList coordinates = tree.getDifferences();				
				Vector3 parentPosition = containers[count].transform.position;
				foreach(Vector4 c in coordinates){
					GameObject voxel;
					Vector3 postion = new Vector3(c.x+parentPosition.x,c.y+parentPosition.y,c.z+parentPosition.z);
					if(c.w != -1)	
						voxel = (GameObject)Instantiate(stem,postion,Quaternion.identity);
					else
						voxel = (GameObject)Instantiate(leaf,postion,Quaternion.identity);
					voxel.transform.parent = containers[count].transform;
				}
				count++;
			}
			foreach(Rule tree in forest){
				tree.update();
			}
			yield return new WaitForSeconds(1);
		}
	}


}
