namespace AuthorName.DemoMod;
internal sealed class AutododgeLeftNextTurnManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public AutododgeLeftNextTurnManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        /* Here we tell it what to do. Since it's a 'next turn gain something', we can also use this moment to do that something */
        if (status != Instance.AutododgeLeftNextTurn.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
        {
            combat.QueueImmediate(new AStatus()
            {
                status = Status.autododgeLeft,
                statusAmount = amount,
                targetPlayer = ship.isPlayerShip,
                timer = 0
            });
            amount = 0;
        }
        return false;
    }
}