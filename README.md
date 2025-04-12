# Abstract
The program was developed as part of Artificial Intelligence and Expert Systems course at Lodz University of Technology.\

The 15 puzzle is a sliding puzzle. It has 15 square tiles numbered 1 to 15 in a frame that is 4 tile positions high and 4 tile positions wide, with one unoccupied position. Tiles in the same row or column of the open position can be moved by sliding them horizontally or vertically, respectively. The goal of the puzzle is to place the tiles in numerical order (from left to right, top to bottom).

The n puzzle is a classical problem for modeling algorithms involving heuristics. Commonly used heuristics for this problem include counting the number of misplaced tiles and finding the sum of the taxicab distances between each block and its position in the goal configuration. Note that both are admissible. That is, they never overestimate the number of moves left, which ensures optimality for certain search algorithms such as A*.  
[Wikipedia](https://en.wikipedia.org/wiki/15_puzzle)
# Usage
## Console arguments
Program takes in given arguments:
* Search argument
  
|       Algorithm      | Console argument |
|:--------------------:|:----------------:|
| breadth-first search |        bfs       |
| depth-first search   |        dfs       |
| A-star               |       astr       |
* Search strategy
  
| Used with |      Parameter     |          Console argument          |
|:---------:|:------------------:|:----------------------------------:|
| BFS, DFS  | Search order       | Permutation of letters: L, R, U, D |
| A*        | Hamming Distance   |                hamm                |
| A*        | Manhattan Distance |                manh                |

* Input file with initial layout
* Output file for solution
* Output file for performance stats

## Input file format
The first line contains two positive integers separated by a space, representing the number of rows and columns, respectively.  
The following lines contain valid puzzle layout, defined by integers separated by spaces.  
The number 0 represents an empty field.

## Solution File
The first line contains a single integer containig number of steps in the solution.  
The second line contains a sequence of letters: L, R, U, and D, representing the steps of the solution.  
If no solution is found, the file contains a single line with the number -1.

## Performance stats file
This file contains following lines:  
1 (integer): the length of the found solution — identical to the value in the solution file (if no solution was found, this value is -1).  
2 (integer): the number of visited states;  
3 (integer): the number of processed states;  
4 (integer): the maximum depth of recursion reached;  
5 (reals number with 3 decimal places): the duration of the computation process in milliseconds.  

## Examples
``program.exe astr manh input.txt solution.txt stats.txt``  
``program.exe dfs LRUD input.txt solution.txt stats.txt``  
``program.exe bfs DLUR input.txt solution.txt stats.txt``
## Remarks
DFS uses a hardcoded maximum search depth of 20.
