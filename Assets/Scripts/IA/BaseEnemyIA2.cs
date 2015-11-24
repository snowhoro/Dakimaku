using UnityEngine;
using System.Collections;

public class BaseEnemyIA2 : BTExecuter
{
    private Blackboard bb;

    public override void BTCreator()
    {
        BTRepeater repeater = new BTRepeater();
        BTSelector selector1 = new BTSelector();
        BTSequence sequencer1 = new BTSequence();
        BTSequence sequencer2 = new BTSequence();
        BTInverter inverter1 = new BTInverter();
        BTInverter inverter2 = new BTInverter();


        repeater.AddChild(selector1);
        selector1.AddChild(inverter1);
        selector1.AddChild(sequencer1);
        selector1.AddChild(inverter2);
        selector1.AddChild(new NextTurn());

        inverter1.AddChild(new DiscountTurn());
        inverter2.AddChild(new ApplyStatusEffect());
        
        sequencer1.AddChild(new CheckTurn());
        sequencer1.AddChild(sequencer2);
        sequencer1.AddChild(new ApplyStatusEffect());
        sequencer1.AddChild(new PrepareNextTurn());
        sequencer1.AddChild(new NextTurn());

        sequencer2.AddChild(new StayAndAttack());
        //sequencer2.AddChild(new MoveToAttack());
        sequencer2.AddChild(new MoveToClose());
        sequencer2.AddChild(new CheckAllSkills());
        sequencer2.AddChild(new Move2());
        sequencer2.AddChild(new UseSkill2());
        sequencer2.AddChild(new EnemyAttack2());

        rootNode = repeater;
    }

    public override Blackboard BlackboardCreator()
    {
        bb = new Blackboard();
        bb.SetObject("heroList", BattleList.instance.GetHeroes());
        bb.SetObject("enemiesList", BattleList.instance.GetEnemies());
        bb.SetObject("turnNumber", GetComponent<Enemy>()._turnNumber);
        bb.SetObject("turn", GetComponent<Enemy>()._turn);
        bb.SetObject("enemy", GetComponent<Enemy>());
        return bb;
    }

    public override void BTUpdate()
    {
        bb.SetObject("turn", GetComponent<Enemy>()._turn);
        //GetComponent<Enemy>()._turn = (int)bb.GetObject("turn");
    }
}