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
                            rect[2].Draw(spriteBatch, i * rect[1].Width + offsetx ,j * rect[1].Height + offesty, 0.5f);
                        }
                    }
                }
            }

            
        }

        public void DrawColisions(SpriteBatch spriteBatch){
            //TODO: 100 needs to be the size of the tiles
            Square col = new Square(100, 100, Color.Black, 2, Color.Black);
            col.Load(spriteBatch.GraphicsDevice);

            foreach(MapLayer ml in layers){

                for(int i = 0; i < ml.GetTerrain().GetTerrainData().GetLength(0); i++){
                    for(int j = 0; j < ml.GetTerrain().GetTerrainData().GetLength(1); j++){
                        if(ml.GetTerrain().GetTerrainData()[i,j].GetTileType() == Tile.SOLID){
                            col.DrawBorder(spriteBatch, j * col.Width, i * col.Height);
                        }
                    }

                }

            }
        }

        public void DrawTileNumbers(SpriteBatch spriteBatch){
            Text number = new Text("CourierNew");
            number.Load();

            foreach(MapLayer ml in layers){

                for(int i = 0; i < ml.GetTerrain().GetTerrainData().GetLength(0); i++){
                    for(int j = 0; j < ml.GetTerrain().GetTerrainData().GetLength(1); j++){
                        number.DrawBordered(spriteBatch, j + "," + i, j * 100, i * 100, Color.White, null, 0.8f);
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

        public Tile getTile(int x, int y, int Layer = 1){
            if((x > 0 && y > 0 )){
                //TODO: 100 is hard coded, needs to be the size of each tile
                //TODO: Doesnt find the correct tile i suppose!
                return layers[Layer].GetTerrain().GetTerrainData()[((int)y / 100),((int)x / 100)];
            }else{
                Console.WriteLine("[getTile] Tile not found");
                return new Tile(0,0,0);
            }
                //TODO: Is probably a temporary solution, Want it be able to just return a null value

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