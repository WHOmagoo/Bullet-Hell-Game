using System;
using System.Collections.Generic;
// using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.controls
{
    public class Controller
    {
        public event EventHandler OnLeft;
        public event EventHandler OnRight;
        public event EventHandler OnUp;
        public event EventHandler OnShoot;
        public event EventHandler OnSlow;
        public event EventHandler OnFast;
        public event EventHandler OnDown;
        public event EventHandler OnRebind;

        private Dictionary<Keys, Action> bindings;

        private bool slow = false;
        
        private string[] Names;
        private Action[] Events;
        private Keys[] DefaultKeys;
        
        public Controller(Dictionary<string, Keys> bindingsIn = null)
        {
            //Initialize default key bindings
            string[] tmp = {"left", "right", "up", "down", "shoot", "slow", "rebindkeys"};
            Names = tmp;
            
            Action[] tmp2 = {_OnLeft, _OnRight, _OnUp, _OnDown, _OnShoot, _OnSlow, _OnRebind};
            Events = tmp2;
            
            Keys[] tmp3 = {Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Space, Keys.LeftControl, Keys.F5};
            DefaultKeys = tmp3;

            if (ReferenceEquals(bindingsIn, null))
            {
                loadDefaultBindings();
            }
            else
            {
                bindKeys(bindingsIn);   
            }            
        }

        public void bindKeys(Dictionary<string, Keys> bindingsIn)
        {
            var bindingsCopy = bindings;

            try
            {         
                bindings = new Dictionary<Keys, Action>();
                for (int i = 0; i < Names.Length; i++)
                {
                    if (bindingsIn.ContainsKey(Names[i]))
                    {
                        addBinding(bindingsIn[Names[i]], Events[i]);   
                    }
                    else
                    {
                        //Load default binding for this action
                        addBinding(DefaultKeys[i], Events[i]);
                    }
                }
            }
            catch (Exception e)
            {
                if (ReferenceEquals(null, bindingsCopy))
                {
                    //No previous bindings loaded
                    loadDefaultBindings();
                    throw new Exception(e.Message + " Default bindings have been loaded");
                }
                else
                {
                    //Reload old bindings
                    bindings = bindingsCopy;
                    throw new Exception(e.Message + " Bindings remain unchanged");
                }
            }
            
        }

        private void loadDefaultBindings()
        {
            bindings = new Dictionary<Keys, Action>();
            int i = 0;
            foreach (var key in DefaultKeys)
            {
                addBinding(key, Events[i]);
                i++;
            }
        }

        //Throws an error
        private void addBinding(Keys key, Action action)
        {
            if (ReferenceEquals(action, OnRebind))
            {
                
            } else if (!bindings.ContainsKey(key))
            {
                bindings.Add(key, action);
            } else if (bindings[key] == action)
            {
                //do nothing as the key was already bound to the same event
                Console.WriteLine("Already bound");
                return;
            }
            else
            {
                throw new Exception("Duplicate keybinding detected. " + key + " bound to " + bindings[key] + ". Default bindings will be loaded");
            }
        }

        public void Update()
        {
            KeyboardState currState = Keyboard.GetState();

            bool wasSlow = slow;
            slow = false;
            
            foreach (Keys key in bindings.Keys)
            {
                if (currState.IsKeyDown(key))
                {
                    bindings[key]();
                }
            }

            if (wasSlow && !slow)
            {
                _OnFast();
            }
        }

        private void _OnFast()
        {
            OnFast?.Invoke(this, EventArgs.Empty);
        }

        private void _OnRebind()
        {
            OnRebind?.Invoke(this, EventArgs.Empty);
        }

        private void _OnSlow()
        {
            slow = true;
            OnSlow?.Invoke(this, EventArgs.Empty);
        }

        private void _OnShoot()
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
        }

        private void _OnDown()
        {
            OnDown?.Invoke(this, EventArgs.Empty);
        }

        private void _OnRight()
        {
            OnRight?.Invoke(this, EventArgs.Empty);
        }

        private void _OnLeft()
        {
            OnLeft?.Invoke(this, EventArgs.Empty);
        }
        
        private void _OnUp()
        {
            OnUp?.Invoke(this, EventArgs.Empty);
        }





//            if (currState.IsKeyDown(Keys.LeftControl))
//            {
//                OnSlow(this, EventArgs.Empty);
//            }
//            else if (currState.IsKeyDown(Keys.RightControl))
//            {
//                OnFast(this, EventArgs.Empty);
//            }
//
//            if (currState.IsKeyDown(Keys.Left) && OnLeft != null)
//            {
//                OnLeft(this, EventArgs.Empty);
//            }
//            else if (currState.IsKeyDown(Keys.Right) && OnRight != null)
//            {
//                OnRight(this, EventArgs.Empty);
//            }
//
//            if (currState.IsKeyDown(Keys.Up) && OnUp != null)
//            {
//                OnUp(this, EventArgs.Empty);
//            }
//            else if (currState.IsKeyDown(Keys.Down) && OnDown != null)
//            {
//                OnDown(this, EventArgs.Empty);
//            }
//
//            if (currState.IsKeyDown(Keys.W) && OnUpLeft != null)
//            {
//                OnUpLeft(this, EventArgs.Empty);
//            }
//            else if (currState.IsKeyDown(Keys.E) && OnUpRight != null)
//            {
//                OnUpRight(this, EventArgs.Empty);
//            }
//
//            if (currState.IsKeyDown(Keys.S) && OnDownLeft != null)
//            {
//                OnDownLeft(this, EventArgs.Empty);
//            }
//            else if (currState.IsKeyDown(Keys.D) && OnDownRight != null)
//            {
//                OnDownRight(this, EventArgs.Empty);
//            }
//
//            if (currState.IsKeyDown(Keys.Space) && OnShoot != null)
//            {
//                OnShoot(this, EventArgs.Empty);
//            }
    }
}