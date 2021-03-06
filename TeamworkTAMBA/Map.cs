﻿namespace TeamworkTAMBA
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;

    using TeamworkTAMBA.Exceptions;

    public class Map
    {
        public const int mapTileSize = 40;

        public int CurrentSprite { get; set; }

        public List<GameObject> MapTiles { get; set; }

        public Dictionary<int, List<GameObject>> CharatersAndItems { get; set; }

        public Map(Form form)
        {
            this.CharatersAndItems = new Dictionary<int, List<GameObject>>();
            this.DrawNextSprite();
            this.InitiaizeCharatersAndItems();
        }

        private void InitiaizeCharatersAndItems()
        {
            for (int i = 0; i < 9; i++)
            {
                List<GameObject> list = new List<GameObject>();
                list = this.InitiaizeItems("../../Sprites/spriteCharacter" + (i + 1).ToString("0#") + ".txt");
                this.CharatersAndItems.Add(i + 1, list);
            }
        }

        public void DrawNextSprite()
        {
            CurrentSprite++;
            this.MapTiles = new List<GameObject>();
            Initiaize("../../Sprites/sprite" + CurrentSprite.ToString("0#") + ".txt");
        }

        public void DrawPreviousSprite()
        {
            CurrentSprite--;
            this.MapTiles = new List<GameObject>();
            Initiaize("../../Sprites/sprite" + CurrentSprite.ToString("0#") + ".txt");
        }

        public void DrawLowerSprite()
        {
            CurrentSprite += 3;
            this.MapTiles = new List<GameObject>();
            Initiaize("../../Sprites/sprite" + CurrentSprite.ToString("0#") + ".txt");
        }
        public void DrawUpperSprite()
        {
            CurrentSprite -= 3;
            this.MapTiles = new List<GameObject>();
            Initiaize("../../Sprites/sprite" + CurrentSprite.ToString("0#") + ".txt");
        }

        private void Initiaize(string mapName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(mapName))
                {
                    int col = 0;

                    bool firstLine = true;
                    int lineLength = 0;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (firstLine)
                        {
                            firstLine = false;
                            lineLength = line.Length;
                        }
                        else
                        {
                            if (line.Length != lineLength)
                            {
                                throw new InvalidLineLengthException("Invalid Line Length in map file");
                            }
                        }

                        for (int row = 0; row < line.Length; row++)
                        {
                            GameObject item = new GameObject();

                            var itemlocation = new Point(row * mapTileSize - 40, col * mapTileSize - 40);

                            switch (line[row].ToString())
                            {
                                case "w":
                                    item = new Wall(SpriteType.Wall, itemlocation, 0);
                                    break;
                                case "r":
                                    item = new Wall(SpriteType.Railing, itemlocation, 0);
                                    break;
                                case "g":
                                    item = new Gate(SpriteType.Gate, itemlocation, 0);
                                    break;
                                case "d":
                                    item = new Desk(SpriteType.Desk, itemlocation, 0);
                                    break;
                                case "a":
                                    item = new Air(SpriteType.Air, itemlocation, 0);
                                    break;
                                case "f":
                                    item = new Floor(SpriteType.Floor, itemlocation, 0);
                                    break;
                                case "c":
                                    item = new Coffee(SpriteType.Coffee, itemlocation, 0);
                                    break;
                                case "n":
                                    item = new Floor(SpriteType.Floor, itemlocation, 1); // next sprite
                                    break;
                                case "p":
                                    item = new Floor(SpriteType.Floor, itemlocation, 2); // previous sprite
                                    break;
                                case "l":
                                    item = new Floor(SpriteType.Floor, itemlocation, 3); // lower sprite
                                    break;
                                case "u":
                                    item = new Floor(SpriteType.Floor, itemlocation, 4); // upper sprite
                                    break;
                                default:
                                    break;
                            }

                            if (item.SpiteType == SpriteType.None)
                            {
                                continue;
                            }
                            this.MapTiles.Add(item);
                        }
                        col++;
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private List<GameObject> InitiaizeItems(string mapName)
        {
            List<GameObject> list = new List<GameObject>();

            try
            {
                using (StreamReader sr = new StreamReader(mapName))
                {
                    int col = 0;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        for (int row = 0; row < line.Length; row++)
                        {
                            GameObject item = new GameObject();

                            var itemlocation = new Point(row * mapTileSize - 40, col * mapTileSize - 40);

                            switch (line[row].ToString())
                            {
                                case "h":
                                    item = new Homework(SpriteType.Homework, itemlocation, 1);
                                    break;
                                case "t":
                                    item = new Teamwork(SpriteType.Teamwork, itemlocation, 1);
                                    break;
                                case "e":
                                    item = new Exam(SpriteType.Exam, itemlocation, 1);
                                    break;
                                case "N":
                                    item = new Nakov(SpriteType.Nakov, itemlocation, 0);
                                    break;
                                case "A":
                                    item = new Nasko(SpriteType.Nasko, itemlocation, 0);
                                    break;
                                case "D":
                                    item = new Didko(SpriteType.Didko, itemlocation, 0);
                                    break;
                                case "V":
                                    item = new Vlado(SpriteType.Vlado, itemlocation, 0);
                                    break;
                                case "K":
                                    item = new SuperVlado(SpriteType.SuperVlado, itemlocation, 0);
                                    break;
                                case "L":
                                    item = new Alex(SpriteType.Alex, itemlocation, 0);
                                    break;
                                case "C":
                                    item = new Coffee(SpriteType.Coffee, itemlocation, 0);
                                    break;
                                case "I":
                                    item = new Time(SpriteType.Time, itemlocation, 0);
                                    break;
                                case "P":
                                    item = new Padlock(SpriteType.Padlock, itemlocation, 0);
                                    break;
                                default:
                                    break;
                            }
                            list.Add(item);
                        }
                        col++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return list;
        }
    }
}
