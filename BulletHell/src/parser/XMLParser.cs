using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BulletHell
{
    public class XMLParser : Parser
    {
        Dictionary<string, GameEngine.Enemy> enemyPrefabs;
        Wave[] waves;
        XElement level;


        public XMLParser(string filename)
        {
            level = XElement.Load(filename);
        }

        public Dictionary<string, GameEngine.Enemy> getEnemyPrefabs() {
            return enemyPrefabs;
        }

        public Wave[] getWaves() {
            return waves;
        }

        public void Parse(){
            foreach (var enemy in level.Elements("enemies")){
                //TODO: impliment XML to dictionary mapping
                // of prefabs
            }

            foreach (var wave in level.Elements("waves")) {
                //TODO: impliment wave creation for each element
                // and pushing it to the waves list
            }
        }
    }
}
