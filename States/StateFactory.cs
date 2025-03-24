public class StateFactory
{
    StateMachine machine;
    public StateFactory(StateMachine machine)
    {
        this.machine = machine;
    }


    public State Falling()
    {
        return new Falling(machine, this);
    }

    public State Walking()
    {
        return new Walking(machine, this);
    }

    public State Jumping()
    {
        return new Jumping(machine, this);
    }

    public State Dead()
    {
        return new Dead(machine, this);
    }
}
