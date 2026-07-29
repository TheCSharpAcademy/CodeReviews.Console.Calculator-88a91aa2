# Console Calculator

## Basic Requirements
- [X] Create a Console Calculator by following the [guided project](https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console) from Microsoft.

## Challenges
- [X] Additional calculations:
  - [X] Square
  - [X] Square Root
  - [X] x to the power of y (i.e. x^y).
  - [X] 10x
  - [X] Trigonometry functions:
    - [X] Sine
    - [X] Cosine
    - [X] Tangent
- [X] Store a history of calculations that the user can view/delete.
- [X] Recall function - allow user to perform new operations by choosing operands from history.
- [X] Show number of times calculator was used (i.e. usage count).

## Lessons learnt based on feedback from previous project(s)
- [X] Avoid storing data in strings, use objects instead.
- [X] Compare enum directly instead of the `ToString()` equivalents.
- [X] Use Switch expressions.
- [X] Reduce/remove unnecessary nullable variables/properties.
- [X] Unit testing as a self-imposed challenge.
  - [X] Followed TDD as much as possible.

## Areas to improve
- Time management
- DRY:
  - The `CalculateFromRecalled` method is essentially a rehash of the `default` case from the `MainMenuChoices` loop.
