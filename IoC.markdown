author: @ScottWeinstein
title: IoC crash course
footer: @ScottWeinstein
subfooter: Lab49
presdate: 4/19/2011

Agenda
===================================================================
* Why IoC
* Topics
    * Basics
    * Lifetime management
    * Other scenarios

Why IoC
=======================================================================
* IoC containers were create to solve problems with the classic application architectures
![Classic App Architecture](https://docs.google.com/drawings/pub?id=12ZD30D5Npiqc9koURuvPXix2LD2575RfMAO9uvRIRZA&w=960&h=720)


Problems with the Classic App Architecture
=======================================================================
* Sharing configuration info
![Classic App Architecture](https://docs.google.com/drawings/pub?id=1w6hCF8HcQ2huuuTPgA8KM0CvYCLvkETZoQ7pw4NM-qA&w=960&h=720)


Problems (2) with the Classic App Architecture
=======================================================================
* Sharing configuration info
![Classic App Architecture](https://docs.google.com/drawings/pub?id=14pmzJ9SekRBpA8zzhplA8Bqd0mme2p9KA0pUD9LIMxU&w=960&h=720)


Problems (3) with the Classic App Architecture
=======================================================================
* Static global state
    * The singleton anti-pattern
* Implicit shared static global state
    * The config
* Hard to manage late bindings
* Plethora of custom factory classes to address the above

Sample Classic Code
=======================================================================
* [Controller creates the model](https://github.com/ScottWeinstein/IoC-Demos/blob/master/NonContainerBased/Controllers/HomeController.cs)
* [Model creates the DB connection](https://github.com/ScottWeinstein/IoC-Demos/blob/master/NonContainerBased/Models/MyModel.cs)
* which reaches out and gets it's connection string
* And they all implement IDisposable


With Ioc
=======================================================================
1. A bit more setup code, but in return, much simpler implementations
2. Built-in flexibility to send in replacement implementations
3. No more shared global state

![Ioc Arch](https://docs.google.com/drawings/pub?id=13MbbO6mMIVl8ywfQFRxf7EhEujnxUIMA0rWobn7MtaQ&w=960&h=720)


With Ioc Code
=======================================================================


Advanced use cases
=======================================================================

1. Lifetime management
2. More than one
3. Anonymous factories


