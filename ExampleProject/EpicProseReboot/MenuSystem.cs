using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public class MenuSystem
    {
        public Menu travelMenu;
        public Menu travelMenu2;
        public Menu playMenu;
        public Menu worldMenu;
        public Menu characterMenu;
        public Menu pickUpMenu;
        public Menu[] inventory;
        public Menu itemMenu;
        public Menu[] encounterMenu;
        public Menu fileMenu;
        public Menu helpMenu;
        public Menu mainMenu;

        Game game;

        public MenuSystem(Game game)
        {
            this.game = game;
            SetupMenues();
        }

        public void SetupMenues()
        {
            mainMenu = new Menu
                (
                "Main Menu",
                new MenuItem("Play", () => game.gameState = Game.GameStates.PLAY_MENU),
                new MenuItem("Options", () => game.gameState = Game.GameStates.OPTIONS),
                new MenuItem("Quit", () => game.gameState = Game.GameStates.QUIT)
                );

            playMenu = new Menu
                (
                "Play Menu",
                new MenuItem("New Game", () => game.gameState = Game.GameStates.NEW_GAME),
                new MenuItem("Load Game", () => game.gameState = Game.GameStates.LOAD_GAME),
                new MenuItem("Main Menu", () => game.gameState = Game.GameStates.MAIN_MENU)
                );

            worldMenu = new Menu
                (
                "What would you like to do?",
                     new MenuItem("Travel", () => Program.console.Print(travelMenu)),
                     new MenuItem("Camp", () => game.gameState = Game.GameStates.QUIT),
                     new MenuItem("Look", () => game.gameState = Game.GameStates.QUIT),
                     new MenuItem("Character", () => game.gameState = Game.GameStates.QUIT),
                     new MenuItem("Pick up", () => game.gameState = Game.GameStates.QUIT),//dynamic?
                     new MenuItem("Search", () => game.gameState = Game.GameStates.MAIN_MENU),
                     new MenuItem("File", () => game.gameState = Game.GameStates.QUIT),
                     new MenuItem("Help", () => game.gameState = Game.GameStates.QUIT)
                );

            travelMenu = new Menu//this one has to be dynamic ? could keep the same. add distance?
                (
                "Travel",
                new MenuItem("Ice Mountain", () => Program.console.Print(worldMenu)),
                new MenuItem("Cave", () => Program.console.Print(worldMenu)),
                new MenuItem("Lair", () => Program.console.Print(worldMenu)),
                new MenuItem("Witch Doctor", () => Program.console.Print(worldMenu)),
                new MenuItem("Burned Village", () => Program.console.Print(worldMenu)),
                new MenuItem("Plainsville", () => Program.console.Print(worldMenu)),
                new MenuItem("Rogue's Den", () => Program.console.Print(worldMenu)),
                new MenuItem("Townopolus", () => Program.console.Print(worldMenu)),
                new MenuItem("Next", () => Program.console.Print(travelMenu2)),
                new MenuItem("Back", () => Program.console.Print(worldMenu))
                );

            travelMenu2 = new Menu//this one has to be dynamic ? could keep the same. add distance?
                (
                "Travel",
                new MenuItem("Sandland", () => Program.console.Print(worldMenu)),
                new MenuItem("Pyramid", () => Program.console.Print(worldMenu)),
                new MenuItem("Prev", () => Program.console.Print(travelMenu)),
                new MenuItem("Back", () => Program.console.Print(worldMenu))
                );

            characterMenu = new Menu//this one has to be dynamic
                (
                "Character",
                new MenuItem("Status", () => Program.console.Print(inventory[0])),
                new MenuItem("Inventory", () => Program.console.Print(inventory[0])),
                new MenuItem("Back", () => Program.console.Print(worldMenu))
                );

            /*inventory = new Menu
                (
                "Inventory",
                new MenuItem("Examine", () => Program.console.Print(worldMenu)),
                new MenuItem("Equip", () => Program.console.Print(worldMenu)),
                new MenuItem("Repair", () => Program.console.Print(worldMenu)),
                new MenuItem("Use", () => Program.console.Print(worldMenu)),
                new MenuItem("Drop", () => Program.console.Print(worldMenu)),
                new MenuItem("Back", () => Program.console.Print(worldMenu))
                );

            encounterMenu = new Menu//this one has to be dynamic
                (
                "Encounter"
                );*/

            fileMenu = new Menu
                (
                "File Menu",
                new MenuItem("Save Game", () => game.gameState = Game.GameStates.NEW_GAME),
                new MenuItem("Load Game", () => game.gameState = Game.GameStates.LOAD_GAME),
                new MenuItem("Options", () => game.gameState = Game.GameStates.LOAD_GAME),
                new MenuItem("Quit Game", () => game.gameState = Game.GameStates.MAIN_MENU)
                );
        }

        public void BuildEncounterMenu()
        {

        }

        public void BuildTravelMenu()//?
        {

        }
    }
}
