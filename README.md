# Logic Gate Simulator

## Purpose

Implement a subset of the logic components found in Logisim.

[Logisim Doc](http://www.cburch.com/logisim/docs/2.7/en/html/libs/index.html)

## Example

[Adder.cs](GateSim/Simulations/Adder.cs) shows an example simulation which hooks logic gates together.

## Definitions

Simulation : A [simulation](GateSim/Sim.cs) is a collection of devices and wires.

Device : A class that takes the values in it's input/s, does something to them, and writes values to it's output/s. For example, a Not gate is a device that takes the value in it's input array, inverts it, and writes it to it's output. There's an [IDevice interface](GateSim/IDevice.cs) that a class can implement to become a device. Devices can contain other devices.

Tick : A device has a tick function. This is where it's computation occurs. In a more general sense, when something ticks it updates it's outputs to new states dependant on the current state of it's inputs.

Wire: A [wire](GateSim/Wiring/Wire.cs) has a reference to a device's output array and one or more references to devices input arrays. When a wire ticks it copies the values in it's input to all it's outputs. However, it's tick function is called CopyInputToOutput(). Wire's should all be ticked at the same time in order for simulation runs to be deterministic. Therefore wire's aren't devices.