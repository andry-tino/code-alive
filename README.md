# code-alive
Hackathon - Visualizing computer programs as living beings while they run

## Description
How do humans function? We are made of cells and these cells interact together. They split, they interact and cooperate with each other to do amazing things. If you took a sample of blood, a neural tissue, whatever biological sample from a human body and inspected it using a microscope, you could see all these cells in action.

How does a program function? We know that very well: we have code running. But not just code. We create components that interact together, just like cells. If we take our microscope to inspect a program, we would just see lines of code in a debugger, lines of logs telling us what's happening. That is not bad, but what if we wanted to visualize all the parts of a program as they interact together? What if we wanted a real (visual) microscope for programs? Then we would see cells and interactions between them. A new class instance would be visualized as a cell undergoing mitosis, data crossing components would look like signals across cells. We would gain a new way of seeing a program while it runs, we could inspect its interiors as if we were looking at a biological living being.

### Why
What is the purpose of this? I am not sure yet, but a new way of visualizing programs as they run can mean a different point of view, and I believe in the power of changing points of view. We could debug differently, we could enable virtual reality as we debug programs, we could find bugs in programs the same way biological scientists cure infections or cancer: by visualizing the entities and removing them. Think about all the possibilities that this new visualization paradigm would allow. We might even change the way we create programs in future, by designing the cells, the parts of the program as 3D objects.

### Final goal
My aim is to stop seeing programs as lines of text and change to a different perspective made of visuals.

## Implementation details
The idea is to have a 2-tier architecture in order to separate the two main layers the whole system is made of:

- **Code instrumentation** The code of the program we want to visualize must be changed in order to send events to the rendering engine about what just happened as the application runs. We need to parse the source code of the target program so that, as it runs, it sends events to the renderer which can align the visualization to reflect the state of the program.
- **Rendering** The engine responsible for listening to events sent by the program and create the graphic representation we want.

### Code instrumentation.
In order to make things simple we can initially target C# applications, very simple applications. By using Roslyn we can easily parse the source code and add the code necessary to send events to the renderer.

### Renderer
The renderer would be developed in Unity. Unity is a 3D engine which interacts well with .NET. It also will enable virtual reality through HoloLens in future.

### Communications (eventing)
We can use WCF (Windows Communication Foundation) to have the renderer spin-up a service endpoint listening to events. The target program's source can be parsed in order to add lines of code sending the events to the service endpoint exposed by the renderer.

We can use HTTP in order to enable a very versatile and decoupled architecture enabling us in future to have the two tiers in different locations and also in order to be able to parse different languages.
