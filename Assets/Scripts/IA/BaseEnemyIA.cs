﻿using UnityEngine;
using System.Collections;

public class BaseEnemyIA : BTExecuter
{
    private Blackboard bb;

    public override void BTCreator()
    {
        BTRepeater repeater = new BTRepeater();
        BTSelector selector1 = new BTSelector();
        BTSequence sequencer1 = new BTSequence();
        BTSelector selector2 = new BTSelector();
        BTSequence sequencer2 = new BTSequence();
        BTSequence sequencer3 = new BTSequence();
        BTInverter inverter1 = new BTInverter();
        BTInverter inverter2 = new BTInverter();
        BTInverter inverter3 = new BTInverter();


        repeater.AddChild(selector1);
        selector1.AddChild(inverter2);
        selector1.AddChild(sequencer1);
        selector1.AddChild(inverter3);
        inverter3.AddChild(new ApplyStatusEffect());
        inverter2.AddChild(new DiscountTurn());
        selector1.AddChild(new NextTurn());

        sequencer1.AddChild(new CheckTurn());
        sequencer1.AddChild(selector2);
        sequencer1.AddChild(new ApplyStatusEffect());
        sequencer1.AddChild(new PrepareNextTurn());
        sequencer1.AddChild(new NextTurn());

        selector2.AddChild(sequencer3);
        selector2.AddChild(sequencer2);
        //selector2.AddChild(new BTSucceeder());
        sequencer2.AddChild(inverter1);
        sequencer2.AddChild(new WhereToGo());
        sequencer2.AddChild(new Move());
        sequencer2.AddChild(new EnemyAttack());

        sequencer3.AddChild(new CheckSkillHit());
        sequencer3.AddChild(new UseSkill());

        inverter1.AddChild(new Surrounded());

        rootNode = repeater;

    }

    public override Blackboard BlackboardCreator()
    {
        bb = new Blackboard();
        bb.SetObject("heroList", BattleList.instance.GetHeroes());
        bb.SetObject("enemiesList", BattleList.instance.GetEnemies());
        bb.SetObject("turnNumber", GetComponent<Enemy>()._turnNumber);
        bb.SetObject("turn", GetComponent<Enemy>()._turn);
        bb.SetObject("enemy", GetComponent<BaseCharacter>());
        bb.SetObject("indexSkill", 0);
        return bb;
    }

    public override void BTUpdate()
    {
        bb.SetObject("turn", GetComponent<Enemy>()._turn);
        //GetComponent<Enemy>()._turn = (int)bb.GetObject("turn");
    }
}