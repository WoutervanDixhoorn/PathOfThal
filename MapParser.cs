using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;

namespace PathOfThal
{
    public class MapParser
    {       
        bool skipLine = false;

        enum Section{
            LAYERS,
            COLISION,
            SCRIPTREGION,
            END

        }

        Section section = Section.LAYERS;
        //NOTE: Parse vars
        string mapData;
        int[,] terrainData;
        Tile currentTile = new Tile(0,0,0);
        List<Tile> currentTerrainLine;
        List<List<Tile>> currentTerrain;

        //Setup the mapParser
        public MapParser(){
            currentTerrainLine = new List<Tile>();
            currentTerrain = new List<List<Tile>>();
        }

        /// <summary> 
        /// Parses a map file to a 'Map' object and returns it
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
                        
                        if(section == Section.LAYERS){
                            if(isUseless(c)){
                                //DoNothing
                            }else if(isComma(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTile = new Tile(0,0,0);
                                //Go to next tile
                            } else if(isNum(c)){
                                currentTile = new Tile((c - '0'),0,0);
                            } else if(isDash(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                            } else if(isAnd(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<Tile>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                            } else if(isStar(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<Tile>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                                section = Section.END;
                            }else if(isOpenBracket(c)){
                                section = Section.COLISION;
                            } else {
                                throw new Exception("Expected a number");
                            }

                        } else if(section == Section.COLISION){
                            Console.WriteLine("Reached collsion");
                            if(isNum(c)){

                            }
                            if(isClosedBracket(c)){
                                Console.WriteLine("Ending collsion");
                                section = Section.LAYERS;
                            }
                        }else{
                            Console.WriteLine("Parsing map succes!");
                        }
                    }
                }
            }

            return new Map(layers);
        }

        #region Utility

        public bool isOpenBracket(char c){
            return c == '[';
        }

        public bool isClosedBracket(char c){
            return c == ']';
        }

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