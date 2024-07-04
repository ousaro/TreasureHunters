using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState _stateData;

    protected bool _turnImmediately;
    protected bool _isPlayerInMinAgroRange;
    protected bool _isAllTurnsDone;
    protected bool _isAllTurnsTimeDone;

    protected float _lastTurnTime;

    protected int _amountOfTurnDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isPlayerInMinAgroRange = _entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        _turnImmediately = false;
        _isAllTurnsTimeDone = false;
        _isAllTurnsDone = false;

        _lastTurnTime = _startTime;
        _amountOfTurnDone = 0;

        _entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_turnImmediately)
        {
            _entity.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnDone++;
            _turnImmediately = false;
        }
        else if(Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && !_isAllTurnsDone)
        {
            _entity.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnDone++;
           
        }
        
        if(_amountOfTurnDone >= _stateData.amoutOfTurns)
        {
            _isAllTurnsDone = true;
        }

        if(Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && _isAllTurnsDone)
        {
            _isAllTurnsTimeDone = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
