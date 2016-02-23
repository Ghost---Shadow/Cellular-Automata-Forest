using System.Collections;
using UnityEngine;

public class TemperateTreeRule:Rule{
	int[,,] buffer1,buffer2;
	bool firstBuffer;
	int size,height;
	public TemperateTreeRule(int s){
		size = s;
		height = s / 2;
		buffer1 = new int[size,size,size];
		buffer2 = new int[size,size,size];
		buffer1 [size - 1, size / 2,size/2] = height;
		firstBuffer = false;
	}
	public void update(){
		if (firstBuffer)
			updateBuf (ref buffer1, ref buffer2);
		else
			updateBuf (ref buffer2, ref buffer1);
		firstBuffer = !firstBuffer;
	}
	private void updateBuf(ref int[,,] front, ref int[,,]back){
		for(int i = 0; i < size; i++){
			for(int j = 0; j < size; j++){
				for(int k = 0; k < size; k++){
					front[i,j,k] = back[i,j,k];
					if(back[i,j,k] > 1){				
						int joffset = back[i,j,k] < 3*height/4?getRandom():0;
						int koffset = back[i,j,k] < 3*height/4?getRandom():0;						
						int termination_chance = back[i,j,k] < 2*height/3?getRandom():0;
						if(front[i-1,(j+joffset-1),k+koffset-1] == 0 && 
						   front[i-1,(j+joffset+1),k+koffset+1] == 0 &&
						   front[i-1,(j+joffset),k+koffset] == 0){	
							front[i-1,j+joffset,k+koffset] = termination_chance == 1? 1 : back[i,j,k] - 1;
						}
					}
				}
			}
		}
		for(int i = 0; i < size; i++){
			for(int j = 0; j < size; j++){
				for(int k = 0; k < size; k++){
					if(front[i,j,k] == 1){
						if(front[i-1,j,k] == 0) front[i-1,j,k] = -1;
						if(front[i,j-1,k] == 0) front[i,j-1,k] = -1;
						if(front[i,j+1,k] == 0) front[i,j+1,k] = -1;
						if(front[i,j,k-1] == 0) front[i,j,k-1] = -1;
						if(front[i,j,k+1] == 0) front[i,j,k+1] = -1;
					}
				}
			}
		}
	}
	private int getRandom(){
		return ((int)(Random.value * 3))-1; 
	}
	public ArrayList getDifferences(){
		ArrayList coordinates = new ArrayList();
		for (int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++){
				for (int k = 0; k < size; k++) {
					if(buffer1[i,j,k] != buffer2[i,j,k]){
						coordinates.Add(new Vector4(j,size-i,k,buffer1[i,j,k] + buffer2[i,j,k]));
					}
				}
			}
		}
		return coordinates;
	}
}
