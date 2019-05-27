using System.Globalization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace PathOfThal
{
    public class Map
    {
        
        List<MapLayer> layers;

        public Map(){
            layers = new List<MapLayer>();
        }

        public Map(List<MapLayer> iLayers){
            layers = new List<MapLayer>();
            layers.AddRange(iLayers);
        }

        public void Draw(SpriteBatch spriteBatch){
            //spriteBatch.Draw();
        }

        public void Draw(SpriteBatch spriteBatch, int x = 0, int y = 0, params Square[] rect){
            int offsetx = x;
            int offesty = y;

            foreach(MapLayer mapLayer in layers){

                for(int i = 0; i < mapLayer.GetTerrain().GetTerrainData().GetLength(1); i++){
                    for(int j = 0; j < mapLayer.GetTerrain().GetTerrainData().GetLength(0); j++){
                        if(mapLayer.GetTerrain().GetTerrainData()[j,i].GetTileNumber() == 0){
                            //rect[0].Draw(spriteBatch, i * rect[0].Width + offsetx ,j * rect[0].Height + offesty);
                        }else if(mapLayer.GetTerrain().GetTerrainData()[j,i].GetTileNumber() == 1){
                            rect[1].Draw(spriteBatch, i * rect[1].Width + offsetx ,j * rect[1].Height + offesty, 2f);
                        }else if(mapLayer.GetTerrain().GetTerrainData()[j,i].GetTileNumber() == 2){
                            rect[2].Draw(spriteBatch, i * rect[1].Width + offsetx ,j * rect[1].Height + offesty, 0.8f);
                        }
                    }
                }
            }

            
        }

        public void AddTerrain(MapLayer iMapLayer){
            layers.Add(iMapLayer);
        }

        public Terrain GetTerrain(int Index){
            if(!(Index >= layers.Count)){
                return layers[Index].GetTerrain();
            }
            throw new Exception("[Map.GetTerrain(Index = " + Index + ") \n Is not valid. You sure its in bound of the layers list?");
        }

        public override string ToString(){
            string terrainString = String.Empty;

            for(int i = 0; i < layers.Count; i++){
                terrainString += "\t[Layer: " + i + " ]\n\t" + layers[i].GetTerrain().ToString();
            }

            return "[Map]\n" + terrainString;
        }
    }   
}