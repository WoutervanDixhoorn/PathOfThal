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
        
        public void Load(){
            foreach(MapLayer ml in layers){
                for(int i = 0; i < ml.GetTerrain().GetTerrainData().GetLength(1); i++){
                    for(int j = 0; j < ml.GetTerrain().GetTerrainData().GetLength(0); j++){
                       
                        ml.GetTerrain().GetTerrainData()[j,i].Load();
                        //Set x and y value for each tile
                        ml.GetTerrain().GetTerrainData()[j,i].X =  i *  ml.GetTerrain().GetTerrainData()[j,i].GetWidth();
                        ml.GetTerrain().GetTerrainData()[j,i].Y =  j *  ml.GetTerrain().GetTerrainData()[j,i].GetHeight();

                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch){
            //spriteBatch.Draw();
        }

        public void Draw(SpriteBatch spriteBatch, int x = 0, int y = 0){
            int offsetx = x;
            int offesty = y;

            foreach(MapLayer mapLayer in layers){

                for(int i = 0; i < mapLayer.GetTerrain().GetTerrainData().GetLength(1); i++){
                    for(int j = 0; j < mapLayer.GetTerrain().GetTerrainData().GetLength(0); j++){
                        mapLayer.GetTerrain().GetTerrainData()[j,i].Draw(spriteBatch);
                    }
                }
            }

            
        }

        public void DrawColisions(SpriteBatch spriteBatch){
            //TODO: 100 needs to be the size of the tiles
            Square col = new Square(100, 100, Color.Black, 2, Color.Black);
            col.Load();

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
            if(!((x > 0 && y > 0 ) && (x < GetWidthOfMap() && y < GetHeightOfMap()) )){
                //Console.WriteLine("[GetTile] Out bound");
                //Console.WriteLine("[getTile] Tile not found at: X:" + ((int)x / 100) + " Y: " + ((int)y / 100));
                return new Tile(0,0,0, Tile.NONE);
            } else{
                //TODO: 100 is hard coded, needs to be the size of each tile
                //TODO: Doesnt find the correct tile i suppose!
                //Console.WriteLine("[GetTile] in bound");
                //Console.WriteLine("[getTile]" + layers[Layer].GetTerrain().GetTerrainData()[((int)y / 100),((int)x / 100)].GetTileType() + "found at: X:" + ((int)x / 100) + " Y: " + ((int)y / 100));

                return layers[Layer].GetTerrain().GetTerrainData()[((int)(y / 100)),((int)(x / 100))];
            }
            
                //TODO: Is probably a temporary solution, Want it be able to just return a null value

        }

        public int GetWidthOfMap(){
            return (layers[0].GetTerrain().GetTerrainData().GetLength(1) ) * 100;
        }

        public int GetHeightOfMap(){
            return (layers[0].GetTerrain().GetTerrainData().GetLength(0) ) * 100;
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