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
            Terrain terrain = new Terrain();

            if(mapData != string.Empty){
                //Loops trough every char and determinates where it needs to go
                foreach(char c in mapData){
                    if(skipLine && isEndLine(c)){
                        skipLine = false;
                    } else if(isComment(c)){
                        skipLine = true;
                    } else if(!skipLine){
                        
                        if(section == 0){
                            if(isComma(c)){
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
                            } else if(isStar(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = "";
                                currentTerrainLine = new List<string>();
                                terrain.SetTerrainData(currentTerrain);
                                section++;
                            }
                        }

                    }
                }
            }

            return new Map(terrain);
        }

        #region Utility
        public bool isComment(char c){
            return c == '#';
        }

        public bool isStar(char c){
            return c == '*';
        }

        public bool isDash(char c){
            return c == '-';
        }

        public bool isNum(char c){
            return Char.IsDigit(c);
        }

        public bool isEndLine(char c){
            return c == '\n';
        }

        public bool isComma(char c){
            return c == ',';
        }

        #endregion
    }
}