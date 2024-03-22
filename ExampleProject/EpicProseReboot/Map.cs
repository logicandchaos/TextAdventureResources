using System;
using System.Collections.Generic;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public class Map
    {
        Cell waterCell = new Cell('~', Color.DarkYellow, Color.DarkRed);
        Cell desertCell = new Cell('░', Color.DarkYellow, Color.DarkRed);
        Cell desertShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        Cell desertShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell desertShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        Cell desertShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell forestCell = new Cell('♣', Color.DarkYellow, Color.DarkRed);
        Cell forestShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        Cell forestShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell forestShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        Cell forestShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell swampCell = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        Cell swampShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        Cell swampShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell swampShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        Cell swampShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell plainsCell = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        Cell plainsShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        Cell plainsShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell plainsShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        Cell plainsShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell snowCell = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        Cell snowShoreCell0 = new Cell('_', Color.DarkYellow, Color.DarkRed);
        Cell snowShoreCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell snowShoreCell2 = new Cell('|', Color.DarkYellow, Color.DarkRed);
        Cell snowShoreCell3 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell mountainCell0 = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        Cell mountainCell1 = new Cell('/', Color.DarkYellow, Color.DarkRed);
        Cell mountainCell2 = new Cell('\\', Color.DarkYellow, Color.DarkRed);
        Cell iceMountain = new Cell('0', Color.DarkYellow, Color.DarkRed);
        Cell dwarfCave = new Cell('1', Color.DarkYellow, Color.DarkRed);
        Cell dragonLair = new Cell('2', Color.DarkYellow, Color.DarkRed);
        Cell witchDoctorHut = new Cell('3', Color.DarkYellow, Color.DarkRed);
        Cell burnedVillage = new Cell('4', Color.DarkYellow, Color.DarkRed);
        Cell plainsville = new Cell('5', Color.DarkYellow, Color.DarkRed);
        Cell roguesDen = new Cell('6', Color.DarkYellow, Color.DarkRed);
        Cell townopolus = new Cell('7', Color.DarkYellow, Color.DarkRed);
        Cell sandland = new Cell('8', Color.DarkYellow, Color.DarkRed);
        Cell pyramid = new Cell('9', Color.DarkYellow, Color.DarkRed);

        MapKey worldMapKey = new MapKey();
        //I have 10 locations, so I might number them 0-9
        Matrix worldMap = new Matrix
            (
            "Epica",
            "0: Ice Mountain, 1: Cave, 2: Lair, 3: Witch Doctor, 4: Burned Village,"
            + "\n5: Plainsville 6: Rogue's Den, 7: Townopolus, 8: Sandland, 9: Pyramid"
            );

        public void SetupMap()
        {
            worldMapKey.AddKey('~', waterCell);
            worldMapKey.AddKey('a', desertCell);
            worldMapKey.AddKey('b', desertShoreCell0);
            worldMapKey.AddKey('c', desertShoreCell1);
            worldMapKey.AddKey('d', desertShoreCell2);
            worldMapKey.AddKey('e', desertShoreCell3);
            worldMapKey.AddKey('f', forestCell);
            worldMapKey.AddKey('g', forestShoreCell0);
            worldMapKey.AddKey('h', forestShoreCell1);
            worldMapKey.AddKey('i', forestShoreCell2);
            worldMapKey.AddKey('j', forestShoreCell3);
            worldMapKey.AddKey('k', swampCell);
            worldMapKey.AddKey('l', swampShoreCell0);
            worldMapKey.AddKey('m', swampShoreCell1);
            worldMapKey.AddKey('n', swampShoreCell2);
            worldMapKey.AddKey('o', swampShoreCell3);
            worldMapKey.AddKey('p', plainsCell);
            worldMapKey.AddKey('q', plainsShoreCell0);
            worldMapKey.AddKey('r', plainsShoreCell1);
            worldMapKey.AddKey('s', plainsShoreCell2);
            worldMapKey.AddKey('t', plainsShoreCell3);
            worldMapKey.AddKey('u', snowCell);
            worldMapKey.AddKey('v', snowShoreCell0);
            worldMapKey.AddKey('w', snowShoreCell1);
            worldMapKey.AddKey('x', snowShoreCell2);
            worldMapKey.AddKey('y', snowShoreCell3);
            worldMapKey.AddKey('z', mountainCell0);
            worldMapKey.AddKey(',', mountainCell1);
            worldMapKey.AddKey('.', mountainCell2);
            worldMapKey.AddKey('0', iceMountain);
            worldMapKey.AddKey('1', dwarfCave);
            worldMapKey.AddKey('2', dragonLair);
            worldMapKey.AddKey('3', witchDoctorHut);
            worldMapKey.AddKey('4', burnedVillage);
            worldMapKey.AddKey('5', plainsville);
            worldMapKey.AddKey('6', roguesDen);
            worldMapKey.AddKey('7', townopolus);
            worldMapKey.AddKey('8', sandland);
            worldMapKey.AddKey('9', pyramid);

            worldMap.CreateMatrixFromString(
            worldMapKey,
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~_________~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~/         \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~|          \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~__/   /\/\     \_____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~|     /mm\m\    /\/\  \____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~|              /mm\m\      \____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~/   /\ /\                /\/\    \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~|  /mm\mm\   0          /mm\m\    |__~~~~~~~~~_____~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~|                                    \_______/     \_~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~_/           /\/\              /\/\     ▒▒▒▒▒▒▒ /\ /\  \~~~~~~~~~~~~~~~",
            @"~~~~~~~~/     ♣  ♣   /mm\m\       1    /mm\m\  ▒▒▒▒▒▒▒▒ /  \  \  |~~~~~~~~~~~~~~",
            @"~~~~~~~_|   ♣  ♣ ♣ ♣                          ▒▒▒▒▒▒▒▒▒▒   2    /~~~~~~~~~~~~~~~",
            @"~~~~~~/    ♣ ♣  ♣   ♣      /\ /\       /\/\  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ |_~~~~~~~~~~~~~~~",
            @"~~~~~/    ♣  ♣  ♣ ♣  ♣    /mm\mm\     /mm\m\ ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~~~~~",
            @"~~~~~|   ♣  ♣  ♣   ♣   ♣                     ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|~~~~~~~~~~~~~",
            @"~~~_/   ♣  ♣  ♣   ♣  ♣ ♣ ♣                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~~~~",
            @"~~|      ♣ ♣▒▒▒▒▒   ♣  ♣  ♣                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|_~~~~~~~~~~~",
            @"~~|   ♣ ♣  ▒▒▒▒▒▒▒ ♣  ♣ ♣  ♣                     ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|~~~~~~~~~~",
            @"~~|   ♣  ♣▒▒▒▒▒▒▒3▒ ♣ ♣  ♣ ♣                       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~",
            @"~~~\    ♣  ▒▒▒▒▒▒▒  ♣  ♣   ♣                   4      ▒▒▒▒▒▒▒▒▒▒▒▒    |~~~~~~~~~",
            @"~~~~|    ♣   ▒▒▒  ♣   ♣   ♣                             ♣   ♣   ♣ ♣ ♣ |~~~~~~~~~",
            @"~~~~\_   ♣  ♣ ♣  ♣   ♣   ♣                            ♣   ♣    ♣   ♣ ♣\~~~~~~~~~",
            @"~~~~~~\    ♣ ♣  ♣  ♣   ♣ ♣                          ♣   ♣  ♣  ♣  ♣  ♣ _|~~~~~~~~",
            @"~~~~~~~\_   ♣  ♣  ♣  ♣  ♣                          ♣  ♣   ♣ ♣  ♣  ♣  ♣\~~~~~~~~~",
            @"~~~~~~~~~\    ♣ ♣  ♣  ♣                             ♣   ♣  ♣  ♣  ♣    ♣|~~~~~~~~",
            @"~~~~~~~~~~\__    ♣  ♣                             ♣   ♣   ♣  ♣   ♣  ♣  |~~~~~~~~",
            @"~~~~~~~~~~~~~~|                   5                ♣   ♣   ♣  ♣  ♣  ♣  |~~~~~~~~",
            @"~~~~~~~~~~~~~~\                                   ♣   ♣  ♣   ♣  ♣ 6   ♣|~~~~~~~~",
            @"~~~~~~~~~~~~~~~\                                    ♣   ♣  ♣   ♣   ♣ ♣ |~~~~~~~~",
            @"~~~~~~~~~~~~~~~|                                     ♣   ♣   ♣   ♣  ♣__|~~~~~~~~",
            @"~~~~~~~~~~~~~~~~\                                      ♣  ♣  ♣  ♣ ♣ |_~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~|                                        ♣ ♣   ♣  ♣  ♣|~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~\                                           ♣   ♣     \~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~\                                                    |~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~\                                                   |~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~\                                                   \~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~\                                                  |~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~|                                        7         |~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~|                                                  |~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~\              ░░░░░░░░░░░░░░░░                    /~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~|        ░░░░░░░░░░░░░░░░░░░░░░░░░░              |~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~\    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░           |~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░       /~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ _/~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░8░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░|~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ /~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░__|~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ___|~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░_/~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░ _/~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░    /~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░  |~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░9░░░░░░░░░░░░░░_|~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░/~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_░░░░░░░░░░░░░░░░  __/~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░__/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_░░░░░░░░░░_/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\___░░░__/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            );
            //key
            /*@"0: Ice Mountain,     1: Dwarf Cave,     2: Dragon Lair     3: Witch Doctor,",
            @"4: Burned Village,   5: Plainsville     6: Rogue's Den,",
            @"7: Townopolus,       8: Sandland        9: Pyramid"*/
        }
    }
}
