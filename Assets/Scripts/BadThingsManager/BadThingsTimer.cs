using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadThingsTimer : MonoBehaviour
{
    public GameData data;
    public HudUpDate hud;
    public BadThingsEvents bad_events;

    //Those are in in-game years
    public float long_event_timer = 15f;
    public float short_event_timer = 5f;
    public float event_intervall = 7f;

    private float accumulator = 0f;
    private float start_year = float.MinValue;
    private float end_year = float.MaxValue;
    
    private bool event_in_progress = false;

    enum BadThings
    {
        birds_of_paradise,
        bats_out_of_hell,
        heaven_gets_the_blues,
        disco_inferno,
        paradise_dices,
        hell_freezes_over,
        heaven_nose,
        hell_handbasket
    }
    private BadThings current_event;

    private void FixedUpdate()
    {
        accumulator += Time.fixedDeltaTime;

        //Every 60 seconds tries 
        if (accumulator > event_intervall * 10f)
        {
            accumulator = 0;

            //Is an event happening?
            if (!event_in_progress)
            {
                event_in_progress = true; 
                start_year = hud.year;
                
                //Picks random event from the enum, ignoring last entry
                current_event = (BadThings)UnityEngine.Random.Range((int)BadThings.birds_of_paradise, 
                                                                    (int)BadThings.hell_handbasket);
                //Starts event
                StartStopRandomEvent(current_event, true);
            }
        }
        else if (hud.year > end_year)
        {
            //Reset for next event
            StartStopRandomEvent(current_event, false);
            event_in_progress = false;
            accumulator = 0;
            start_year = float.MinValue;
            end_year = float.MaxValue;
        }
    }

    private void StartStopRandomEvent(BadThings spawn_event, bool start)
    {
        //Spawns the even using the enum value
        switch (spawn_event)
        {
            case BadThings.birds_of_paradise:
                bad_events.BirdsOfParadise(start);
                SetStartAndEnd(true, start);
                break;
            
            case BadThings.bats_out_of_hell:
                bad_events.BatsOutOfHell(start);
                SetStartAndEnd(true, start); 
                break;
            
            case BadThings.heaven_gets_the_blues:
                bad_events.HeavenGetsTheBlues(start);
                SetStartAndEnd(true, start); 
                break;
            
            case BadThings.disco_inferno:
                bad_events.DiscoInferno(start);
                SetStartAndEnd(false, start); 
                break;
            
            case BadThings.paradise_dices:
                bad_events.ParadiseDices(start);
                SetStartAndEnd(false, start);
                break;
            
            case BadThings.hell_freezes_over:
                bad_events.HellFreezesOver(start);
                SetStartAndEnd(true, start); 
                break;
            
            case BadThings.heaven_nose:
                bad_events.HeavenNose(start);
                SetStartAndEnd(false, start);
                break;
            
            case BadThings.hell_handbasket:
                bad_events.HellHandbasket(start);
                SetStartAndEnd(false, start);
                break;
        }
    }

    //Sets the new duration of the event
    private void SetStartAndEnd(bool is_long, bool start)
    {
        //Doesn't reset timer if event is being closed
        if (!start) return;
        
        if (is_long)
        {
            end_year = start_year + long_event_timer;
        }
        else
        {
            end_year = start_year + short_event_timer; 
        }
    }
}
