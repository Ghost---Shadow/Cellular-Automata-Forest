using System.Collections;
using UnityEngine;

public class TestRule:Rule{
	int[,] buffer1,buffer2;
	bool firstBuffer;
	int size;
	public TestRule(int s){
		size = s;
		buffer1 = new int[size,size];
		buffer2 = new int[size,size];
		buffer1 [size - 1, size / 2] = 5;
		firstBuffer = false;
	}
	public void update(){
		if (firstBuffer)
			updateBuf (ref buffer1, ref buffer2);
		else
			updateBuf (ref buffer2, ref buffer1);
		firstBuffer = !firstBuffer;
	}
	private void updateBuf(ref int[,] front, ref int[,]back){
		//string output = "";
		for (int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++){
				//output+=buffer1[i,j] + " ";
				if(back[i,j] > 0){
					front[i,j] = back[i,j];
					front[i-1,j] = front[i,j] - 1;
				}
			}
			//output += "\n";
		}
		//Debug.Log (output);
	}
	public ArrayList getDifferences(){
		ArrayList coordinates = new ArrayList();
		for (int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++){
				if(buffer1[i,j] != buffer2[i,j]){
					coordinates.Add(new Vector4(j,size-i,0,buffer1[i,j] + buffer2[i,j]));
				}
			}
		}
		return coordinates;
	}
}
