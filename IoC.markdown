author: @ScottWeinstein
title: IoC crash course
company: Lab49
email: sweinstein@lab49.com
presdate: 4/19/2011
footer-top-right:  IoC w/ Autofac
footer-bottom-left:  
footer-bottom-right:  
footer: @ScottWeinstein
{{ gradient blue white }}


Agenda
===================================================================
* Why IoC
* What's we'll cover
* Topics
    * Basics
    * Lifetime management
    * Other scenarios


Why IoC
=======================================================================
* IoC containers were create to solve specific problems

* Static global state
    * The singleton anti-pattern
* Implicit shared static global state
    * The config
* Hard to manage late binding of Interfaces
* Plethora of custom factory classes to address the above


What's we'll cover
=======================================================================

* Sample MVC app written w/o IoC
* Same app refactored to use IoC w/ Autofac


Benefits
=======================================================================

1. A bit more setup code, but in return, much simpler implementations
2. Built-in flexibility to send in replacement implementations
3. No more shared global state

Advanced use cases
=======================================================================

1. Lifetime management
2. More than one
3. Anonymous factories



