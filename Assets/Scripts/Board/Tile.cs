﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour, ITile, IFadeableGameObject {
    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.white;

    public Vector2 TilePosition { get; private set; }
    public bool isAValidMove { get; private set; }

    public void InitState ( ) {
        TilePosition = new Vector2 ( transform.position.x , transform.position.y );
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        spriteRenderer.color = defaultColor;
        isAValidMove = true;
    }

    public void MarkTileAsSelected ( int playerByID ) {
        Color playerColor;
        if ( playerByID == 0 ) {
            playerColor = Color.red;
        } else {
            playerColor = Color.blue;
        }

        if ( isAValidMove ) {
            spriteRenderer.color = playerColor;
            isAValidMove = false;
        }
    }

    public void MakeActiveInScene() {
        gameObject.SetActive ( true );
    }

    // implements 'IFadeAbleGameObject' -- fades each child tild of boarObject
    public IEnumerable FadeIn ( float fadeMultiplier ) { yield return null; }
    public IEnumerable FadeOut( float fadeMultiplier ) { // a good speed is between .3f and 5.0f
        while ( spriteRenderer.color.a > 0 ) {
            Color spriteAlpha = spriteRenderer.color;
            print ( "ALPHA" + spriteAlpha.a );
            spriteAlpha.a -= Time.deltaTime * fadeMultiplier;
            spriteRenderer.color = new Color(spriteAlpha.r, spriteAlpha.g, spriteAlpha.b, spriteAlpha.a );
            yield return null;
        }
        Destroy ( gameObject );
    }
}
