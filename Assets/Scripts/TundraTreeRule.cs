﻿using System.Collections;
using UnityEngine;

public class TundraTreeRule:Rule{
	int[,,] buffer1,buffer2;
	bool firstBuffer;
	int size,height;
	public TundraTreeRule(int s){
		size = s;
		height = s/3 + (int)(Random.value * s/2);
		buffer1 = new int[size,size,size];
		buffer2 = new int[size,size,size];
		buffer1 [size - 1, size / 2,size / 2] = height;
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
					if(back[i,j,k] > 0){				
						front[i,j,k] = back[i,j,k];
						front[i-1,j,k] = back[i,j,k] - 1;
						if(front[i,j,k] < height - 1){
							if(front[i,j-1,k] == 0) 
								front[i,j-1,k] = front[i,j,k]/2;
							if(front[i,j+1,k] == 0) 
								front[i,j+1,k] = front[i,j,k]/2;
							if(front[i,j,k-1] == 0) 
								front[i,j,k-1] = front[i,j,k]/2;
							if(front[i,j,k+1] == 0) 
								front[i,j,k+1] = front[i,j,k]/2;
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
