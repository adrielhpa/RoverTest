# RoverTest

Our company has recently created a new department to build software/middleware for
remote devices in space. This rover must navigate around a plateau of the planet Mars.
Our headquarters on Earth will send a command to the rover in Mars indicating the
dimensions of the plateau and a movement sequence the rover must follow. If more than one
rover is deployed, then the command will contain more than one rover command.

Possible commands:

	M: move to next grid location

	L: turn left

	R: turn right

Example Command

	5 5

	1 2 N

	LML

The “5 5” part of the command indicates the size of the plateau. “1 2 N” indicates that the
rover is positioned on grid square 1,2 and is pointing north. So, if the rover moved, it would
move in the direction it was facing, in this case north. The last part of the command is the
movement sequence. “LML” means, ‘turn left, move, turn left’.
Additionally, the rover must not drive off the plateau. Should a human error occur, e.g. should
an operator try to drive the multi-million-dollar rover off the plateau, the rover should stop
and await rescue.
There can also be two or more rovers, in which case the instructions for all rovers will be
sent. Of course, the size of the exploration plateau will be the same. For example, the
command below will instruct two rovers on the 5 by 5 plateau:

	5 5

	1 2 N

	LML

	3 3 E

	MMR

Your task is to develop the software that will parse the command and move the rover(s).
