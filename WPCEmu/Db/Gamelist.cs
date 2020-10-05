using System;
using System.Linq;

namespace WPCEmu.Db
{
    public static class Gamelist
    {
        static IDb[] wpcGames =
        {
            new AddamsFamily(),
            new AddamsFamilyValues(),
            new AttackFromMars(),
            new AttackFromMarsFreewpc(),
            new BlackRose(),
            new BramStokersDracula(),
            new CactusCanyon(),
            new CirqusVoltaire(),
            new Congo(),
            new Corvette(),
            new CorvetteFreewpc(),
            new CreatureFromTheBlackLagoon(),
            new DemolitionMan(),
            new DemolitionManFreewpc(),
            new DirtyHarry(),
            new DrDude(),
            //new drWho(),
            //new fishTales(),
            //new funhouse(),
            //new funhouseFreeWpc(),
            //new gilligansIsland(),
            //new harly(),
            //new highSpeed2TheGetaway(),
            //new hotShotBasketball(),
            //new hurricane(),
            //new indianapolis500(),
            new IndianaJonesThePinballAdventure(),
            //new jackBot(),
            //new judgeDredd(),
            //new junkYard(),
            //new johnnyMnemonic(),
            //new leagueChamp(),
            //new monsterBash(),
            new MedievalMadness(),
            //new nbaFastbreak(),
            //new noGoodGofers(),
            //new noFear(),
            //new popeyeSavesTheEarth(),
            //new redTedsRoadShow(),
            //new safeCracker(),
            //new scaredStiff(),
            //new slugFest(),
            //new starTrekTheNextGeneration(),
            //new strikeMaster(),
            //new talesOfTheArabianNights(),
            //new theMachineBrideOfPinbot(),
            //new terminator2Freewpc(),
            new Terminator2(),
            //new theatreOfMagic(),
            new TheAddamsFamilySpecial(),
            //new theChampionPub(),
            //new theFlintstones(),
            //new thePartyZone(),
            //new theShadow(),
            //new ticketTacToe(),
            new TwilightZone(),
            //new twilightZoneFreewpc(),
            //new whiteWater(),
            //new whiteWaterFreewpc(),
            //new whoDunnit(),
            //new worldCupSoccer(),
            //new worldCupSoccerFreewpc(),
            //new wpcTestrom(),
            //new wpcAlphaTestrom(),
            //new wpc95Testrom(),
            //new wpcSTestrom(),
            //new uploadWpcAlpha(),
            //new uploadWpc(),
            //new uploadWpcFlip(),
            //new uploadWpcS(),
            //new uploadWpc95(),
        };

        public static string[] getAllNames()
        {
            return wpcGames
                .Where(entry => entry.rom != null && entry.rom?.u06 != null)
                .Select(entry => entry.name)
                .OrderBy(name => name)
                .ToArray();
        }

        public static IDb getByName(string name)
        {
            return wpcGames.FirstOrDefault(entry => entry.name == name);
        }

        public static IDb getByPinmameName(string filename)
        {
            return wpcGames.FirstOrDefault(entry =>
            {
                return entry.pinmame != null && Array.IndexOf(entry.pinmame?.knownNames, filename.ToLower()) != -1;
            });
        }
    }
}