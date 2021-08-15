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
            new DrWho(),
            new FishTales(),
            new Funhouse(),
            new FunhouseFreeWpc(),
            new GilligansIsland(),
            new Harly(),
            new HighSpeed2TheGetaway(),
            new HotShotBasketball(),
            new Hurricane(),
            new Indianapolis500(),
            new IndianaJonesThePinballAdventure(),
            new JackBot(),
            new JudgeDredd(),
            new JunkYard(),
            new JohnnyMnemonic(),
            new LeagueChamp(),
            new MonsterBash(),
            new MedievalMadness(),
            new NBAFastbreak(),
            new NoGoodGofers(),
            new NoFear(),
            new PopeyeSavesTheEarth(),
            new RedTedsRoadShow(),
            new SafeCracker(),
            new ScaredStiff(),
            new SlugFest(),
            new StarTrekTheNextGeneration(),
            new StrikeMaster(),
            new TalesOfTheArabianNights(),
            new TheMachineBrideOfPinbot(),
            new Terminator2Freewpc(),
            new Terminator2(),
            new TheatreOfMagic(),
            new TheAddamsFamilySpecial(),
            new TheChampionPub(),
            new TheFlintstones(),
            new ThePartyZone(),
            new TheShadow(),
            new TicketTacToe(),
            new TwilightZone(),
            new TwilightZoneFreewpc(),
            new WhiteWater(),
            new WhiteWaterFreewpc(),
            new WhoDunnit(),
            new WorldCupSoccer(),
            new WorldCupSoccerFreewpc(),
            new WpcTestrom(),
            new WpcAlphaTestrom(),
            new Wpc95Testrom(),
            new WpcSTestrom(),
            new UploadWpcAlpha(),
            new UploadWpc(),
            new UploadWpcFlip(),
            new UploadWpcS(),
            new UploadWpc95(),
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
                return entry.pinmame != null && Array.Exists(entry.pinmame?.knownNames, name => name == filename.ToLower());
            });
        }
    }
}