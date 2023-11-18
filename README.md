# 💶 Denomination Routine 

This projetc is part of [Ria Money Transfer](https://www.riamoneytransfer.com/en-us/) Software Engineer assignment.

## Problem

Write a program which will calculate for each payout the possible combinations which the ATM can pay out.
For example, for 100 EUR the available payout denominations would be:

- 10 x 10 EUR
- 1 x 50 EUR + 5 x 10 EUR
- 2 x 50 EUR
- 1 x 100 EUR

## Technologies used

- `C#` as programming language

## Solution

This is a coin change problem, that can be solved in various ways.

Several solutions use recursive functions, but they do not work well for getting all payout possibilities due to hight memory consumption.
This solution was created from scracth by [Arthur Soares](https://github.com/arthursoas), using an iterative approach.

### Steps

Calculate the first possibility using the highest denominations available, and then apply a limit usage for each denomination until only the lowest denomination is available.

The limit is applied by decrementing the lowest denomination used more than 0 times in the previous step. The lowest denomination of all denominations is never decremented to avoid unnecessary processing.
After applying the limit, all denominations with values below the changed one can be used infinite times.

If the payout value cannot be reached with the applied limits, the limits are decremented again.

The initial limit allows infinite use of any denomination.

Scenario:

- **Payout**: 100
- **Denominations**: 10, 50, 100

| Step 1 Limits | Step 1 Result  | Step 2 Limits | Step 2 Results | Step 3 Limits | Step 3 Results | Step 4 Limits | Step 4 Result  |
| ------        | -------------- | ------------- | -------------- | ------------- | -------------- | ------        | -------------- |
| `100: ∞`      | `100: 1`       | `100: 0`      | `100: 0`       | `100: 0`      | `100: 0`       | `100: 0`      | `100: 0`       |
| `50:  ∞`      | `50:  0`       | `50:  ∞`      | `50:  2`       | `50:  1`      | `50:  1`       | `50:  0`      | `50:  0`       |
| `10:  ∞`      | `10:  0`       | `10:  ∞`      | `10:  0`       | `10:  ∞`      | `10:  5`       | `10:  ∞`      | `10:  10`      |
