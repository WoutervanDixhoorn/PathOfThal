using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace PathOfThal
{
    public class EventParser
    {
        public List<string> DialogText;
        string currentDialogLine;
        public string Type;

        string eventData;
        IEvent eventObject;

        public enum Section{
            TYPE,
            TYPEDATA,
            DATA,
            DATADATA,
            DONE,
            NONE
        }
        public Section currentSection;

        public EventParser(){

        }

        public IEvent ParseEvent(string iFileName){

            //NOTE: Parse mapData to a 'Map' object
            eventData = File.ReadAllText(iFileName);

            if(eventData != string.Empty){
                //Reset some vars because parser is declared once
                currentSection = Section.TYPE;
                DialogText = new List<string>();
                Type = string.Empty;
                currentDialogLine = String.Empty;

                //Parse event
                foreach(char c in eventData){
                    if(currentSection == Section.TYPE){
                        if(Utility.IsQuote(c)){
                            currentSection = Section.TYPEDATA;
                        }
                    }else if(currentSection == Section.TYPEDATA){
                        if(Utility.IsQuote(c)){
                            currentSection = Section.DATA;
                        }else if(Utility.IsChar(c)){
                            Type += c;
                        }
                    }else if(currentSection == Section.DATA){
                        if(Utility.IsDoubleQuote(c)){
                            currentSection = Section.DATADATA;
                        }else if(Utility.isStar(c)){
                            currentSection = Section.DONE;
                            eventObject = new Dialog(DialogText.ToArray());
                        }
                    }else if(currentSection == Section.DATADATA){
                        if(Utility.IsDoubleQuote(c)){
                            currentSection = Section.DATA;
                            DialogText.Add(currentDialogLine);
                            currentDialogLine = "";
                        }else if(Utility.IsChar(c) || Utility.isNum(c)){
                            currentDialogLine += c;
                        }
                    }else if(currentSection == Section.DONE){
                        Console.WriteLine("[EventParser] Parsed following event correctly");
                        Console.WriteLine(eventObject.ToString());
                        currentSection = Section.NONE;
                    }
                }

            }
            return eventObject;
        }
    }
}