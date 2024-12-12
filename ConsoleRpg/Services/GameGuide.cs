using ConsoleRpg.Helpers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Services
{
    public class GameGuide
    {
        private readonly OutputManager _outputManager;
        private Table _logTable;
        private Panel _mapPanel;

        public GameGuide(OutputManager outputManager)
        {
            _outputManager = outputManager;
        }

        public void ChapterIndex()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Select what you to learn. To return to the previous menu, please input 4.");
                _outputManager.AddLogEntry("1. Races");
                _outputManager.AddLogEntry("2. Classes");
                _outputManager.AddLogEntry("3. Abilities");
                _outputManager.AddLogEntry("4. Quit");
                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        RaceChapters();
                        break;
                    case "2":
                        ClassChapters();
                        break;
                    case "3":
                        AbilityChapters();
                        break;
                    case "4":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose one of the numbers listed.");
                        break;
                }

                if (input == "4")
                {
                    break;
                }
            }
        }

        private void RaceChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("1. Angels");
                _outputManager.AddLogEntry("2. Demons");
                _outputManager.AddLogEntry("3. Dwarves");
                _outputManager.AddLogEntry("4. The Fey");
                _outputManager.AddLogEntry("5. Golems");
                _outputManager.AddLogEntry("6. Hobbits");
                _outputManager.AddLogEntry("7. Humans");
                _outputManager.AddLogEntry("8. Hybrids");
                _outputManager.AddLogEntry("9. Kitsune");
                _outputManager.AddLogEntry("10. Kumiho");
                _outputManager.AddLogEntry("11. Ogres");
                _outputManager.AddLogEntry("12. Oni");
                _outputManager.AddLogEntry("13. Revenants");
                _outputManager.AddLogEntry("14. Therianthropes");
                _outputManager.AddLogEntry("15. Trolls");
                _outputManager.AddLogEntry("16. Vampires");
                _outputManager.AddLogEntry("17. Exit");
                _outputManager.AddLogEntry("Select which race you want to learn about. To return to the chapter index, input 17.");
                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        AngelChapter();
                        break;
                    case "2":
                        DemonChapters();
                        break;
                    case "3":
                        DwarfChapter();
                        break;
                    case "4":
                        FeyChapters();
                        break;
                    case "5":
                        GolemChapter();
                        break;
                    case "6":
                        HobbitChapter();
                        break;
                    case "7":
                        HumanChapter();
                        break;
                    case "8":
                        HybridRaceChapters();
                        break;
                    case "9":
                        KitsuneChapter();
                        break;
                    case "10":
                        KumihoChapter();
                        break;
                    case "11":
                        OgreChapter();
                        break;
                    case "12":
                        OniChapter();
                        break;
                    case "13":
                        RevenantChapter();
                        break;
                    case "14":
                        TherianthropeChapters();
                        break;
                    case "15":
                        TrollChapter();
                        break;
                    case "16":
                        VampireChapter();
                        break;
                    case "17":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose a number between 0 and 18.");
                        break;
                }

                if (input == "18")
                {
                    break;
                }
            }
        }

        private void ClassChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("1. Artificer");
                _outputManager.AddLogEntry("2. Barbarian");
                _outputManager.AddLogEntry("3. Bard");
                _outputManager.AddLogEntry("4. Cleric");
                _outputManager.AddLogEntry("5. Druid");
                _outputManager.AddLogEntry("6. Fighter");
                _outputManager.AddLogEntry("7. Monk");
                _outputManager.AddLogEntry("8. Paladin");
                _outputManager.AddLogEntry("9. Ranger");
                _outputManager.AddLogEntry("10. Revenant");
                _outputManager.AddLogEntry("11. Rogue");
                _outputManager.AddLogEntry("12. Sorcerer");
                _outputManager.AddLogEntry("13. Therianthrope");
                _outputManager.AddLogEntry("14. Vampire");
                _outputManager.AddLogEntry("15. Warlock");
                _outputManager.AddLogEntry("16. Wizard");
                _outputManager.AddLogEntry("17. Exit");
                _outputManager.AddLogEntry("Select which class you want to learn about. To return to the chapter index, input 17.");
                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        ArtificerChapter();
                        break;
                    case "2":
                        BarbarianChapter();
                        break;
                    case "3":
                        BardChapter();
                        break;
                    case "4":
                        ClericChapter();
                        break;
                    case "5":
                        DruidChapter();
                        break;
                    case "6":
                        FighterChapter();
                        break;
                    case "7":
                        MonkChapter();
                        break;
                    case "9":
                        PaladinChapter();
                        break;
                    case "10":
                        RangerChapter();
                        break;
                    case "11":
                        RevenantChapter();
                        break;
                    case "12":
                        RogueChapter();
                        break;
                    case "13":
                        SorcererChapter();
                        break;
                    case "14":
                        TherianthropeChapters();
                        break;
                    case "15":
                        VampireChapter();
                        break;
                    case "16":
                        WarlockChapter();
                        break;
                    case "17":
                        WizardChapter();
                        break;
                    case "18":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose a number between 0 and 19.");
                        break;
                }

                if (input == "18")
                {
                    break;
                }
            }
        }

        private void AngelChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Angels are the personal servants of the gods, acting on behalf of their progrenitors in order to carry out their will on the mortal plane. Due the varied and wildly differing personalities within the pantheon, the true forms of anegls take on a wide variety of forms that suit the domains of their creator god. However, as shapeshifters, they have become more widely known for taking the form of beautiful humans with bird wings sprouting from their backs and being garbed in robes.");
                _outputManager.AddLogEntry("Input 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void DemonChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("\"Demon\" is the name given to the myriad races of creatures that reside in Hell. While those born in Hell itself are simply amoral beings born from the collective unconscious of mortal races, non-deific beings who are particularly vile and wicked enough can transform into a demon. Even the mighty angels have some of their number that have been cast out from Heaven, becoming demons. Extremely powerful demons are known to carve territories in Hell, ruling them as Overlords with massive conscripted armies of other demons and damned souls.");
                _outputManager.AddLogEntry("Input 1 to continue.");
                var inputOne = _outputManager.GetUserInput("Continue:");

                switch (inputOne)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (inputOne == "1")
                {
                    break;
                }
            }

            while (true)
            {
                _outputManager.AddLogEntry("1. Balrogs");
                _outputManager.AddLogEntry("2. The Fallen");
                _outputManager.AddLogEntry("3. Ghouls");
                _outputManager.AddLogEntry("4. Rakshasa");
                _outputManager.AddLogEntry("5. Exit");
                _outputManager.AddLogEntry("Select a demon race to learn about. To return to the race chapter index, input 5.");
                var inputTwo = _outputManager.GetUserInput("Selection:");

                switch (inputTwo)
                {
                    case "1":
                        BalrogChapter();
                        break;
                    case "2":
                        FallenChapter();
                        break;
                    case "3":
                        GhoulChapter();
                        break;
                    case "4":
                        RakshasaChapter();
                        break;
                    case "5":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input a number between 0 and 6.");
                        break;
                }

                if (inputTwo == "5")
                {
                    break;
                }
            }
        }

        private void BalrogChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Balrogs are the most powerful natural race of demons, surpassed only by the Fallen themselves. Because of this, represnted the majority of Overlords before most of their positions were taken by the Fallen, those usurped either killed or forced to serve under them. The true form of a Balrog is that of a black, hulking horned beast with giant, bat-like wings able to exude flames from its body. Balrogs, like the Fallen, are proud creatures. Unlike them, they are able to be swayed on pragmatic matters and more prone ignore \"lesser\" creatures, but are also more brutish in their methods and quicker to anger.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FallenChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Angels may be the specially-created aervants of the gods, but that doesn't make them any less infallible than the mortals below them. Many a prideful and power hungry angel has tried and failed to usurp their god's position in the pantheon, only for their efforts to end in failure and be cast down from Heaven into Hell as a result. Those that survive its unforgiving environment become influenced by Hell's nature to become blacked and decayed. These are what are known as the Fallen, the current Overlords of Hell who forever seek to corrupt mortals as well to bolster their vast armies to fight in their petty conflicts.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void GhoulChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Revenants are forever doomed to be tied to their fetters until they manage to come to terms with their death. Some learn to heal, others become consumed with obsession, and still others are turned into pawns for the Fallen. Demonic minions of these Overlords are known to offer deals to revenants in helping to destroy their fetters in exchange for servitude to their master. Those revenants who accept this help are then added to the Overlord\'s army, eventually being transformed by the influence of Hell and their Fallen master into a ghoul. These revenants are granted an undying, immortal body with all the strength of their original undead state, but are tied to servitude by a ravenous appetite dor the flesh of the living.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void RakshasaChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Rakshasa are gluttonous demons whose true form resembles that of a bestial human with traits of that of a tiger, such as claws, sharp teeth, a feline-seeming head, and orange skin. They are well known for consuming mortals like the ghouls, but whereas the latter do it out of necessity to stiate an endless hunger, the rakshasa do so out of sadism and a genuine love for the taste of flesh. However, Rakshasa also possess a craftier side to them, those serving a Fallen often tempting mortals away from a more vituous path in order turn them into more soldiers for their master once they end up in Hell.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void DwarfChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Dwarves are a race of human with a short & stout stature, but also possess hardy skeletons and more powerful muscles. Dwarves are well known for both their propensity for all forms of engineering, but also their constant mining for both material & valuable gems. As such, most of their settlements reside in and on top of mountains.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FeyChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("The fey are a broad categorization of magical creatures infamous for their mischevious behavior, their odd moral sensibilities, and their enormous sense of pride. The latter is especially loathed about them, as they are well known to seek petty retribution for the most minor of slights. This trait is worsened by the fact that the fey wield a vast amount of magical power that only comes up short to the divine or hellish, allowing them to perform more outlandish feats of magic. There is also no true base appearance for a fey, thiugh they are usually depicted as lean & lithe humans with the wings of insects sprouting from their backs. The only known weakness of the fey is that the touch of cold iron is caustic to their skin.");
                _outputManager.AddLogEntry("\nInput 1 to continue.");
                var inputOne = _outputManager.GetUserInput("Continue:");

                switch (inputOne)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (inputOne == "1")
                {
                    break;
                }
            }

            while (true)
            {
                _outputManager.AddLogEntry("1. Changelings");
                _outputManager.AddLogEntry("2. Faeries");
                _outputManager.AddLogEntry("3. Fairies");
                _outputManager.AddLogEntry("4. Exit");
                _outputManager.AddLogEntry("Select a fey race to learn about. To return to the race chapter index, input 4.");
                var inputTwo = _outputManager.GetUserInput("Selection:");

                switch (inputTwo)
                {
                    case "1":
                        ChangelingChapter();
                        break;
                    case "2":
                        FaeryChapter();
                        break;
                    case "3":
                        FairyChapter();
                        break;
                    case "4":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input a number between 0 and 5.");
                        break;
                }

                if (inputTwo == "4")
                {
                    break;
                }
            }
        }

        private void ChangelingChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Changelings are fae who have been placed in the homes of human children, having been made to take the place of one of said children as an infant by their birth parents. While they have limited access to fey magic in their imposed disguises, these same disguises often of deformities that can easily be mistaken for simple mutation.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FaeryChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Faeries are one of the two races of fey. They are the more vindictive and brutal of the two races, bestowing fates worth than death to those who have wronged them in some perceived way. They also possess almost completely alien thought processes and moral principles, resulting in them being almost entirely unpredictable to what can set any one of them off. However, what is most definitely foul of them is that they are also known to abduct anyone who gets lost in their forests, with any number of unspeakable acts done to them, only to be released soon after with only haunting nightmares as their rememberance of the tortures endured.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FairyChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Fairies are one of the two races of fey. While they are more benevolent and reasonable than faeries, they still possess an inflated sense of ego, are greatly mischievous, and their massive amount of magical power. Despite this, they are mell more known for being helpful towards mortals if it suits their fancies and even been to known to copulate and form long-lasting relationships with them. These bonds with humans in particular are what gave rise to the race of elves.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void GolemChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Golem are a type of large humanoid robot powered by magic, allowing them to cast spells, and possess a strong A.I. as their central consciousness, being able to perform tasks and solve problems without the assistance of a mortal. Most industrial races are known to produce golems, except for the elves.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HobbitChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Hobbits are a race of humans who are even smaller than dwarves and have thick, hairy feet. They live longer lives than humans and are well known for their carefree & hedonistic attitudes.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HumanChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Good old Homo sapiens. Humanity manages to stand out in a dangerous and diverse magical world by being the most technologically advanced of all races, having finally advanced enough to create super A.I.s. However, this isn't universal among their nations, with many just as easily still being stuck in Middle Age-style feudal systems due to sharing territory with multiple enemy races. Also, the Knights of the Round Table were real and they're back. Don't know what that's about.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HybridRaceChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("There are a myriad of races that are descended from both humans and some ther second race. In ancient times, any created were the result of either the Coldblood Hegemony\'s genetics technology or the supernatural nature of the second parent. Nowadays, humans have genetically augmented themselves to the point of both sexes being able to breed with most other human-derived races. The only exception to this, for some reason, are the orcs.");
                _outputManager.AddLogEntry("Input 1 to continue.");
                var inputOne = _outputManager.GetUserInput("Continue:");

                switch (inputOne)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (inputOne == "1")
                {
                    break;
                }
            }

            while (true)
            {
                _outputManager.AddLogEntry("1. Cambions");
                _outputManager.AddLogEntry("2. Draconians");
                _outputManager.AddLogEntry("3. Elves");
                _outputManager.AddLogEntry("4. Garudas");
                _outputManager.AddLogEntry("5. Goblins");
                _outputManager.AddLogEntry("6. Half-Dwarves");
                _outputManager.AddLogEntry("7. Half-Elves");
                _outputManager.AddLogEntry("8. Half-Goblins");
                _outputManager.AddLogEntry("9. Half-Ogres");
                _outputManager.AddLogEntry("10. Half-Trolls");
                _outputManager.AddLogEntry("11. Lamias");
                _outputManager.AddLogEntry("12. Mermen");
                _outputManager.AddLogEntry("13. Minotaurs");
                _outputManager.AddLogEntry("14. Nagas");
                _outputManager.AddLogEntry("15. Orcs");
                _outputManager.AddLogEntry("16. Saurians");
                _outputManager.AddLogEntry("17. Exit");
                _outputManager.AddLogEntry("Select a hybrid race to learn about. To return to the race chapter index, input 17.");

                var inputTwo = _outputManager.GetUserInput("Selection:");
                switch (inputTwo)
                {
                    case "1":
                        CambionChapter();
                        break;
                    case "2":
                        DraconianChapter();
                        break;
                    case "3":
                        ElfChapter();
                        break;
                    case "4":
                        GarudaChapter();
                        break;
                    case "5":
                        GoblinChapter();
                        break;
                    case "6":
                        HalfDwarfChapter();
                        break;
                    case "7":
                        HalfElfChapter();
                        break;
                    case "8":
                        HalfGoblinChapter();
                        break;
                    case "9":
                        HalfOgreChapter();
                        break;
                    case "10":
                        HalfTrollChapter();
                        break;
                    case "11":
                        LamiaChapter();
                        break;
                    case "12":
                        MermanChapter();
                        break;
                    case "13":
                        MinotaurChapter();
                        break;
                    case "14":
                        NagaChapter();
                        break;
                    case "15":
                        OrcChapters();
                        break;
                    case "16":
                        SaurianChapter();
                        break;
                    case "17":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input a number between 0 and 18.");
                        break;
                }

                if (inputTwo == "17")
                {
                    break;
                }
            }
        }

        private void CambionChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Cambions are the result of demons copulating with humans. Their magic is not as great as their demon forebearers, but they inherit traits from whatever subspecies of demon their parent is.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void DraconianChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Draconians are a race of half-human, half-dragons created during the Age of Scales. Resembling humans with the proportional heads, necks, tails, and sometimes even wings of dragons; Draconians can not only breath fire and possess greater strength, but can use the innate magic of their draconic heritage to cast spells.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void ElfChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Elves are the result of fairies copulating with humans, marked by their tall & lean stature, as well as their pointed ears. A race of immortals, elf society is well known for being both intellectual and philosophical, but they also possess strong emotions that they work rigorously to maintain control over.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void GarudaChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Garudas are one of the races of Beastmen that came into being in the wake of the Age of Scales. They are half-human and half-eagle, possessing talons on the end of each finger and toe, as well as an elongated finger on each hand and long feathers on their arms that allow them to also use their arms as functioning wings.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void GoblinChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Goblins are a heavily mutated form of elf that are green in color, possess larger ears and longer noses, are short in stature, and are infamous for their unburdened and wild nature. While no less intelligent than any other elf, it is mostly dedicated to whatever craft suits their fancy in the moment, often at the risk of others if not themselves.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HalfDwarfChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Half-dwarves are the children of a human and a dwarf. They are only somewhat shorter than the average human and aren't as strong or sturdy as dwarves, but aren't as unflinchably stubborn as the latter.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HalfElfChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Half-elves are the children of a human and a elf. They aren't as lithe and agile as regular elves and possess even less fay magic, but they are still extremely long-lived for a mortal being and don't have as much emotional intensity as their elf parents.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HalfGoblinChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Half-goblins are the children of a human and a goblin. Their more goblin-esque features are more subdued than in regular goblins, possess a similar aptitude for machinery and tricks, and their skin is a tan-ish mix of green and pink. This combinations results in a just as wild intelligence, if a lot more focused and patient, being known to both banter with their enemies, execute long-term plans & gambits, and develop exotic weaponry for their purposes.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HalfOgreChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Half-ogres are the children of a human and an ogre. The more bestial features of the ogre are more subdued and they don't possess appetites nearly as voracious. However, they still possess intelligence and maturaity on par with children, thus adoptive parents or other guardians often have to keep them under supervision.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HalfTrollChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Half-trolls are the children of a human and a troll. The more bestial features of the troll are more subdued and the troll's infamous regenerative ability is not as effective. However, half-troll eyes are more easily able to withstand sunlight, though only in small amounts.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void LamiaChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Lamia are the children of a human and a naga. They resemble humans whose lower half instead ends in a long snake-like tail. Lamia are infamous for consuming children, due to a curse put on them for the hubris of a lamia queen, and don't possess as great a connection to Vitar, the Serpent God, as their naga parents.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void MermanChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Mermen are a race of half-human, half-fish created during the Age of Scales. They resemble humans whose lower half instead ends in the tail in a species of fish, there being multiple lineages spanning the vast majority of fish species. As they possess posterior fins in place of legs, Mermen require mechanical limbs in order to function on land, having both lungs and gills.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void MinotaurChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Minotaurs are one of the races of Beastmen that came into being in the wake of the Age of Scales. They are half-human and half-cattle, possessing the proportionally-sized hind legs, heads, and tails of cattle. Large in stature, Minotaurs can be just as temperamental as any bull.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void NagaChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Nagas are a race of half-human, half-snakes created during the Age of Scales. They possess the upper torso of humans and the proportionally-sized head, neck, and tail of a snake. Nagas were created with an ethereal connection to the Vitar, Serpent God, able to receive instructions from and commune with her. The snake half of a Naga can come from any species of snake.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void OrcChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("The Orcs are two races of elf which have been corrupted by Mrogar, the Boar God, into living engines of war. They are unique among the human-derived race of mortals for being the only ones that can't breed with true humans.");
                _outputManager.AddLogEntry("Input 1 to continue.");
                var inputOne = _outputManager.GetUserInput("Continue:");

                switch (inputOne)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (inputOne == "1")
                {
                    break;
                }
            }

            while (true)
            {
                _outputManager.AddLogEntry("1. Orroks");
                _outputManager.AddLogEntry("2. Uruks");
                _outputManager.AddLogEntry("3. Exit");
                _outputManager.AddLogEntry("Select an orc race to learn about. To return to the race chapter index, input 3.");
                var inputTwo = _outputManager.GetUserInput("Selection:");

                switch (inputTwo)
                {
                    case "1":
                        OrrokChapter();
                        break;
                    case "2":
                        UrukChapter();
                        break;
                    case "3":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input a number between 0 and 4.");
                        break;
                }

                if (inputTwo == "3")
                {
                    break;
                }
            }
        }

        private void OrrokChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Orroks are the quintessential warlike race and are the larger of the two races of Orc. Orroks are much larger and appear more bestial than their Uruk cousins and are also more bestial in appearance, having tusks growing from their large underbites. They also have a scientific and mechanical aptitude rivaling humanity itself, their magitech usually resembling ramshackle kitpieces. However, Orroks are also far more fun-loving and irrational than Uruks, bloodlust utterly consuming them in battle and possessing a love of war and combat that leads them to only be further emboldened to succeed in the face of defeat.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void UrukChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Uruks are the smaller of the two races of Orc. Uruks bear the most resemblence to their elf ancestors, but still possess physiological differences such as sharp teeth, longer arms, and a more pudgy and sickly appearance. However, they are known to flee when things go south for them, often being forced back into the fray by their Orrok cousins. They're also more self-aware than the Orroks, hating their own existence and using their constant warring as an outlet for their hatred. Conversely, Uruks aren't as smart as even the Orroks, on average only being marginally smarter than the average human. As such, their tech almost universally comes from either the Orroks or scavenged spoils of war.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void SaurianChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Saurians are a race of half-human, half-lizards created during the Age of Scales. They possess a humanoid body structure, clawed fingers; and the proportional heads, necks, hind legs, and tails of lizards. Saurians are typically cold and calculating, usually being highly pragmatic to the point of disregarding emotional connections. As a result, most usually live the life of mercenaries and don't possess many nations. The lizard half of a Saurian can come from any species of lizard.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("\nInvalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void KitsuneChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Kitsune are a race of spirits who primarily take the form of flesh-and-blood red foxes, distinguished by possessing nine tails instead of one. Despite this, they are known to be light-hearted tricksters able to disguise themselves as anything. Their most common form of disguise is that of a human woman in her 20s, regardless of actual gender. Unfortunately, kitsune can easily be discovered due to retaining features from their fox form.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void KumihoChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Kumiho are a race of spirits who resemble kitsune in appearance, abilities, and weaknesses. One can easily mistake them for their more benevolent counterparts, but doing so would be a grave error. Kumiho view themselves are inherintly superior to mortals and thus, are unrepentant in playing sick games with them that ultimately end in their death. They are also easily insulted, killing the would-be criticizer once the utterance leaves their mouth.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void OgreChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Ogres are a race of monstrous human that possess great strength and sharp teeth to coincide with their large size. Because of these adaptations, however, they're not as smart as their human counterparts, with child-like intelligence and mentality being considered average. The sole god in their most major religion is that of the Purinina God, venerating consumption as the greatest of life's pleasures.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void OniChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Oni are malevolent spirits that usually take the form of an orcish creature, possessing great tusks from their underbite, but also having great horns like that of a bull. They are known to dwell in caves on the tops of mountains and love to sneak up on unsuspecting travellers and take them back there to consume them.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void TherianthropeChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Known to themselves as the fera, therianthropes are mortals who have been cursed to transform into bestial versions of themselves, resembling humanoid predators. Enormously strong and fiercely protectful of their territory, therianthropes have earned their epithet as \"nature\'s wrath.\" They can spread their curse to whomever the manage to scratch, only the undead being immune to it, breeds can reproduce through more mundane means with each other, and their only known exploitable weakness is silver. It is a common misconception that therianthropes also only transform at night, thanks to the association of the moon with the night, when in truth they transform whenever a full moon is in the sky, even in the midst of daylight.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var inputOne = _outputManager.GetUserInput("Continue:");

                switch (inputOne)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (inputOne == "1")
                {
                    break;
                }
            }

            while (true)
            {
                _outputManager.AddLogEntry("1. Werebats");
                _outputManager.AddLogEntry("2. Werebears");
                _outputManager.AddLogEntry("3. Werecrocodiles");
                _outputManager.AddLogEntry("4. Weredolphins");
                _outputManager.AddLogEntry("5. Werehyenas");
                _outputManager.AddLogEntry("6. Werelions");
                _outputManager.AddLogEntry("7. Wereravens");
                _outputManager.AddLogEntry("8. Werescorpions");
                _outputManager.AddLogEntry("9. Weresharks");
                _outputManager.AddLogEntry("10. Weresnakes");
                _outputManager.AddLogEntry("11. Werespiders");
                _outputManager.AddLogEntry("12. Weretigers");
                _outputManager.AddLogEntry("13. Werewolves");
                _outputManager.AddLogEntry("14. Exit");
                _outputManager.AddLogEntry("Select a therianthrope breed to learn about. To return to the race chapter index, input 14.");
                var inputTwo = _outputManager.GetUserInput("Selection:");

                switch (inputTwo)
                {
                    case "1":
                        WerebatChapter();
                        break;
                    case "2":
                        WerebearChapter();
                        break;
                    case "3":
                        WerecrocodileChapter();
                        break;
                    case "4":
                        WeredolphinChapter();
                        break;
                    case "5":
                        WerehyenaChapter();
                        break;
                    case "6":
                        WerelionChapter();
                        break;
                    case "7":
                        WereravenChapter();
                        break;
                    case "8":
                        WerescorpionChapter();
                        break;
                    case "9":
                        WeresharkChapter();
                        break;
                    case "10":
                        WeresnakeChapter();
                        break;
                    case "11":
                        WerespiderChapter();
                        break;
                    case "12":
                        WeretigerChapter();
                        break;
                    case "13":
                        WerewolfChapter();
                        break;
                    case "14":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input a number between 0 and 15.");
                        break;
                }

                if (inputTwo == "14")
                {
                    break;
                }
            }
        }

        private void WerebatChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werebats, also known as chiropterathropes, are the two breeds of therianthrope capable of powered flight. They are also the only purely nocturnal breed, often picking off targets in the midst of dense forests and jungles. Werebats are specifically modelled after leaf-nosed bats, being the most well-known group of carnivorous bats.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerebearChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werebears, also known as ursathropes, are one of the physically strongest of therianthrope breeds. They tend occupy forested territories and are also more intelligent than most other therianthropes, owing to bears being overall the most intelligent group of carnivorans, and are often underestimated by monsters hunters because of their size & strength. Werebears are known to fight with werewolves over terriotory and food.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerecrocodileChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werecrocodiles, also known as crocodylathropes, are one of the two reptilian breeds of therianthropes. They are also one of the strongest and most dangerous, often ambushing trespassers to their river & lake territories from under the water. Werecrocodiles can also be surprisingly intelligent, being known to work together in packs if the need arises despite being mostly solitary hunters. As one of the therianthrope breeds not persecuted agaisnt in teh Age of Scales, werecrocodiles are derided along with the weresharks & weresnakes as standing back as the other breeds suffered under the Coldblood Hegemony.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WeredolphinChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Weredolphins, also known as delphinithropes, are one of the two marine breeds of therianthropes. People might at first scoff that a dolphin-based therianthrope can be much of a threat, forgetting that dolphins are both extremly intelligent and no less carnivorous than the more dread shark. Combined with echolocation, weredolphins like to play with their prey, sadistically toying with them before going in for the kill.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerehyenaChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werehyenas, also known as hyaenathropes, are one of the three feliform breeds of therianthropes. The popular view of hyenas as cowardly scavengers has been passed on to their therianthrope counterprtas, but this is far from the truth. Much like the animals they were based on, werehyenas are perfectly capable and willing to kill live prey. And thanks to the almost nocturnal nature of the therianthropes' transformation, too many an amateur and inexperienced hunter has underestimated and fallen prey to this breed. Werehyenas are often known to compete with werelions, being second only the wereolves as the latter's most sworn rivals.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerelionChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werelions, also known as leothropes, are one of the three feliform breeds of therianthropes. Their societies are often based on those of actual lion prides, with males guarding the pride\'s territory and females hunting prey to feed the pride. Werelions are also one of the more confrontational of the therianthrope breeds, getting into scuffles with various competing breeds. This particularly includes werehyenas, but they most especially are jealous of the werewolves are their reach and numerical superiority.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WereravenChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Wereravens, also known as corvothropes, are the two breeds of therianthrope capable of powered flight. As one of the most intelligent breeds, they more than often toy with their prey before going in for the kill. This also allows wereravens to assess situations on the fly and avoid traps laid out for them by monster hunters.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerescorpionChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werescorpions, also known as scorpithropes, one are the two breeds of arachnid therianthrope. While still possessing great strength and durability like the other breeds, they unfortunately aren't as smart as them. Werescorpions are more often than not raging berserkers, impetiously going in for the kill instead of assessing the situation beforehand. It is only their raw power and savagery that has allowed the werescorpions to endure alongside the other breeds.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WeresharkChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Weresharks, also known as selachithropes, are one the two breeds of marine therianthrope. Of the misconceptions mortals have around the animals that the therianthrope are based on, none have been more effected by this than the weresharks. Because of the negative reputation of sharks, mortals often believe that their reputations as vicious, blood-hungry killers was passed onto their therianthrope counterparts. In truth, weresharks will only attack non-cursed mortals if provoked, being the most docile towards them of the various breeds and are more oten than not curious. As one of the therianthrope breeds not persecuted agaisnt in the Age of Scales, weresharks are derided along with the werecrocodiles & weresnakes as standing back as the other breeds suffered under the Anapsid Hegemony.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WeresnakeChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Weresnakes, also known as ophidiothropes, are one the two breeds of reptilian therianthrope. They are the masters of ambush hunting, often springing forth from under the ground to catch unsuspecting prey unaware. Their long tails also allow weresnakes to bind their prey before they have the chance to escape for fight back. As one of the therianthrope breeds not persecuted agaisnt in the Age of Scales, weresnakes are derided along with the werecrocodiles & weresharks as standing back as the other breeds suffered under the Coldblood Hegemony.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerespiderChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werespiders, also known as araneaethropes, are one the two breeds of arachnid therianthrope. Being able to produce webbing, it is often employed by them not only for traps and detecting prey, but also for crafting makeshift weapons like clubs or crossbows. This combined with their dirty tactics have made werespiders some of the most crafty and dangerous therianthrope breeds, able to outsmart and kill monster hunters on a regular basis.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WeretigerChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Weretigers, also known as tigrithropes, are one of the three feliform breeds of therianthropes. They are undoubtibly the most vindictive and spiteful of the various breeds, often having laundry lists of grudges that they intend to settle in one way or another. They will even go so far as to track down and kill their targets in heavily-populated areas, even if that boldness easily puts their lives at stake. That said, weretigers are usually far from reckless, more than capable of using stealth to issue vengeance, some even making the kill when the full moon isn't in the sky if they think they can get away with it.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void TrollChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Trolls are a race of monstrous human with a lean stature that belies a great strength. They also possess sharp teeth and the ability to rapidly regenerate from any injury taken during battle. Being a subterranean race, troll eyes are super-sensitive to light, and thus have to wear specialized lenses to bear the presence of sunlight.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void ArtificerChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Masters of invention, artificers use ingenuity and magic to unlock extraordinary capabilities in objects. They see magic as a complex system waiting to be decoded and then harnessed in their spells and inventions. You can find everything you need to play one of these inventors in the next few sections.\n\nArtificers use a variety of tools to channel their arcane power. To cast a spell, an artificer might use alchemist's supplies to create a potent elixir, calligrapher's supplies to inscribe a sigil of power, or tinker's tools to craft a temporary charm. The magic of artificers is tied to their tools and their talents, and few other characters can produce the right tool for a job as well as an artificer.\n\nOrc artificers are called \"Mekboyz\" by Orroks.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void BarbarianChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("For some, their rage springs from a communion with fierce animal spirits. Others draw from a roiling reservoir of anger at a world full of pain. For every barbarian, rage is a power that fuels not just a battle frenzy but also uncanny reflexes, resilience, and feats of strength.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void BardChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Whether scholar, skald, or scoundrel, a bard weaves magic through words and music to inspire allies, demoralize foes, manipulate minds, create illusions, and even heal wounds. The bard is a master of song, speech, and the magic they contain.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void ClericChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Clerics are intermediaries between the mortal world and the distant planes of the gods. As varied as the gods they serve, clerics strive to embody the handiwork of their deities. No ordinary priest, a cleric is imbued with divine magic.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void DruidChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Whether calling on the elemental forces of nature or emulating the creatures of the animal world, druids are an embodiment of nature's resilience, cunning, and fury. They claim no mastery over nature, but see themselves as extensions of nature's indomitable will.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FighterChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Fighters share an unparalleled mastery with weapons and armor, and a thorough knowledge of the skills of combat. They are well acquainted with death, both meting it out and staring it defiantly in the face.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void MonkChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Monks are united in their ability to magically harness the energy that flows in their bodies. Whether channeled as a striking display of combat prowess or a subtler focus of defensive ability and speed, this energy infuses all that a monk does.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void PaladinChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Whether sworn before a god's altar and the witness of a priest, in a sacred glade before nature spirits and fey beings, or in a moment of desperation and grief with the dead as the only witness, a paladin's oath is a powerful bond.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void RangerChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Far from the bustle of cities and towns, past the hedges that shelter the most distant farms from the terrors of the wild, amid the dense-packed trees of trackless forests and across wide and empty plains, rangers keep their unending watch.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void RevenantChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Revenants are corporeal remains reanimated by a soul who died with strong and traumatic emotions, kept from passing onto the afterlife by the continued existence of the beings, objects, etc. responsible for said trauma, referred to as fetters. Being a soul piloting a rotting corpse, they have as much time to destroy their fetters should they choose to before the body decays into uselessness, being condemned to haunt the living as a ghost instead.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void RogueChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Rogues rely on skill, stealth, and their foes' vulnerabilities to get the upper hand in any situation. They have a knack for finding the solution to just about any problem, demonstrating a resourcefulness and versatility that is the cornerstone of any successful adventuring party.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void SorcererChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Sorcerers carry a magical birthright conferred upon them by an exotic bloodline, some otherworldly influence, or exposure to unknown cosmic forces. No one chooses sorcery; the power chooses the sorcerer.\n\nOrc sorcerers are lumped by Orroks into the category of \"Weirdboyz\" alongside warlocks & wizards.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void VampireChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Vampires are an artificial form of revenant created by necromancers. Instead of fetters, souls are tied to the physical world (and thus their body) through the use of their vitae, the form their blood takes on upon succumbing to vampirism. Vitae keeps the body from decaying and take the place of cells in healing the body, but there is a limited amount that can only be replaced by drinking the blood of mortals. Because of the hellish ritual used to create them, vampires possess both distinct magical abilities and rather inconvenient weaknesses.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WarlockChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Warlocks are seekers of the knowledge that lies hidden in the fabric of the multiverse. Through pacts made with mysterious beings of supernatural power, warlocks unlock magical effects both subtle and spectacular.\n\nOrc sorcerers are lumped by Orroks into the category of \"Weirdboyz\" alongside sorcerers & wizards.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WerewolfChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Werewolves, also known as lycanthropes, are the most prominent race of therianthrope, being both the most widespread and the most dominant among the various breeds. They are also infamous for being egotistic, believing themselves to be the most superior breed of therianthrope. This often ends with them coming to blows with other therianthropes, particularly werelions, just often as they do non-cursed mortals.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void WizardChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Wizards are supreme magic-users, defined and united as a class by the spells they cast. Drawing on the subtle weave of magic that permeates the cosmos, wizards cast spells of explosive fire, arcing lightning, subtle deception, brute-force mind control, and much more.\n\nOrc sorcerers are lumped by Orroks into the category of \"Weirdboyz\" alongside sorcerers & warlocks.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void AbilityChapters()
        {
            while (true)
            {
                _outputManager.AddLogEntry("1. Bite");
                _outputManager.AddLogEntry("2. Bubble");
                _outputManager.AddLogEntry("3. Drain");
                _outputManager.AddLogEntry("4. Fire");
                _outputManager.AddLogEntry("5. Heal");
                _outputManager.AddLogEntry("6. Lightning");
                _outputManager.AddLogEntry("7. Mist");
                _outputManager.AddLogEntry("8. Rage");
                _outputManager.AddLogEntry("9. Shove");
                _outputManager.AddLogEntry("10. Exit");
                _outputManager.AddLogEntry("Select which ability you want to learn about. To return to the chapter index, input 11.");
                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        BiteChapter();
                        break;
                    case "2":
                        BubbleChapter();
                        break;
                    case "3":
                        DrainChapter();
                        break;
                    case "4":
                        FireChapter();
                        break;
                    case "5":
                        HealChapter();
                        break;
                    case "6":
                        LightningChapter();
                        break;
                    case "7":
                        MistChapter();
                        break;
                    case "8":
                        RageChapter();
                        break;
                    case "9":
                        ShoveChapter();
                        break;
                    case "10":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose a number between 0 and 11.");
                        break;
                }

                if (input == "10")
                {
                    break;
                }
            }
        }

        private void BiteChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("The more bestial races are known to bite down on their opponents or prey, should they possess sharp teeth. Those derived from actual beasts are known to aim for the jugular. Vampires are also known to do this when intiating quick kills. Being able to assume the form of beasts, druids are also known to this.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void BubbleChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("One of the most common defensive spells is the ability to erect a dome of energy to shield one\' self from enemy attack. This forcefield will remain intact so long as the caster is able to concentrate, however, it will not protect from any intense heat in the vicinity, such as from a great fire.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void DrainChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("One of the more insidious spells is to strip a portion of a being's life away from them to be absorbed by the caster, much like a parasite. Vampire's have a more material equivalent, able to quickly draw blood from a victim into their fangs and into their body in order to recover in the midst of battle. The amount of life taken or blood drawn is dependent on how strong the afflicter is.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void FireChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Projecting fire is one of the most devastating of abilities. While this is one of the most common abilities among spellcasters, dragons and their cousins - such as draconians - are able to naturally produce it from within their bodies. Artificers, particularly engineers, are also known to create working flamethrowers to aid them in battle. Most races of demon can also able to produce hellfire due to their nature as beings of Hell.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void HealChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("One of the most signature mystical abilities of a cleric, thanks to their connection to their god, is the ability to heal one's self and others. Trolls also have the ability to regenerate rapidly from damage, healing from any injury that may have been incurred. How much these clerics and trolls heal from injury is dependent on their strength.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void LightningChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("A signature ability of spellcasters is the summon great blasts of lightning from their hands, though not nearly as potent as the real thing. One of the many artificer creations if the lightning cannon, a mechanical version of the spell projected from a rocket launcher-like device that is held the same way.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void MistChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Shapeshifters are able to turn into mist render physical attacks against them ineffective. This is also a common trick among vampires, whose own shapeshifting abilities are nowhere near as potent as those of others.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void RageChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("Barbarians are well known to go into a bloodthirsty rage in the middle of battle, their physical prowess bolstered by the adrenaline pumping through their veins. Therianthrope are also well known for this, letting their instincts take over when reason starts availing them. Orroks have a far more extreme version of this known as the \"WAAAGH,\" a mystical representation of their instinctual drive for war that accumulates the more Orroks are driven into this state.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }

        private void ShoveChapter()
        {
            while (true)
            {
                _outputManager.AddLogEntry("A basic move where the opponent is pushed back with a weapon, claws, or even sheer strength.");
                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }
    }
}