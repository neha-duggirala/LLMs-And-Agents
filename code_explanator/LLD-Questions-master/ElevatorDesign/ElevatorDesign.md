# Elevator System Design

## Requirements

1. The elevator system should consist of multiple elevators serving multiple floors.
2. Each elevator should have a capacity limit and should not exceed it.
3. Users should be able to request an elevator from any floor and select a destination floor.
4. The elevator system should efficiently handle user requests and optimize the movement of elevators to minimize waiting time.
5. The system should prioritize requests based on the direction of travel and the proximity of the elevators to the requested floor.
6. The elevators should be able to handle multiple requests concurrently and process them in an optimal order.
7. The system should ensure thread safety and prevent race conditions when multiple threads interact with the elevators.

# Solution

## Architecture & Design Patterns

1. **Singleton Pattern**  
   - **Class:** ElevatorSystem  
   - **Purpose:** Ensures a single, globally accessible instance of the elevator system, managing all elevators and floors.

2. **Strategy Pattern**  
   - **Classes:** IElevatorCarStrategy, AllFloorsElevatorStrategy, OddEvenElevatorCarStrategy  
   - **Purpose:** Allows dynamic assignment of different scheduling and movement algorithms to elevator cars. Each car can use a different strategy for handling requests, making the system flexible and extensible.

3. **Factory Pattern**  
   - **Class:** ButtonFactory (suggested for button creation)  
   - **Purpose:** Centralizes the creation of CarButton and FloorButton objects, decoupling instantiation logic from the rest of the system and supporting future extensibility.

4. **Command Pattern (Implicit)**  
   - **Classes:** CarButton, FloorButton  
   - **Purpose:** Encapsulates button press actions as objects, allowing requests to be parameterized and handled uniformly.

5. **Controller Pattern**  
   - **Class:** FloorController  
   - **Purpose:** Manages the assignment of elevator cars to floor requests, selecting the most appropriate car based on direction, current state, and proximity.

## Core Components

- **ElevatorCar:** Represents an individual elevator, maintains its current floor, direction, and handles button presses using its assigned strategy.  
- **Floor:** Represents a building floor, with up/down buttons and a reference to the floor controller.  
- **CarButton / FloorButton:** Represent buttons inside the elevator and on each floor, respectively. Each button triggers a request when pressed.  
- **FloorController:** Decides which elevator car should respond to a floor request, preferring cars already moving in the requested direction or idle cars.  
- **ElevatorSystem:** Singleton managing all cars and floors, providing initialization and access methods.

## Concurrency

- The system supports concurrent requests using threads. In the test simulation (Test1), two threads independently generate random button presses for odd and even elevator cars, as well as floor buttons, to simulate real-world usage.

## Logic Highlights

### Request Handling

- Floor button presses are routed to the FloorController, which selects the best elevator car based on direction and proximity.  
- Car button presses are handled by the car's strategy, which manages the queue of pending stops and direction changes.

### Strategy Flexibility

- The strategy pattern allows for easy swapping or upgrading of elevator scheduling algorithms without modifying the core elevator logic.

### Thread Safety

- Critical sections (e.g., car assignment in FloorController) are protected with locks to ensure safe concurrent access.

## Extensibility

- Add new strategies by implementing IElevatorCarStrategy and assigning them to elevator cars.  
- Extend button types using the factory pattern for new button behaviors.  
- Modify controller logic to experiment with different car assignment algorithms.
