using System;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public static class WorldMapCells
    {
        public static Cell waterCell = new Cell('~', Color.DarkYellow, Color.DarkRed);
        public static Cell desertCell = new Cell('░', Color.DarkYellow, Color.DarkRed);
        public static Cell desertShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        public static Cell desertShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell desertShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        public static Cell desertShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell forestCell = new Cell('♣', Color.DarkYellow, Color.DarkRed);
        public static Cell forestShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        public static Cell forestShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell forestShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        public static Cell forestShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell swampCell = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        public static Cell swampShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        public static Cell swampShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell swampShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        public static Cell swampShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsCell = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell snowCell = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        public static Cell snowShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        public static Cell snowShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell snowShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        public static Cell snowShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell mountainCell0 = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        public static Cell mountainCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        public static Cell mountainCell2 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        public static Cell iceMountain = new Cell('0', Color.DarkYellow, Color.DarkRed);
        public static Cell dwarfCave = new Cell('1', Color.DarkYellow, Color.DarkRed);
        public static Cell dragonLair = new Cell('2', Color.DarkYellow, Color.DarkRed);
        public static Cell witchDoctorHut = new Cell('3', Color.DarkYellow, Color.DarkRed);
        public static Cell burnedVillage = new Cell('4', Color.DarkYellow, Color.DarkRed);
        public static Cell plainsville = new Cell('5', Color.DarkYellow, Color.DarkRed);
        public static Cell roguesDen = new Cell('6', Color.DarkYellow, Color.DarkRed);
        public static Cell townopolus = new Cell('7', Color.DarkYellow, Color.DarkRed);
        public static Cell sandland = new Cell('8', Color.DarkYellow, Color.DarkRed);
        public static Cell pyramid = new Cell('9', Color.DarkYellow, Color.DarkRed);

        public static MapKey worldMapKey = new MapKey(
            Tuple.Create('~', waterCell),
            Tuple.Create('a', desertCell),
            Tuple.Create('b', desertShoreCell0),
            Tuple.Create('c', desertShoreCell1),
            Tuple.Create('d', desertShoreCell2),
            Tuple.Create('e', desertShoreCell3),
            Tuple.Create('f', forestCell),
            Tuple.Create('g', forestShoreCell0),
            Tuple.Create('h', forestShoreCell1),
            Tuple.Create('i', forestShoreCell2),
            Tuple.Create('j', forestShoreCell3),
            Tuple.Create('k', swampCell),
            Tuple.Create('l', swampShoreCell0),
            Tuple.Create('m', swampShoreCell1),
            Tuple.Create('n', swampShoreCell2),
            Tuple.Create('o', swampShoreCell3),
            Tuple.Create('p', plainsCell),
            Tuple.Create('q', plainsShoreCell0),
            Tuple.Create('r', plainsShoreCell1),
            Tuple.Create('s', plainsShoreCell2),
            Tuple.Create('t', plainsShoreCell3),
            Tuple.Create('u', snowCell),
            Tuple.Create('v', snowShoreCell0),
            Tuple.Create('w', snowShoreCell1),
            Tuple.Create('x', snowShoreCell2),
            Tuple.Create('y', snowShoreCell3),
            Tuple.Create('z', mountainCell0),
            Tuple.Create(',', mountainCell1),
            Tuple.Create('.', mountainCell2),
            Tuple.Create('0', iceMountain),
            Tuple.Create('1', dwarfCave),
            Tuple.Create('2', dragonLair),
            Tuple.Create('3', witchDoctorHut),
            Tuple.Create('4', burnedVillage),
            Tuple.Create('5', plainsville),
            Tuple.Create('6', roguesDen),
            Tuple.Create('7', townopolus),
            Tuple.Create('8', sandland),
            Tuple.Create('9', pyramid)
       );
    }
}
