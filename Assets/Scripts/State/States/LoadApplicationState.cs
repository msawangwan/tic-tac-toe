﻿using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;

public class LoadApplicationState :  IState {
    private float loadTime = 0;
    private float currentStopwatchTick = 0;
    private Utility stopwatch;

    private bool isInitialised = false;

    public bool isStateExecuting { get; private set; }
    public bool isStateExit { get; private set; }

    public LoadApplicationState() {
        isStateExecuting = true;
        loadTime = 3f;
        float interval = 1.2f;
        stopwatch = new Utility ( interval );
    }

    public void EnterState ( ) {
        Debug.Log ( "[LoadApplicationState][EnterState] Entering state ... " );
    }

    public void ExecuteState() {
        if ( isInitialised == false ) {
            stopwatch.Timer ( ); // do a little counting
            currentStopwatchTick = stopwatch.timer_tickCount;
            if ( currentStopwatchTick > loadTime ) {
                isInitialised = true;
                isStateExecuting = false; ;
                HandleOnApplicationLoadComplete ( );
            }
        }   
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void HandleOnApplicationLoadComplete() {
        IState nextState = new MainMenuState();
        Debug.Log ( "[LoadApplicationState][HandleOnApplicationLoadComplete] Next state: " + nextState.GetType ( ) );
        IStateTransition transition = new ExitLoadingTransition();
        Debug.Log ( "[LoadApplicationState][HandleOnApplicationLoadComplete] Transition: " + transition.GetType ( ) );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        Debug.Log ( "[LoadApplicationState][HandleOnApplicationLoadComplete] ExitEvent: " + exitEvent.GetType ( ) );
        // yield wait for end of frame??
        StartStateTransition ( exitEvent );
    }
}