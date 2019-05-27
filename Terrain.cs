using System;
using System.Linq;
using System.Collections.Generic;

namespace PathOfThal
{
    public class Terrain
    {
        
        int[,] TerrainData;

        public Terrain(){
        }

        public void SetTerrainData(List<List<string>> iTerrainData){
            int TerrainHeight = iTerrainData.Count;
            int TerrainWidth = iTerrainData[0].Count;

            TerrainData = new int[TerrainHeight, TerrainWidth];

            for(int i = 0; i < TerrainHeight; i++){
                for(int j = 0; j < TerrainWidth; j++){
                    TerrainData[i,j] = Int32.Parse(iTerrainData[i][j]);
                }
            }

        }

        public int[,] GetTerrainData(){
            return TerrainData;
        }

        public override string ToString(){
            string data = "";

            for(int i = 0; i < TerrainData.GetLength(0); i++){
                for(int j = 0; j < TerrainData.GetLength(1); j++){
                    data += " " + TerrainData[i,j];
                }
                data += '\n';
            }

            return "[Terrain] \n" + data;
        }
    }
}