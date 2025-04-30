# Position Punch
PositionPunch is a 3D boxing game where reading your opponent is the only way to win.

# How to play
You have four actions you can do as you exchange blows with your opponent:
**Jab,** **Feint,** **Guard,** and **Slip**.\
Exchanges are won by performing an action that results in a hit against your opponent, i.e. slipping when your opponent jabs. The full list of interactions can be found below. 


# Interaction Table
> [!NOTE]
> N = Nothing happens \
> Charge = Whoever has more charge wins.

| Action  | Jab | Feint  | Guard | Slip |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| **Jab**  | Charge  | Jab  | Guard Charge &uarr;  | Slip  |
| **Feint**  | Jab  | N  | Feint  | Feint  |
| **Guard**  | Guard Charge &uarr;  | Feint  | N  | N  |
| **Slip**  | Slip  | Feint  | N  | N  |


## Controls 
j - Jab\
k - Feint\
l - Guard\
; - Slip
> [!NOTE]
> Controls will be updated as two player functionality and controller support are implemented.



# Code Documentation
> [!NOTE]
> As a design and technical desicion both the AI opponent and player have the same moveset. so they both share the Fighter base class.

## Fighter Class
Both Opponent and player inherit from this class. It is in charge of the stats of fighters like health and charge as well as where the moves come from.

## Action Manager
The action manager is in charge of what happens when fighters do a move. in code, the visuals happen in the State Machine.

## State Machine
The State Machine is responsible for the transitions animations based off of outcomes from the Action Manager as well as handling damage application. Transitions happens based off of Success and Fail bools that are used by the action manager.

## UI Manager
This one is hopefully self explanatory: It is in charge of UI that changes based off of remaining health and charge. 


# Why Did I Make This?
Position Punch is a combination of my love of martial arts and programming. The goal of Position Punch is to make a foundation of a fighting game with code that is optimized code and most importantly: healthy. 

Animations used are [Basic Boxing/Fighting Animations](https://assetstore.unity.com/packages/3d/animations/basic-boxing-fighting-animations-251206#description).
