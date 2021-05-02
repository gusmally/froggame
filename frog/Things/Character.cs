using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace frog.Things
{
    public class Character
    {
        public string Name { get; }
        public (Pronoun main, Dictionary<PronounType, string> dict) Pronouns { get; set; }
        public Texture2D SmallSprite { get; }
        public Texture2D LargeSprite { get; }
        public Vector2 Position { get; }
        public Occupation Occupation { get; set; }

        public Character(string name,
                         Pronoun pronoun,
                         Texture2D smallSprite,
                         Texture2D largeSprite,
                         Occupation occupation = null)
        {
            this.Name = name;
            this.SmallSprite = smallSprite;
            this.LargeSprite = largeSprite;
            this.Position = Vector2.Zero;
            this.Occupation = occupation;

            switch (pronoun)
            {
                case Pronoun.Custom: // define this later
                    break;
                case Pronoun.He:
                    var hePronouns = new Dictionary<PronounType, string>();
                    hePronouns.Add(PronounType.Subject, "he");
                    hePronouns.Add(PronounType.Object, "him");
                    hePronouns.Add(PronounType.PossessiveAdjective, "his");
                    hePronouns.Add(PronounType.Possessive, "his");
                    hePronouns.Add(PronounType.Reflexive, "himself");
                    this.Pronouns = (Pronoun.He, hePronouns);
                    break;
                case Pronoun.She:
                    var shePronouns = new Dictionary<PronounType, string>();
                    shePronouns.Add(PronounType.Subject, "she");
                    shePronouns.Add(PronounType.Object, "her");
                    shePronouns.Add(PronounType.PossessiveAdjective, "her");
                    shePronouns.Add(PronounType.Possessive, "hers");
                    shePronouns.Add(PronounType.Reflexive, "herself");
                    this.Pronouns = (Pronoun.She, shePronouns);
                    break;
                case Pronoun.They:
                    var theyPronouns = new Dictionary<PronounType, string>();
                    theyPronouns.Add(PronounType.Subject, "they");
                    theyPronouns.Add(PronounType.Object, "them");
                    theyPronouns.Add(PronounType.PossessiveAdjective, "their");
                    theyPronouns.Add(PronounType.Possessive, "theirs");
                    theyPronouns.Add(PronounType.Reflexive, "themself");
                    this.Pronouns = (Pronoun.They, theyPronouns);
                    break;

            }
        }
    }
}
