using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;

namespace PathOfThal
{
    public class MapParser
    {       
        bool skipLine = false;

        public enum Section{
            LAYERS,
            COLISION,
            EVENT,
            EVENTNAME,
            END
        }

        #region Section getters
        public static Section LAYERS {get{return Section.LAYERS;}}
        public static Section COLISION {get{return Section.COLISION;}}
        public static Section EVENT {get{return Section.EVENT;}}
        public static Section EVENTNAME {get{return Section.EVENTNAME;}}
        public static Section END {get{return Section.END;}}
        #endregion

        Section section = Section.LAYERS;
        //NOTE: Parse vars
        string mapData;
        int[,] terrainData;
        Tile currentTile = new Tile(0,0,0);
        List<Tile> currentTerrainLine;
        List<List<Tile>> currentTerrain;

        //Event Parse
        string currentEvent;

        //Setup the mapParser
        public MapParser(){
            currentTerrainLine = new List<Tile>();
            currentTerrain = new List<List<Tile>>();
            currentEvent = string.Empty;
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
                    if(skipLine && Utility.isUseless(c)){
                        skipLine = false;
                    } else if(Utility.isComment(c)){
                        skipLine = true;
                    } else if(!skipLine){
                        
                        if(section == Section.LAYERS){
                            if(Utility.isUseless(c)){
                                //DoNothing
                            }else if(Utility.isComma(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTile = new Tile(0,0,0);
                                //Go to next tile
                            } else if(Utility.isNum(c)){
                                currentTile = new Tile((c - '0'),0,0);
                            } else if(Utility.isDash(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                            } else if(Utility.isAnd(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<Tile>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                            } else if(Utility.isStar(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTerrain.Add(currentTerrainLine);
                                currentTile = new Tile(0,0,0);;
                                currentTerrainLine = new List<Tile>();
                                terrain.SetTerrainData(currentTerrain);
                                currentTerrain = new List<List<Tile>>();
                                layers.Add(new MapLayer(terrain));
                                terrain = new Terrain();
                                section = END;
                            }else if(Utility.isOpenBracket(c)){
                                section = COLISION;
                            } else if(Utility.isOpenCurlBracket(c)){
                                section = EVENT;
                            }else {
                                throw new Exception("Expected a number");
                            }

                        } else if(section == COLISION){
                            //Console.WriteLine("Reached collsion");
                            if(Utility.isNum(c)){
                                currentTile = new Tile((c - '0'),0,0, Tile.SOLID);
                            } else if(Utility.isComma(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTile = new Tile(0,0,0);
                                //Go to next tile
                            } else if(Utility.isClosedBracket(c)){
                                //Console.WriteLine("Ending collsion");
                                section = Section.LAYERS;
                            }
                        }else if(section == EVENT){
                            if(Utility.IsDoubleQuote(c)){
                                section = EVENTNAME;
                            } else if(Utility.isNum(c)){
                                currentTile = new Tile((c - '0'),0,0, Tile.EVENT, currentEvent);
                                currentEvent = "";
                            } else if(Utility.isComma(c)){
                                currentTerrainLine.Add(currentTile);
                                currentTile = new Tile(0,0,0);
                                //Go to next tile
                            } else if(Utility.isClosedCurlBracket(c)){
                                //Console.WriteLine("Ending collsion");
                                section = Section.LAYERS;
                            }

                        }else if(section == EVENTNAME){
                            if(Utility.IsChar(c)){
                                currentEvent += c;
                            }else if(Utility.IsDoubleQuote(c)){
                                section = EVENT;
                            }   
                        }else{
                            Console.WriteLine("Parsing map succes!");
                        }
                    }
                }
            }

            return new Map(layers);
        }
    }
}