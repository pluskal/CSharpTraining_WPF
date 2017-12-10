# Introduction to WPF Commands

In a previous chapter of this tutorial, we talked about how to handle events, e.g. when the user clicks on a button or a menu item. In a modern user interface, it's typical for a function to be reachable from several places though, invoked by different user actions.

+++

For instance, if you have a typical interface with a main menu and a set of toolbars, an action like New or Open might be available in the menu, on the toolbar, in a context menu (e.g. when right clicking in the main application area) and from a keyboard shortcut like Ctrl+N and Ctrl+O.

+++

Each of these actions needs to perform what is typically the exact same piece of code, so in a WinForms application, you would have to define an event for each of them and then call a common function. With the above example, that would lead to at least three event handlers and some code to handle the keyboard shortcut. Not an ideal situation.

+++

## Commands

With WPF, Microsoft is trying to remedy that with a concept called commands. It allows you to define actions in one place and then refer to them from all your user interface controls like menu items, toolbar buttons and so on. WPF will also listen for keyboard shortcuts and pass them along to the proper command, if any, making it the ideal way to offer keyboard shortcuts in an application.

+++

Commands also solve another hassle when dealing with multiple entrances to the same function. In a WinForms application, you would be responsible for writing code that could disable user interface elements when the action was not available. For instance, if your application was able to use a clipboard command like Cut, but only when text was selected, you would have to manually enable and disable the main menu item, the toolbar button and the context menu item each time text selection changed.

+++

With WPF commands, this is centralized. With one method you decide whether or not a given command can be executed, and then WPF toggles all the subscribing interface elements on or off automatically. This makes it so much easier to create a responsive and dynamic application!

+++

## Command bindings

Commands don't actually do anything by them self. At the root, they consist of the ICommand interface, which only defines an event and two methods: Execute() and CanExecute(). The first one is for performing the actual action, while the second one is for determining whether the action is currently available. To perform the actual action of the command, you need a link between the command and your code and this is where the CommandBinding comes into play.

+++

A CommandBinding is usually defined on a Window or a UserControl, and holds a references to the Command that it handles, as well as the actual event handlers for dealing with the Execute() and CanExecute() events of the Command.

+++

## Pre-defined commands

You can of course implement your own commands, which we'll look into in one of the next chapters, but to make it easier for you, the WPF team has defined over 100 commonly used commands that you can use. They have been divided into 5 categories, called ApplicationCommands, NavigationCommands, MediaCommands, EditingCommands and ComponentCommands. Especially ApplicationCommands contains commands for a lot of very frequently used actions like New, Open, Save and Cut, Copy and Paste.

+++

## Summary

Commands help you to respond to a common action from several different sources, using a single event handler. It also makes it a lot easier to enable and disable user interface elements based on the current availability and state. This was all theory, but in the next chapters we'll discuss how commands are used and how you define your own custom commands.

---

# Using WPF commands

In the previous article, we discussed a lot of theory about what commands are and how they work. In this chapter, we'll look into how you actually use commands, by assigning them to user interface elements and creating command bindings that links it all together.

+++

We'll start off with a very simple example:

```XML
<Window x:Class="WpfTutorialSamples.Commands.UsingCommandsSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="UsingCommandsSample" Height="100" Width="200">
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.New" Executed="NewCommand_Executed" CanExecute="NewCommand_CanExecute" />
    </Window.CommandBindings>

    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <Button Command="ApplicationCommands.New">New</Button>
    </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WpfTutorialSamples.Commands
{
        public partial class UsingCommandsSample : Window
        {
                public UsingCommandsSample()
                {
                        InitializeComponent();
                }

                private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
                {
                        e.CanExecute = true;
                }

                private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
                {
                        MessageBox.Show("The New command was invoked");
                }
        }
}
```

+++

![An interface using a WPF command](http://www.wpf-tutorial.com/chapters/commands/images/simple_command.png "An interface using a WPF command")

+++

We define a command binding on the Window, by adding it to its CommandBindings collection. We specify that Command that we wish to use (the New command from the ApplicationCommands), as well as two event handlers. The visual interface consists of a single button, which we attach the command to using the **Command** property.

+++

In Code-behind, we handle the two events. The **CanExecute** handler, which WPF will call when the application is idle to see if the specific command is currently available, is very simple for this example, as we want this particular command to be available all the time. This is done by setting the **CanExecute** property of the event arguments to true.

+++

The **Executed** handler simply shows a message box when the command is invoked. If you run the sample and press the button, you will see this message. A thing to notice is that this command has a default keyboard shortcut defined, which you get as an added bonus. Instead of clicking the button, you can try to press Ctrl+N on your keyboard - the result is the same.

+++

## Using the CanExecute method

In the first example, we implemented a CanExecute event that simply returned true, so that the button would be available all the time. However, this is of course not true for all buttons - in many cases, you want the button to be enabled or disabled depending on some sort of state in your application.

+++

A very common example of this is the toggling of buttons for using the Windows Clipboard, where you want the Cut and Copy buttons to be enabled only when text is selected, and the Paste button to only be enabled when text is present in the clipboard. This is exactly what we'll accomplish in this example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Commands.CommandCanExecuteSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CommandCanExecuteSample" Height="200" Width="250">
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.Cut" CanExecute="CutCommand_CanExecute" Executed="CutCommand_Executed" />
        <CommandBinding Command="ApplicationCommands.Paste" CanExecute="PasteCommand_CanExecute" Executed="PasteCommand_Executed" />
    </Window.CommandBindings>
    <DockPanel>
        <WrapPanel DockPanel.Dock="Top" Margin="3">
            <Button Command="ApplicationCommands.Cut" Width="60">_Cut</Button>
            <Button Command="ApplicationCommands.Paste" Width="60" Margin="3,0">_Paste</Button>
        </WrapPanel>
        <TextBox AcceptsReturn="True" Name="txtEditor" />
    </DockPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WpfTutorialSamples.Commands
{
        public partial class CommandCanExecuteSample : Window
        {
                public CommandCanExecuteSample()
                {
                        InitializeComponent();
                }

                private void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
                {
                        e.CanExecute = (txtEditor != null) && (txtEditor.SelectionLength > 0);
                }

                private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
                {
                        txtEditor.Cut();
                }

                private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
                {
                        e.CanExecute = Clipboard.ContainsText();
                }

                private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
                {
                        txtEditor.Paste();
                }
        }
}
```

+++

![WPF command using the CanExecute method](http://www.wpf-tutorial.com/chapters/commands/images/commands_can_execute_simple.png "WPF command using the CanExecute method")

+++

So, we have this very simple interface with a couple of buttons and a TextBox control. The first button will cut to the clipboard and the second one will paste from it.

+++

In Code-behind, we have two events for each button: One that performs the actual action, which name ends with _Executed, and then the CanExecute events. In each of them, you will see that I apply some logic to decide whether or not the action can be executed and then assign it to the return value **CanExecute** on the EventArgs.

+++

The cool thing about this is that you don't have to call these methods to have your buttons updated - WPF does it automatically when the application has an idle moment, making sure that you interface remains updated all the time.

+++

## Default command behavior and CommandTarget

As we saw in the previous example, handling a set of commands can lead to quite a bit of code, with a lot of being method declarations and very standard logic. That's probably why the WPF team decided to handle some it for you. In fact, we could have avoided all of the Code-behind in the previous example, because a WPF TextBox can automatically handle common commands like Cut, Copy, Paste, Undo and Redo.

+++

WPF does this by handling the Executed and CanExecute events for you, when a text input control like the TextBox has focus. You are free to override these events, which is basically what we did in the previous example, but if you just want the basic behavior, you can let WPF connect the commands and the TextBox control and do the work for you. Just see how much simpler this example is:

+++

```XML
<Window x:Class="WpfTutorialSamples.Commands.CommandsWithCommandTargetSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CommandsWithCommandTargetSample" Height="200" Width="250">
    <DockPanel>
        <WrapPanel DockPanel.Dock="Top" Margin="3">
            <Button Command="ApplicationCommands.Cut" CommandTarget="{Binding ElementName=txtEditor}" Width="60">_Cut</Button>
            <Button Command="ApplicationCommands.Paste" CommandTarget="{Binding ElementName=txtEditor}" Width="60" Margin="3,0">_Paste</Button>
        </WrapPanel>
        <TextBox AcceptsReturn="True" Name="txtEditor" />
    </DockPanel>
</Window>
```

+++

![WPF command with command targets](http://www.wpf-tutorial.com/chapters/commands/images/commands_with_command_targets.png "WPF command with command targets")

+++

No Code-behind code needed for this example - WPF deals with all of it for us, but only because we want to use these specific commands for this specific control. The TextBox does the work for us.

Notice
+++
 how I use the **CommandTarget** properties on the buttons, to bind the commands to our TextBox control. This is required in this particular example, because the WrapPanel doesn't handle focus the same way e.g. a Toolbar or a Menu would, but it also makes pretty good sense to give the commands a target.

+++

## Summary

Dealing with commands is pretty straight forward, but does involve a bit extra markup and code. The reward is especially obvious when you need to invoke the same action from multiple places though, or when you use built-in commands that WPF can handle completely for you, as we saw in the last example.

---

# Implementing a custom WPF Command

In the previous chapter, we looked at various ways of using commands already defined in WPF, but of course you can implement your own commands as well. It's pretty simply, and once you've done it, you can use your own commands just like the ones defined in WPF.

+++

The easiest way to start implementing your own commands is to have a _static_ class that will contain them. Each command is then added to this class as static fields, allowing you to use them in your application. Since WPF, for some strange reason, doesn't implement an Exit/Quit command, I decided to implement one for our custom commands example. It looks like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.Commands.CustomCommandSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:self="clr-namespace:WpfTutorialSamples.Commands"
        Title="CustomCommandSample" Height="150" Width="200">
    <Window.CommandBindings>
        <CommandBinding Command="self:CustomCommands.Exit" CanExecute="ExitCommand_CanExecute" Executed="ExitCommand_Executed" />
    </Window.CommandBindings>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Menu>
            <MenuItem Header="File">
                <MenuItem Command="self:CustomCommands.Exit" />
            </MenuItem>
        </Menu>
        <StackPanel Grid.Row="1" HorizontalAlignment="Center" VerticalAlignment="Center">
            <Button Command="self:CustomCommands.Exit">Exit</Button>
        </StackPanel>
    </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WpfTutorialSamples.Commands
{
        public partial class CustomCommandSample : Window
        {
                public CustomCommandSample()
                {
                        InitializeComponent();
                }

                private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
                {
                        e.CanExecute = true;
                }

                private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
                {
                        Application.Current.Shutdown();
                }
        }

        public static class CustomCommands
        {
                public static readonly RoutedUICommand Exit = new RoutedUICommand
                        (
                                "Exit",
                                "Exit",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                                }
                        );

                //Define more commands here, just like the one above
        }
}
```

+++

![An interface using a custom WPF command](http://www.wpf-tutorial.com/chapters/commands/images/custom_command.png "An interface using a custom WPF command")

+++

In the markup, I've defined a very simple interface with a menu and a button, both of them using our new, custom Exit command. This command is defined in Code-behind, in our own **CustomCommands** class, and then referenced in the CommandBindings collection of the window, where we assign the events that it should use to execute/check if it's allowed to execute.

+++

All of this is just like the examples in the previous chapter, except for the fact that we're referencing the command from our own code (using the "self" namespace defined in the top) instead of a built-in command.

In Code-behind, we respond to the two events for our command: One event just allows the command to execute all the time, since that's usually true for an exit/quit command, and the other one calls the **Shutdown** method that will terminate our application. All very simple.

+++

As already explained, we implement our Exit command as a field on a static CustomCommands class. There are several ways of defining and assigning properties on the commands, but I've chosen the more compact approach (it would be even more compact if placed on the same line, but I've added line breaks here for readability) where I assign all of it through the constructor. The parameters are the text/label of the command, the name of the command, the owner type and then an InputGestureCollection, allowing me to define a default shortcut for the command (Alt+F4).

+++

## Summary

Implementing custom WPF commands is almost as easy as consuming the built-in commands, and it allows you to use commands for every purpose in your application. This makes it very easy to re-use actions in several places, as shown in the example of this chapter.
