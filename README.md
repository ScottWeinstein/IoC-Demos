author: @ScottWeinstein
title: IoC crash course
footer: @ScottWeinstein
subfooter: Lab49
presdate: 4/19/2011

Agenda
===================================================================
* Why IoC
* Before and After
* Lifetime management

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
* The containment model means that IDisposability _infects_ everthing

![Classic App Architecture](https://docs.google.com/drawings/pub?id=14pmzJ9SekRBpA8zzhplA8Bqd0mme2p9KA0pUD9LIMxU&w=960&h=720)


Problems (3) with the Classic App Architecture
=======================================================================
* Static global state
    * The singleton anti-pattern
* Implicit shared static global state
    * The config
* High coupling
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
Using Autofac...

* [Register the container](https://github.com/ScottWeinstein/IoC-Demos/blob/master/ContainerBased/Global.asax.cs)
* [Controllers just ask for what they need](https://github.com/ScottWeinstein/IoC-Demos/blob/master/ContainerBased/Controllers/HomeController.cs)
* [Insulated from changes](https://github.com/ScottWeinstein/IoC-Demos/blob/master/ContainerBased/Models/MyModel.cs) - look how the model is simpler, but has added more dependencies

Anonymous factories
=======================================================================
    builder.Register<DBEntities>(ctx => new DBEntities(ctx.Resolve<Config>().ConnectionString));
	builder.Register<HttpContextBase>(ctx => FakeHttpContext());

Lifetime management and Creating more than one
=======================================================================
When does my stuff get cleaned up?

With MVC apps, at the end of each request (by default)
Can override in many ways, `.SingleInstance()` and `Owned<T>` are two methods.

Let's look at [AdvancedModel](https://github.com/ScottWeinstein/IoC-Demos/blob/master/ContainerBased/Models/AdvancedModel.cs) and 
[LifetimeManagementModel](https://github.com/ScottWeinstein/IoC-Demos/blob/master/ContainerBased/Models/LifetimeManagementModel.cs)


Unit testing w/ Autofac
=======================================================================
[AutofacLifetimeFacts](https://github.com/ScottWeinstein/IoC-Demos/blob/master/UnitTestingWithIoC/AutofacLifetimeFacts.cs)

Integration with non Web apps
=======================================================================
* WCF - similar model to MVC
* WPF/Silverlight - fine w/ ViewModel first frameworks, such as Caliburn.* If you're coding View-first, integrating IoC can be hard

Pitfalls
=======================================================================
* Setups can get complex
* Resolution failure is at runtime, exceptions messages can be... obscure



Where's the code and stuff?
=======================================================================
[https://github.com/ScottWeinstein/IoC-Demos](https://github.com/ScottWeinstein/IoC-Demos)

How to build the presentation
=======================================================================
1. Install [pandoc](http://johnmacfarlane.net/pandoc/)
1. msbuild Demos.sln
1. pandoc -t slidy -s .\README.md -o pres.html
