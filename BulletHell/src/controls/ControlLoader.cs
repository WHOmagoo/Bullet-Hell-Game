using System;
using System.Collections.Generic;
using System.IO;
using BulletHell.Annotations;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.controls
{
    public class ControlLoader
    {   
        private string filePath;
        
        public ControlLoader(String filePath)
        {
            this.filePath = filePath;
        }

        public void OnLoadKeys(object sender, EventArgs e)
        {
            Controller controller = sender as Controller;
            if (!ReferenceEquals(controller, null))
            {
                StreamReader stream = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                Dictionary<string, Keys> dictionary = new Dictionary<string, Keys>();

                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();

                    char[] splitter = {',', ':', ' '};

                    string[] pair = line.Split(splitter, 2);

                    Keys k = getKey(pair[1]);

                    if (k != Keys.None)
                    {
                        dictionary.Add(pair[0].ToLower(), k);
                    }
                }
                
                controller.bindKeys(dictionary);
            }
            else
            {
                throw new Exception("Wrong sender trying to load keys");
            }


        }

        private Keys getKey(string name)
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                name = name.ToLower();
                string curKeyName = key.ToString("G").ToLower();

                if (curKeyName.Equals(name))
                {
                    return key;
                }
            }

            return Keys.None;
        }
    }
}