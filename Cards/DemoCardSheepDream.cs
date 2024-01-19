using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AuthorName.DemoMod.Cards;

internal sealed class DemoCardSheepDream : Card, IDemoCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SheepDream", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SheepDream", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 0,
            exhaust = true,
            /* In a similar manner to how we localized card names, we'll localize their descriptions
             * For example, if Sheep Dream is upgraded to B, this description would try getting information from card > SheepDream > Description > B in the locale file */
            description = ModEntry.Instance.Localizations.Localize(["card", "SheepDream", "description", upgrade.ToString()])
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 3,
                        /* We can add a status to the attack. This status will be applied if the attack hits a ship part, and Jupiter drones will also copy these when copying an AAttack
                         * To remember: AStatus applies a status to the enemy no matter where they are, but an AAttack with a status attached will only apply it if it hits
                         * In this case, we'll give the enemy 1 stack of Boost, so the next status they gain will get +1, sppoky!
                         * Note that Boost no longer gets used up by Status.shield or Status.tempShield. This change was implemented in the 1.0.2 patch */
                        status = Status.boost,
                        statusAmount = 1,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 4,
                        status = Status.boost,
                        statusAmount = 1,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 5,
                        status = Status.boost,
                        statusAmount = 2,
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
