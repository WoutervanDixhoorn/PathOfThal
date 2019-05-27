using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace PathOfThal
{
    public class Map
    {
        
        Terrain terrain;

        public Map(){
            terrain = new Terrain();
        }

        public Map(Terrain iTerrain){
            terrain = iTerrain;
        }

        public void Draw(SpriteBatch spriteBatch){
            //spriteBatch.Draw();
        }

        public void Draw(SpriteBatch spriteBatch, int x = 0, int y = 0, params Square[] rect){
            int offsetx = x;
            int offesty = y;

            for(int i = 0; i < terrain.GetTerrainData().GetLength(1); i++){
                for(int j = 0; j < terrain.GetTerrainData().GetLength(0); j++){
                    if(terrain.GetTerrainData()[j,i] == 0){
                        rect[0].Draw(spriteBatch, i * rect[0].Width + offsetx ,j * rect[0].Height + offesty);
                    }else if(terrain.GetTerrainData()[j,i] == 1){
                        rect[1].Draw(spriteBatch, i * rect[1].Width + offsetx ,j * rect[1].Height + offesty);
                    }else if(terrain.GetTerrainData()[j,i] == 2){
                        rect[2].Draw(spriteBatch, i * rect[1].Width + offsetx ,j * rect[1].Height + offesty);
                    }
                }
            }
        }

        public void SetTerrain(Terrain iTerrain){
            terrain = iTerrain;
        }

        public Terrain GetTerrain(){
            return terrain;
        }

        public override string ToString(){
            return "[Map]\n" + terrain.ToString();
        }
    }   
}