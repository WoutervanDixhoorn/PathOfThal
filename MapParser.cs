using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;

namespace PathOfThal
{
    public class MapParser
    {       
        bool skipLine = false;
        int section = 0;

        //NOTE: Parse vars
        string mapData;
        int[,] terrainData;
        string currentTile = "";
        List<string> currentTerrainLine;
        List<List<string>> currentTerrain;

        //Setup the mapParser
        public MapParser(){
            currentTerrainLine = new List<String>();
            currentTerrain = new List<List<String>>();
        }

        /// <summary> 
        /// Parses a map to a 'Map' object and returns it
        /// </summary>
        /// <param name="iFileName">name of map file</param>
        public Map Parse(string iFileName){

            //NOTE: Parse mapData to a 'Map' object
            mapData = File.ReadAllText(iFileName);
            List<MapLayer> layers = new List<MapLayer>();
            Terrain terrain = new Terrain();

            if(mapData != string.Empty){
                //Loops trough every char and determinates where it needs to go
                foreach(char c in mapData){
                    if(skipLine && isUseless(c)){
                        skipLine = false;
                    } else if(isComment(c)){
                        skipLine = true;
                    } else if(!skipLine){
                        
                        if(section == 0){
                            if(isUseless(c)){
                                //DoNothing
                            }else if(isComma(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTile = "";
                                //Go to next tile
                            } else if(isNum(c)){
                                currentTile += c;
                            } else if(isDash(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = "";
                                currentTerrainLine = new List<string>();
                            } else if(isAnd(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = "";
                                currentTerrainLine = new List<string>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<String>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                            } else if(isStar(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = "";
                                currentTerrainLine = new List<string>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<String>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                                section = 1;
                            } else {
                                throw new Exception("Expected a number");
                            }

                        } else if(section == 1){
                            Console.WriteLine("Reached section 1");
                            section++;
                        }
                    }
                }
            }

            return new Map(layers);
        }

        #region Utility

        public bool isUseless(char c){
            return c == '\r' || c == '\n';
        }
        public bool isComment(char c){
            return c == '#';
        }

        public bool isStar(char c){
            return c == '*';
        }

        public bool isAnd(char c){
            return c == '&';
        }

        public bool isDash(char c){
            return c == '-';
        }

        public bool isNum(char c){
            return Char.IsDigit(c);
        }

        public bool isComma(char c){
            return c == ',';
        }

        #endregion
    }
}