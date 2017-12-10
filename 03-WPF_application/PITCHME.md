# A WPF Application - Introduction

In this tutorial, our primary focus will be on using WPF to create applications. As you may know, .NET can be executed on all platforms which have a .NET implementation, but the most common platform is still Microsoft Windows. When we talk about Windows applications in this tutorial, it really just means an application that runs on Windows (or another .NET compatible platform) and not in a browser or remotely over the Internet.

+++

A WPF application requires the .NET framework to run, just like any other .NET application type. Fortunately, Microsoft has been including the .NET framework on all versions of Windows since Vista, and they have been pushing out the framework on older versions through Windows Update. In other words, you can be pretty sure that most Windows users out there will be able to run your WPF application.

---

# The Window

When creating a WPF application, the first thing you will meet is the Window class. It serves as the root of a window and provides you with the standard border, title bar and maximize, minimize and close buttons. A WPF window is a combination of a XAML (.xaml) file, where the <Window> element is the root, and a CodeBehind (.cs) file. If you're using Visual Studio (Express) and you create a new WPF application, it will create a default window for you, which will look something like this:

+++

```XML
<Window x:Class="WpfApplication1.Window1"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="Window1" Height="300" Width="300">
    <Grid>

    </Grid>
</Window>
```

+++

The _x:class_ attribute tells the XAML file which class to use, in this case Window1, which Visual Studio has created for us as well. You will find it in the project tree in VS, as a child node of the XAML file. By default, it looks something like this:

+++

```C#
using System;
using System.Windows;
using System.Windows.Controls;
//…more using statements

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
    }
}
```

+++

As you can see, the Window1 class is definied as partial, because it's being combined with your XAML file in runtime to give you the full window. This is actually what the call to InitializeComponent() does, which is why it's required to get a full functioning window up and running.

+++

If we go back to the XAML file, you will notice a couple of other interesting attributes on the Window element, like Title, which defines the title of the window (shown in the title bar) as well as the starting width and height. There are also a couple of namespace definitions, which we will talk about in the XAML chapters.

+++

You will also notice that Visual Studio has created a Grid control for us inside the Window. The Grid is one of the WPF panels, and while it could be any panel or control, the Window can only have ONE child control, so a Panel, which in turn can contain multiple child controls, is usually a good choice. Later in this tutorial, we will have a much closer look into the different types of panels that you can use, as they are very important in WPF.

---

## Important Window properties

The WPF Window class has a bunch of interesting attributes that you may set to control the look and behavior of your application window. Here's a short list of the most interesting ones:

**Icon** - Allows you to define the icon of the window, which is usually shown in the upper right corner, right before the window title.

+++

**ResizeMode** - This controls whether and how the end-user can resize your window. The default is CanResize, which allows the user to resize the window like any other window, either by using the maximize/minimize buttons or by dragging one of the edges. CanMinimize will allow the user to minimize the window, but not to maximize it or drag it bigger or smaller. NoResize is the strictest one, where the maximize and minimize buttons are removed and the window can't be dragged bigger or smaller.

+++

**ShowInTaskbar** - The default is true, but if you set it to false, your window won't be represented in the Windows taskbar. Useful for non-primary windows or for applications that should minimize to the tray.

**SizeToContent** - Decide if the Window should resize itself to automatically fit its content. The default is Manual, which means that the window doesn't automatically resize. Other options are Width, Height and WidthAndHeight, and each of them will automatically adjust the window size horizontally, vertically or both.

+++

**Topmost** - The default is false, but if set to true, your Window will stay on top of other windows unless minimized. Only useful for special situations.

+++

**WindowStartupLocation** - Controls the initial position of your window. The default is Manual, which means that the window will be initially positioned according to the Top and Left properties of your window. Other options are CenterOwner, which will position the window in the center of it's owner window, and CenterScreen, which will position the window in the center of the screen.

+++

**WindowState** - Controls the initial window state. It can be either Normal, Maximized or Minimized. The default is Normal, which is what you should use unless you want your window to start either maximized or minimized.

---

# Working with App.xaml

App.xaml is the declarative starting point of your application. Visual Studio will automatically create it for you when you start a new WPF application, including a Code-behind file called App.xaml.cs. They work much like for a Window, where the two files are partial classes, working together to allow you to work in both markup (XAML) and Code-behind.

+++

App.xaml.cs extends the Application class, which is a central class in a WPF Windows application. .NET will go to this class for starting instructions and then start the desired Window or Page from there. This is also the place to subscribe to important application events, like application start, unhandled exceptions and so on. More about that later.

+++

One of the most commonly used features of the App.xaml file is to define global resources that may be used and accessed from all over an application, for instance global styles. This will be discussed in detail later on.

---

## App.xaml structure

When creating a new application, the automatically generated App.xaml will look something like this:

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>

    </Application.Resources>
</Application>
```

+++

The main thing to notice here is the StartupUri property. This is actually the part that instructs which Window or Page to start up when the application is launched. In this case, MainWindow.xaml will be started, but if you would like to use another window as the starting point, you can simply change this.

In some situations, you want more control over how and when the first window is displayed. In that case, you can remove the StartupUri property and value and then do it all from Code-Behind instead. This will be demonstrated below.

---

## App.xaml.cs structure

The matching App.xaml.cs will usually look like this for a new project:

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples
{
  public partial class App : Application
  {

  }
}
```

+++

You will see how this class extends the Application class, allowing us to do stuff on the application level. For instance, you can subscribe to the Startup event, where you can manually create your starting window.

Here's an example:

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                         Startup="Application_Startup">
    <Application.Resources></Application.Resources>
</Application>
```

+++

Notice how the StartupUri has been replaced with a subscription to the Startup event (subscribing to events through XAML is explained in another chapter). In Code-Behind, you can use the event like this:

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples
{
  public partial class App : Application
    {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      // Create the startup window
      MainWindow wnd = new MainWindow();
      // Do stuff here, e.g. to the window
      wnd.Title = "Something else";
      // Show the window
      wnd.Show();
    }
  }
}
```

The cool thing in this example, compared to just using the StartupUri property, is that we get to manipulate the startup window before showing it. In this, we change the title of it, which is not terribly useful, but you could also subscribe to events or perhaps show a splash screen. When you have all the control, there are many possibilities. We will look deeper into several of them in the next articles of this tutorial.

---

# Command-line parameters in WPF

Command-line parameters are a technique where you can pass a set of parameters to an application that you wish to start, to somehow influence it. The most common example is to make the application open with a specific file, e.g. in an editor. You can try this yourself with the built-in Notepad application of Windows, by running (select Run from the Start menu or press [WindowsKey-R]):

notepad.exe c:\Windows\win.ini

+++

This will open Notepad with the win.ini file opened (you may have to adjust the path to match your system). Notepad simply looks for one or several parameters and then uses them and your application can do the same!

Command-line parameters are passed to your WPF application through the Startup event, which we subscribed to in the App.xaml article. We will do the same in this example, and then use the value passed on to through the method arguments. First, the App.xaml file:

+++

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                         Startup="Application_Startup">
    <Application.Resources></Application.Resources>
</Application>
```

+++

All we do here is to subscribe to the **Startup** event, replacing the **StartupUri** property. The event is then implemented in App.xaml.cs:

+++

```XML
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples
{
  public partial class App : Application
  {
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      MainWindow wnd = new MainWindow();
      if(e.Args.Length == 1)
        MessageBox.Show("Now opening file: \n\n" + e.Args[0]);
      wnd.Show();
    }
  }
}
```

+++

The **StartupEventArgs** is what we use here. It's passed into the Application Startup event, with the name e. It has the property **Args**, which is an array of strings. Command-line parameters are separated by spaces, unless the space is inside a quoted string.

---

## Testing the command-line parameter

If you run the above example, nothing will happen, because no command-line parameters have been specified. Fortunately, Visual Studio makes it easy to test this in your application. From the **Project** menu select **"[Project name] properties"** and then go to the **Debug** tab, where you can define a command-line parameter. It should look something like this:

+++

![The command-line project settings](http://www.wpf-tutorial.com/chapters/wpf-application/images/commandline_parameters.png "The command-line project settings")

+++

Try running the application and you will see it respond to your parameter.

Of course, the message isn't terribly useful. Instead you might want to either pass it to the constructor of your main window or call a public open method on it, like this:

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples
{
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            // The OpenFile() method is just an example of what you could do with the
             // parameter. The method should be declared on your MainWindow class, where
             // you could use a range of methods to process the passed file path
             if(e.Args.Length == 1)
                wnd.OpenFile(e.Args[0]);
             wnd.Show();
        }
    }
}
```

---

## Command-line possibilities

In this example, we test if there is exactly one argument and if so, we use it as a filename. In a real world example, you might collect several arguments and even use them for options, e.g. toggling a certain feature on or off. You would do that by looping through the entire list of arguments passed while collecting the information you need to proceed, but that's beyond the scope of this article.

---

# Resources

WPF introduces a very handy concept: The ability to store data as a resource, either locally for a control, locally for the entire window or globally for the entire application. The data can be pretty much whatever you want, from actual information to a hierarchy of WPF controls. This allows you to place data in one place and then use it from or several other places, which is very useful.

+++

The concept is used a lot for styles and templates, which we'll discuss later on in this tutorial, but as it will be illustrated in this chapter, you can use it for many other things as well. Allow me to demonstrate it with a simple example:

+++

```XML
<Window x:Class="WpfTutorialSamples.WPF_Application.ResourceSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ResourceSample" Height="150" Width="350">
    <Window.Resources>
        <sys:String x:Key="strHelloWorld">Hello, world!</sys:String>
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBlock Text="{StaticResource strHelloWorld}" FontSize="56" />
        <TextBlock>Just another "<TextBlock Text="{StaticResource strHelloWorld}" />" example, but with resources!</TextBlock>
    </StackPanel>
</Window>
```

+++

![A simple resource sample](http://www.wpf-tutorial.com/chapters/wpf-application/images/hello_resource.png "A simple resource sample")

Resources are given a key, using the x:Key attribute, which allows you to reference it from other parts of the application by using this key, in combination with the StaticResource markup extension. In this example, I just store a simple string, which I then use from two different **TextBlock** controls.

---

## StaticResource vs. DynamicResource

In the examples so far, I have used the StaticResource markup extension to reference a resource. However, an alternative exists, in form of the DynamicResource.

The main difference is that a static resource is resolved only once, which is at the point where the XAML is loaded. If the resource is then changed later on, this change will not be reflected where you have used the StaticResource.

+++

A DynamicResource on the other hand, is resolved once it's actually needed, and then again if the resource changes. Think of it as binding to a static value vs. binding to a function that monitors this value and sends it to you each time it's changed - it's not exactly how it works, but it should give you a better idea of when to use what. Dynamic resources also allows you to use resources which are not even there during design time, e.g. if you add them from Code-behind during the startup of the application.

---

## More resource types

Sharing a simple string was easy, but you can do much more. In the next example, I'll also store a complete array of strings, along with a gradient brush to be used for the background. This should give you a pretty good idea of just how much you can do with resources:

+++

```XML
<Window x:Class="WpfTutorialSamples.WPF_Application.ExtendedResourceSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ExtendedResourceSample" Height="160" Width="300"
        Background="{DynamicResource WindowBackgroundBrush}">
    <Window.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>

        <x:Array x:Key="ComboBoxItems" Type="sys:String">
            <sys:String>Item #1</sys:String>
            <sys:String>Item #2</sys:String>
            <sys:String>Item #3</sys:String>
        </x:Array>

        <LinearGradientBrush x:Key="WindowBackgroundBrush">
            <GradientStop Offset="0" Color="Silver"/>
            <GradientStop Offset="1" Color="Gray"/>
        </LinearGradientBrush>
    </Window.Resources>
    <StackPanel Margin="10">
        <Label Content="{StaticResource ComboBoxTitle}" />
        <ComboBox ItemsSource="{StaticResource ComboBoxItems}" />
    </StackPanel>
</Window>
```

+++

![A more advanced example with several resource types](http://www.wpf-tutorial.com/chapters/wpf-application/images/resources_extended.png "A more advanced example with several resource types")

+++

This time, we've added a couple of extra resources, so that our Window now contains a simple string, an array of strings and a LinearGradientBrush. The string is used for the label, the array of strings is used as items for the ComboBox control and the gradient brush is used as background for the entire window. So, as you can see, pretty much anything can be stored as a resource.

---

## Local and application wide resources

For now, we have stored resources on a window-level, which means that you can access them from all over the window.

If you only need a given resource for a specific control, you can make it more local by adding it to this specific control, instead of the window. It works exactly the same way, the only difference being that you can now only access from inside the scope of the control where you put it:

+++

```XML
<StackPanel Margin="10">
    <StackPanel.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>
    </StackPanel.Resources>
    <Label Content="{StaticResource ComboBoxTitle}" />
</StackPanel>
```

+++

In this case, we add the resource to the StackPanel and then use it from its child control, the Label. Other controls inside of the StackPanel could have used it as well, just like children of these child controls would have been able to access it. Controls outside of this particular StackPanel wouldn't have access to it, though.

+++

If you need the ability to access the resource from several windows, this is possible as well. The **App.xaml** file can contain resources just like the window and any kind of WPF control, and when you store them in App.xaml, they are globally accessible in all of windows and user controls of the project. It works exactly the same way as when storing and using from a Window:

+++

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:sys="clr-namespace:System;assembly=mscorlib"
             StartupUri="WPF application/ExtendedResourceSample.xaml">
    <Application.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>
    </Application.Resources>
</Application>
```

+++

Using it is also the same - WPF will automatically go up the scope, from the local control to the window and then to App.xaml, to find a given resource:

```XML
<Label Content="{StaticResource ComboBoxTitle}" />
```

---

## Resources from Code-behind

So far, we've accessed all of our resources directly from XAML, using a markup extension. However, you can of course access your resources from Code-behind as well, which can be useful in several situations. In the previous example, we saw how we could store resources in several different places, so in this example, we'll be accessing three different resources from Code-behind, each stored in a different scope:

+++

**App.xaml:**

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:sys="clr-namespace:System;assembly=mscorlib"
             StartupUri="WPF application/ResourcesFromCodeBehindSample.xaml">
    <Application.Resources>
        <sys:String x:Key="strApp">Hello, Application world!</sys:String>
    </Application.Resources>
</Application>
```

+++

**Window:**

```XML
<Window x:Class="WpfTutorialSamples.WPF_Application.ResourcesFromCodeBehindSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ResourcesFromCodeBehindSample" Height="175" Width="250">
    <Window.Resources>
        <sys:String x:Key="strWindow">Hello, Window world!</sys:String>
    </Window.Resources>
    <DockPanel Margin="10" Name="pnlMain">
        <DockPanel.Resources>
            <sys:String x:Key="strPanel">Hello, Panel world!</sys:String>
        </DockPanel.Resources>

        <WrapPanel DockPanel.Dock="Top" HorizontalAlignment="Center" Margin="10">
            <Button Name="btnClickMe" Click="btnClickMe_Click">Click me!</Button>
        </WrapPanel>

        <ListBox Name="lbResult" />
    </DockPanel>
</Window>
```

+++

**Code-behind:**

```C#
using System;
using System.Windows;

namespace WpfTutorialSamples.WPF_Application
{
        public partial class ResourcesFromCodeBehindSample : Window
        {
                public ResourcesFromCodeBehindSample()
                {
                        InitializeComponent();
                }

                private void btnClickMe_Click(object sender, RoutedEventArgs e)
                {
                        lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
                        lbResult.Items.Add(this.FindResource("strWindow").ToString());
                        lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());
                }
        }
}
```

+++

![Resources grabbed from Code-behind](http://www.wpf-tutorial.com/chapters/wpf-application/images/codebehind_resources.png "Resources grabbed from Code-behind")

+++

So, as you can see, we store three different "Hello, world!" messages: One in App.xaml, one inside the window, and one locally for the main panel. The interface consists of a button and a ListBox.

In Code-behind, we handle the click event of the button, in which we add each of the text strings to the ListBox, as seen on the screenshot. We use the **FindResource()** method, which will return the resource as an object (if found), and then we turn it into the string that we know it is by using the ToString() method.

+++

Notice how we use the FindResource() method on different scopes - first on the panel, then on the window and then on the current **Application** object. It makes sense to look for the resource where we know it is, but as already mentioned, if a resource is not found, the search progresses up the hierarchy, so in principal, we could have used the FindResource() method on the panel in all three cases, since it would have continued up to the window and later on up to the application level, if not found.

+++

The same is not true the other way around - the search doesn't navigate down the tree, so you can't start looking for a resource on the application level, if it has been defined locally for the control or for the window.

---

# Handling exceptions in WPF  

If you're familiar with C# or any of the other .NET languages that you may use with WPF, then exception handling should not be new to you: Whenever you have a piece of code that are likely to throw an exception, then you should wrap it in a try-catch block to handle the exception gracefully. For instance, consider this example:

+++

```C#
private void Button_Click(object sender, RoutedEventArgs e)
{
        string s = null;
        s.Trim();
}
```

+++

Obviously it will go wrong, since I try to perform the Trim() method on a variable that's currently null. If you don't handle the exception, your application will crash and Windows will have to deal with the problem. As you can see, that isn't very user friendly:

+++

![An unhandled exception, left for Windows to deal with](http://www.wpf-tutorial.com/chapters/wpf-application/images/exception_unhandled.png "An unhandled exception, left for Windows to deal with")

+++

In this case, the user would be forced to close your application, due to such a simple and easily avoided error. So, if you know that things might go wrong, then you should use a try-catch block, like this:

+++

```C#
private void Button_Click(object sender, RoutedEventArgs e)
{
        string s = null;
        try
        {
                s.Trim();
        }
        catch(Exception ex)
        {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
}
```

+++

However, sometimes even the simplest code can throw an exception, and instead of wrapping every single line of code with a try- catch block, WPF lets you handle all unhandled exceptions globally. This is done through the **DispatcherUnhandledException** event on the Application class. If subscribed to, WPF will call the subscribing method once an exception is thrown which is not handled in your own code. Here's a complete example, based on the stuff we just went through:

+++

```XML
<Window x:Class="WpfTutorialSamples.WPF_Application.ExceptionHandlingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ExceptionHandlingSample" Height="200" Width="200">
    <Grid>
        <Button HorizontalAlignment="Center" VerticalAlignment="Center" Click="Button_Click">
            Do something bad!
        </Button>
    </Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;

namespace WpfTutorialSamples.WPF_Application
{
        public partial class ExceptionHandlingSample : Window
        {
                public ExceptionHandlingSample()
                {
                        InitializeComponent();
                }

                private void Button_Click(object sender, RoutedEventArgs e)
                {
                        string s = null;
                        try
                        {
                                s.Trim();
                        }
                        catch(Exception ex)
                        {
                                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        s.Trim();
                }
        }
}
```

+++

Notice that I call the Trim() method an extra time, outside of the try-catch block, so that the first call is handled, while the second is not. For the second one, we need the App.xaml magic:

+++

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             DispatcherUnhandledException="Application_DispatcherUnhandledException"
             StartupUri="WPF Application/ExceptionHandlingSample.xaml">
    <Application.Resources>
    </Application.Resources>
</Application>
```

+++

```C#
using System;
using System.Windows;

namespace WpfTutorialSamples
{
        public partial class App : Application
        {
                private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
                {
                        MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                        e.Handled = true;
                }
        }
}
```

+++

![A locally handled exception](http://www.wpf-tutorial.com/chapters/wpf-application/images/exception_handled_locally.png "A locally handled exception")

+++

![A globally handled exception](http://www.wpf-tutorial.com/chapters/wpf-application/images/exception_handled_globally.png "A globally handled exception")

+++

We handle the exception much like the local one, but with a slightly different text and image in the message box. Also, notice that I set the e.Handled property to true. This tells WPF that we're done dealing with this exception and nothing else should be done about it.

---

## Summary

Exception handling is a very important part of any application and fortunately, WPF and .NET makes it very easy to handle exceptions both locally and globally. You should handle exceptions locally when it makes sense and only use the global handling as a fallback mechanism, since local handling allows you to be more specific and deal with the problem in a more specialized way.
