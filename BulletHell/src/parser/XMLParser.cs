using System;
using System.Collections.Generic;
using System.Xml;
using BulletHell.character;
using BulletHell.ObjectCreation;
using Microsoft.Xna.Framework;
//using System.Xml.Linq;

namespace BulletHell
{
    public class XMLParser : Parser
    {
        List<Encounter> encounterList;
        XmlDocument level;
        EnemyFactory enemyFactory;

        public XMLParser(string filename)
        {
            encounterList = new List<Encounter>();
            level = new XmlDocument();
            level.Load(filename);
            enemyFactory = new EnemyFactory();
        }

        public void Parse()
        {
            int i = 0;
            PrefabRepo prefabRepo = PrefabRepo.getPrefabRepo();
            prefabRepo.emptyEnemyPrefabs();
            XmlNodeList Header = level.DocumentElement.SelectNodes("/level/Header/prefab");
            foreach (XmlNode enemy in Header)
            {
                string name = enemy["name"].InnerText;
                int health;
                if (!Int32.TryParse(enemy["health"].InnerText, out health))
                {
                    health = 3;
                }
                string sprite = enemy["sprite"].InnerText;
                double scale;
                if (!Double.TryParse(enemy["scale"].InnerText, out scale))
                {
                    scale = 1;
                }
                string gun = enemy["gun"].InnerText;

                string s_delay = enemy["gun"].GetAttribute("delay");
                
                float delay;
                delay = float.TryParse(s_delay, out delay) ? delay : 1;
                List<PathData> complexPath = new List<PathData>();

                XmlNodeList path = enemy["path"].ChildNodes;

                foreach (XmlNode part in path)
                {
                    int duration;
                    double offset;
                    int speed;
                    if (!Int32.TryParse(part["dur"].InnerText, out duration))
                    {
                        duration = 0;
                    }
                    if (!Double.TryParse(part["offset"].InnerText, out offset))
                    {
                        offset = 0;
                    }
                    if (!Int32.TryParse(part["speed"].InnerText, out speed))
                    {
                        speed = 0;
                    }
                    complexPath.Add(new PathData(part["type"].InnerText, duration,
                                        offset, speed));
                }
                i++;
                //Console.WriteLine(i);
                Enemy e = enemyFactory.makeEnemy(sprite, health, Vector2.Zero, complexPath, gun, delay, scale);
                try
                {
                    prefabRepo.registerEnemyPrefab(name, e);
                }
                catch (ArgumentException)
                {
                    throw new Exception("Defining a redundant enemy prefab name");
                }
            }

            XmlNodeList Encounters = level.DocumentElement.SelectNodes("/level/Encounters/encounter");
            foreach (XmlNode encounter in Encounters)
            {
                string type = encounter["type"].InnerText;
                int time;
                double xlocal, ylocal;
                if (!Int32.TryParse(encounter["time"].InnerText, out time))
                {
                    time = 0;
                }
                string boss = "";
                try
                {
                    boss = encounter["boss"].InnerText;
                }
                catch {}
                bool isBoss = false;
                if(boss == "true"){
                    isBoss = true;
                }

                if (!Double.TryParse(encounter["location"]["x"].InnerText, out xlocal))
                {
                    xlocal = 0;
                }

                if (!Double.TryParse(encounter["location"]["y"].InnerText, out ylocal))
                {
                    ylocal = 0;
                }
                
                

                encounterList.Add(new Encounter(type, time, new Vector2((float)xlocal, (float)ylocal), isBoss));
            }
        }
        public List<Encounter> getEncounterList()
        {
            return encounterList;
        }
    }

}
