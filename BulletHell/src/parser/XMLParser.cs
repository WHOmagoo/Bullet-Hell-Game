using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using BulletHell.ObjectCreation;
using BulletHell.GameEngine;
using BulletHell.character;

namespace BulletHell
{
    public class XMLParser : Parser
    {
        List<Encounter> encounterList;
        XmlDocument level;
        EnemyFactory enemyFactory;

        public XMLParser(string filename)
        {
            level = new XmlDocument();
            level.Load(filename);
            enemyFactory = new EnemyFactory();
        }

        public void Parse(){
            PrefabRepo prefabRepo = PrefabRepo.getPrefabRepo();
            prefabRepo.emptyEnemyPrefabs();
            XmlNodeList Header = level.DocumentElement.SelectNodes("/Level/Header");
            foreach(XmlNode enemy in Header){
                string name = enemy["typename"].Value;
                int health;
                if(!Int32.TryParse(enemy["health"].Value, out health)) {
                    health = 3;
                }
                string sprite = enemy["sprite"].Value;
                string gun = enemy["gun"].Value;
                List<PathData> complexPath = new List<PathData>();
                
                XmlNode path = enemy["path"];
                foreach(XmlNode part in path){
                    int duration;
                    double offset;
                    int speed;
                    if(!Int32.TryParse(part["duration"].Value, out duration)){
                        duration = 0;
                    }
                    if(!Double.TryParse(part["offset"].Value, out offset)){
                        offset = 0;
                    }
                    if(!Int32.TryParse(part["speed"].Value, out speed)){
                        speed = 0;
                    }
                    complexPath.Add(new PathData(part["type"].Value, duration, 
                                        offset, speed));
                }
                Enemy e = enemyFactory.makeEnemy(sprite, health, Vector2.Zero, complexPath, gun);
                try
                {
                    prefabRepo.registerEnemyPrefab(name, e);
                }
                catch(ArgumentException)
                {
                    throw new Exception("Defining a redundant enemy prefab name");
                }
            }

            XmlNodeList Encounters = level.DocumentElement.SelectNodes("/Level/Encounters");
            foreach(XmlNode encounter in Encounters){
                string type = encounter["type"].Value;
                int time;
                double xlocal, ylocal;
                if(!Int32.TryParse(encounter["time"].Value, out time)){
                    time = 0;
                }

                if(!Double.TryParse(encounter["location"]["x"].Value, out xlocal)){
                    xlocal = 0;
                }

                if(!Double.TryParse(encounter["location"]["y"].Value, out ylocal)){
                    ylocal = 0;
                }

                encounterList.Add(new Encounter(type, time, new Vector2((float)xlocal, (float)ylocal)));
            }
        }
        public List<Encounter>  getEncounterList(){
            return encounterList;
        }
    }

}
