﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : UserInterfaceMenu, IUIEvent {
    public MainMenu() : base() {
        buttonEvent = this;

        menuObject = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.mainMenu ) );
        menuObject.SetActive ( false );
        menuObject.transform.SetParent ( uiCanvasReference.transform, false );

        FindButtonsInChildren ( );
    }

    public event Action<StateBeginExitEvent> RaiseUIEvent;

    protected override void MapButtons ( ) {
        foreach ( Button btn in menuButtons ) {
            if ( btn.CompareTag ( TagsUI.startNewGameBtn ) ) {        // btn - starts a new round
                btn.onClick.RemoveAllListeners ( );
                btn.onClick.AddListener ( ( ) => {
                    float loadTime = .6f;
                    IState nextState = new LoadRoundState ( loadTime );
                    IStateTransition transition = new MenuExitTransition ( menuObject );
                    StateBeginExitEvent newRoundState = new StateBeginExitEvent ( nextState, transition );
                    RaiseUIEvent ( newRoundState );
                } );
            } else if ( btn.CompareTag ( TagsUI.settingsMenuBtn ) ) { // btn - toggle settings menu
                btn.onClick.RemoveAllListeners ( );
                btn.onClick.AddListener ( ( ) => {
                    Debug.Log ( "[MainMenu][OnToggleSettingsMenu] Settings menu not yet implemented ... " );
                } );
            }
        }
    }
}
