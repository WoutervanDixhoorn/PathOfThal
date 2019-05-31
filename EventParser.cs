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
            DONE
        }
        public Section currentSection;

        public EventParser(){
            currentSection = Section.TYPE;
            DialogText = new List<string>();
            Type = string.Empty;
            currentDialogLine = String.Empty;
        }

        public IEvent ParseEvent(string iFileName){

            //NOTE: Parse mapData to a 'Map' object
            eventData = File.ReadAllText(iFileName);

            if(eventData != string.Empty){
                
                foreach(char c in eventData){
                    if(currentSection == Section.TYPE){
                        if(Utility.IsQuote(c)){
                            currentSection = Section.TYPEDATA;
                        }
                    }else if(currentSection == Section.TYPEDATA){
                        if(Utility.IsQuote(c)){
                            Type += c;
                            currentSection = Section.DATA;
                        }else if(Utility.IsChar(c)){
                            Type += c;
                        }
                    }else if(currentSection == Section.DATA){
                        if(Utility.IsDoubleQuote(c)){
                            currentSection = Section.DATADATA;
                        } else if(Utility.isUseless(c)){
                            currentSection = Section.DONE;
                        }
                    }else if(currentSection == Section.DATADATA){
                        if(Utility.IsDoubleQuote(c)){
                            currentSection = Section.DATA;
                            currentDialogLine += c;
                            DialogText.Add(currentDialogLine);
                        }else if(Utility.IsChar(c)){
                            currentDialogLine += c;
                        }
                    }else {
                        Console.WriteLine("[EventParser] Parsed event correctly");
                    }
                }

            }
            return new Dialog(DialogText.ToArray());
        }
    }
}