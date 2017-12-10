# Introduction to WPF data binding

[Wikipedia](http://en.wikipedia.org/wiki/Data_binding) describes the concept of data binding very well:

_Data binding is general technique that binds two data/information sources together and maintains synchronization of data._

+++

With WPF, Microsoft has put data binding in the front seat and once you start learning WPF, you will realize that it's an important aspect of pretty much everything you do. If you come from the world of WinForms, then the huge focus on data binding might scare you a bit, but once you get used to it, you will likely come to love it, as it makes a lot of things cleaner and easier to maintain.

+++

Data binding in WPF is the preferred way to bring data from your code to the UI layer. Sure, you can set properties on a control manually or you can populate a ListBox by adding items to it from a loop, but the cleanest and purest WPF way is to add a binding between the source and the destination UI element.

+++

## Agenda

In the next chapter, we'll look into a simple example where data binding is used and after that, we'll talk some more about all the possibilities. The concept of data binding is included pretty early in this tutorial, because it's such an integral part of using WPF, which you will see once you explore the rest of the chapters, where it's used almost all of the time.

+++

However, the more theoretical part of data binding might be too heavy if you just want to get started building a simple WPF application. In that case I suggest that you have a look at the "Hello, bound world!" article to get a glimpse of how data binding works, and then save the rest of the data binding articles for later, when you're ready to get some more theory.

---

# Hello, bound world!

Just like we started this tutorial with the classic "Hello, world!" example, we'll show you how easy it is to use data binding in WPF with a "Hello, bound world!" example. Let's jump straight into it and then I'll explain it afterwards:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.HelloBoundWorldSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="HelloBoundWorldSample" Height="110" Width="280">
    <StackPanel Margin="10">
                <TextBox Name="txtValue" />
                <WrapPanel Margin="0,10">
                        <TextBlock Text="Value: " FontWeight="Bold" />
                        <TextBlock Text="{Binding Path=Text, ElementName=txtValue}" />
                </WrapPanel>
        </StackPanel>
</Window>
```

+++

![A simple data binding between controls](http://www.wpf-tutorial.com/chapters/data-binding/images/hello_bound_world.png "A simple data binding between controls")

This simple example shows how we bind the value of the TextBlock to match the Text property of the TextBox. As you can see from the screenshot, the TextBlock is automatically updated when you enter text into the TextBox. In a non-bound world, this would require us to listen to an event on the TextBox and then update the TextBlock each time the text changes, but with data binding, this connection can be established just by using markup.

+++

## The syntax of a Binding

All the magic happens between the curly braces, which in XAML encapsulates a Markup Extension. For data binding, we use the Binding extension, which allows us to describe the binding relationship for the Text property. In its most simple form, a binding can look like this:

+++

{Binding}

This simply returns the current data context (more about that later). This can definitely be useful, but in the most common situations, you would want to bind a property to another property on the data context. A binding like that would look like this:

{Binding Path=NameOfProperty}

The Path notes the property that you want to bind to, however, since Path is the default property of a binding, you may leave it out if you want to, like this:

+++

{Binding NameOfProperty}

You will see many different examples, some of them where Path is explicitly defined and some where it's left out. In the end it's really up to you though.

A binding has many other properties though, one of them being the ElementName which we use in our example. This allows us to connect directly to another UI element as the source. Each property that we set in the binding is separated by a comma:

{Binding Path=Text, ElementName=txtValue}

+++

## Summary

This was just a glimpse of all the binding possibilities of WPF. In the next chapters, we'll discover more of them, to show you just how powerful data binding is.

---

# Using the DataContext

The DataContext property is the default source of your bindings, unless you specifically declare another source, like we did in the previous chapter with the ElementName property. It's defined on the FrameworkElement class, which most UI controls, including the WPF Window, inherits from. Simply put, it allows you to specify a basis for your bindings

+++

There's no default source for the DataContext property (it's simply null from the start), but since a DataContext is inherited down through the control hierarchy, you can set a DataContext for the Window itself and then use it throughout all of the child controls. Let's try illustrating that with a simple example:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.DataContextSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataContextSample" Height="130" Width="280">
        <StackPanel Margin="15">
                <WrapPanel>
                        <TextBlock Text="Window title:  " />
                        <TextBox Text="{Binding Title, UpdateSourceTrigger=PropertyChanged}" Width="150" />
                </WrapPanel>
                <WrapPanel Margin="0,10,0,0">
                        <TextBlock Text="Window dimensions: " />
                        <TextBox Text="{Binding Width}" Width="50" />
                        <TextBlock Text=" x " />
                        <TextBox Text="{Binding Height}" Width="50" />
                </WrapPanel>
        </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Windows;

namespace WpfTutorialSamples.DataBinding
{
        public partial class DataContextSample : Window
        {
                public DataContextSample()
                {
                        InitializeComponent();
                        this.DataContext = this;
                }
        }
}
```

+++

![Several data bindings using the DataContext](http://www.wpf-tutorial.com/chapters/data-binding/images/datacontext.png "Several data bindings using the DataContext")

The Code-behind for this example only adds one line of interesting code: After the standard InitalizeComponent() call, we assign the "this" reference to the DataContext, which basically just tells the Window that we want itself to be the data context.

+++

In the XAML, we use this fact to bind to several of the Window properties, including Title, Width and Height. Since the window has a DataContext, which is passed down to the child controls, we don't have to define a source on each of the bindings - we just use the values as if they were globally available.

+++

Try running the example and resize the window - you will see that the dimension changes are immediately reflected in the textboxes. You can also try writing a different title in the first textbox, but you might be surprised to see that this change is not reflected immediately. Instead, you have to move the focus to another control before the change is applied. Why? Well, that's the subject for the next chapter.

+++

## Summary

Using the DataContext property is like setting the basis of all bindings down through the hierarchy of controls. This saves you the hassle of manually defining a source for each binding, and once you really start using data bindings, you will definitely appreciate the time and typing saved.

+++

However, this doesn't mean that you have to use the same DataContext for all controls within a Window. Since each control has its own DataContext property, you can easily break the chain of inheritance and override the DataContext with a new value. This allows you to do stuff like having a global DataContext on the window and then a more local and specific DataContext on e.g. a panel holding a separate form or something along those lines.

---

# The UpdateSourceTrigger property

In the previous article we saw how changes in a TextBox was not immediately sent back to the source. Instead, the source was updated only after focus was lost on the TextBox. This behavior is controlled by a property on the binding called **UpdateSourceTrigger**.

+++

It defaults to the value "Default", which basically means that the source is updated based on the property that you bind to. As of writing, all properties except for the Text property, is updated as soon as the property changes (PropertyChanged), while the Text property is updated when focus on the destination element is lost (LostFocus).

+++

Default is, obviously, the default value of the UpdateSourceTrigger. The other options are **PropertyChanged**, **LostFocus** and **Explicit**. The first two has already been described, while the last one simply means that the update has to be pushed manually through to occur, using a call to UpdateSource on the Binding.

To see how all of these options work, I have updated the example from the previous chapter to show you all of them:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.DataBinding.DataContextSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataContextSample" Height="130" Width="310">
        <StackPanel Margin="15">
                <WrapPanel>
                        <TextBlock Text="Window title:  " />
                        <TextBox Name="txtWindowTitle" Text="{Binding Title, UpdateSourceTrigger=Explicit}" Width="150" />
                        <Button Name="btnUpdateSource" Click="btnUpdateSource_Click" Margin="5,0" Padding="5,0">*</Button>
                </WrapPanel>
                <WrapPanel Margin="0,10,0,0">
                        <TextBlock Text="Window dimensions: " />
                        <TextBox Text="{Binding Width, UpdateSourceTrigger=LostFocus}" Width="50" />
                        <TextBlock Text=" x " />
                        <TextBox Text="{Binding Height, UpdateSourceTrigger=PropertyChanged}" Width="50" />
                </WrapPanel>
        </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfTutorialSamples.DataBinding
{
        public partial class DataContextSample : Window
        {
                public DataContextSample()
                {
                        InitializeComponent();
                        this.DataContext = this;
                }

                private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
                {
                        BindingExpression binding = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
                        binding.UpdateSource();
                }
        }
}
```

+++

![Several data bindings, each using different UpdateSourceTrigger values](http://www.wpf-tutorial.com/chapters/data-binding/images/update_source_trigger.png "Several data bindings, each using different UpdateSourceTrigger values")

As you can see, each of the three textboxes now uses a different **UpdateSourceTrigger**.

+++

The first one is set to **Explicit**, which basically means that the source won't be updated unless you manually do it. For that reason, I have added a button next to the TextBox, which will update the source value on demand. In the Code-behind, you will find the Click handler, where we use a couple of lines of code to get the binding from the destination control and then call the UpdateSource() method on it.

The second TextBox uses the **LostFocus** value, which is actually the default for a Text binding. It means that the source value will be updated each time the destination control loses focus.

+++

The third and last TextBox uses the **PropertyChanged** value, which means that the source value will be updated each time the bound property changes, which it does in this case as soon as the text changes.

Try running the example on your own machine and see how the three textboxes act completely different: The first value doesn't update before you click the button, the second value isn't updated until you leave the TextBox, while the third value updates automatically on each keystroke, text change etc.

+++

## Summary

The UpdateSourceTrigger property of a binding controls how and when a changed value is sent back to the source. However, since WPF is pretty good at controlling this for you, the default value should suffice for most cases, where you will get the best mix of a constantly updated UI and good performance.

+++

For those situations where you need more control of the process, this property will definitely help though. Just make sure that you don't update the source value more often than you actually need to. If you want the full control, you can use the **Explicit** value and then do the updates manually, but this does take a bit of the fun out of working with data bindings.

---

# Responding to changes

So far in this tutorial, we have mostly created bindings between UI elements and existing classes, but in real life applications, you will obviously be binding to your own data objects. This is just as easy, but once you start doing it, you might discover something that disappoints you: Changes are not automatically reflected, like they were in previous examples.

+++

## Responding to data source changes

There are two different scenarios that you may or may not want to handle when dealing with data source changes: Changes to the list of items and changes in the bound properties in each of the data objects. How to handle them may vary, depending on what you're doing and what you're looking to accomplish, but WPF comes with two very easy solutions that you can use: The **ObservableCollection** and the **INotifyPropertyChanged** interface.

+++

**The following example will show you why we need these two things:**

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.ChangeNotificationSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ChangeNotificationSample" Height="150" Width="300">
        <DockPanel Margin="10">
                <StackPanel DockPanel.Dock="Right" Margin="10,0,0,0">
                        <Button Name="btnAddUser" Click="btnAddUser_Click">Add user</Button>
                        <Button Name="btnChangeUser" Click="btnChangeUser_Click" Margin="0,5">Change user</Button>
                        <Button Name="btnDeleteUser" Click="btnDeleteUser_Click">Delete user</Button>
                </StackPanel>
                <ListBox Name="lbUsers" DisplayMemberPath="Name"></ListBox>
        </DockPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.DataBinding
{
        public partial class ChangeNotificationSample : Window
        {
                private List<User> users = new List<User>();

                public ChangeNotificationSample()
                {
                        InitializeComponent();

                        users.Add(new User() { Name = "John Doe" });
                        users.Add(new User() { Name = "Jane Doe" });

                        lbUsers.ItemsSource = users;
                }

                private void btnAddUser_Click(object sender, RoutedEventArgs e)
                {
                        users.Add(new User() { Name = "New user" });
                }

                private void btnChangeUser_Click(object sender, RoutedEventArgs e)
                {
                        if(lbUsers.SelectedItem != null)
                                (lbUsers.SelectedItem as User).Name = "Random Name";
                }

                private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
                {
                        if(lbUsers.SelectedItem != null)
                                users.Remove(lbUsers.SelectedItem as User);
                }
        }

        public class User
        {
                public string Name { get; set; }
        }
}
```

+++

![Not receiving change notifications](http://www.wpf-tutorial.com/chapters/data-binding/images/change_notification1.png "Not receiving change notifications")

+++

Try running it for yourself and watch how even though you add something to the list or change the name of one of the users, **nothing in the UI is updated**. The example is pretty simple, with a User class that will keep the name of the user, a ListBox to show them in and some buttons to manipulate both the list and its contents. The ItemsSource of the list is assigned to a quick list of a couple of users that we create in the window constructor. The problem is that none of the buttons seems to work. Let's fix that, in two easy steps.

+++

## Reflecting changes in the list data source

The first step is to get the UI to respond to changes in the list source (ItemsSource), like when we add or delete a user. What we need is a list that notifies any destinations of changes to its content, and fortunately, WPF provides a type of list that will do just that. It's called ObservableCollection, and you use it much like a regular List<T>, with only a few differences.

+++

In the final example, which you will find below, we have simply replaced the List<User> with an ObservableCollection<User> - that's all it takes! This will make the Add and Delete button work, but it won't do anything for the "Change name" button, because the change will happen on the bound data object itself and not the source list - the second step will handle that scenario though.

+++

## Reflecting changes in the data objects

The second step is to let our custom User class implement the INotifyPropertyChanged interface. By doing that, our User objects are capable of alerting the UI layer of changes to its properties. This is a bit more cumbersome than just changing the list type, like we did above, but it's still one of the simplest way to accomplish these automatic updates.

+++

## The final and working example

With the two changes described above, we now have an example that WILL reflect changes in the data source. It looks like this:

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.ChangeNotificationSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ChangeNotificationSample" Height="135" Width="300">
        <DockPanel Margin="10">
                <StackPanel DockPanel.Dock="Right" Margin="10,0,0,0">
                        <Button Name="btnAddUser" Click="btnAddUser_Click">Add user</Button>
                        <Button Name="btnChangeUser" Click="btnChangeUser_Click" Margin="0,5">Change user</Button>
                        <Button Name="btnDeleteUser" Click="btnDeleteUser_Click">Delete user</Button>
                </StackPanel>
                <ListBox Name="lbUsers" DisplayMemberPath="Name"></ListBox>
        </DockPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfTutorialSamples.DataBinding
{
        public partial class ChangeNotificationSample : Window
        {
                private ObservableCollection<User> users = new ObservableCollection<User>();

                public ChangeNotificationSample()
                {
                        InitializeComponent();

                        users.Add(new User() { Name = "John Doe" });
                        users.Add(new User() { Name = "Jane Doe" });

                        lbUsers.ItemsSource = users;
                }

                private void btnAddUser_Click(object sender, RoutedEventArgs e)
                {
                        users.Add(new User() { Name = "New user" });
                }

                private void btnChangeUser_Click(object sender, RoutedEventArgs e)
                {
                        if(lbUsers.SelectedItem != null)
                                (lbUsers.SelectedItem as User).Name = "Random Name";
                }

                private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
                {
                        if(lbUsers.SelectedItem != null)
                                users.Remove(lbUsers.SelectedItem as User);
                }
        }

        public class User : INotifyPropertyChanged
        {
                private string name;
                public string Name {
                        get { return this.name; }
                        set
                        {
                                if(this.name != value)
                                {
                                        this.name = value;
                                        this.NotifyPropertyChanged("Name");
                                }
                        }
                }

                public event PropertyChangedEventHandler PropertyChanged;

                public void NotifyPropertyChanged(string propName)
                {
                        if(this.PropertyChanged != null)
                                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
        }
}
```

+++

![Receiving change notifications](http://www.wpf-tutorial.com/chapters/data-binding/images/change_notification2.png "Receiving change notifications")

+++

## Summary

As you can see, implementing INotifyPropertyChanged is pretty easy, but it does create a bit of extra code on your classes, and adds a bit of extra logic to your properties. This is the price you will have to pay if you want to bind to your own classes and have the changes reflected in the UI immediately. Obviously you only have to call NotifyPropertyChanged in the setter's of the properties that you bind to - the rest can remain the way they are.

+++

The ObservableCollection on the other hand is very easy to deal with - it simply requires you to use this specific list type in those situations where you want changes to the source list reflected in a binding destination.

---

# Value conversion with IValueConverter

So far we have used some simple data bindings, where the sending and receiving property was always compatible. However, you will soon run into situations where you want to use a bound value of one type and then present it slightly differently.

+++

## When to use a value converter

Value converters are very frequently used with data bindings. Here are some basic examples:

+++

*   You have a numeric value but you want to show zero values in one way and positive numbers in another way
*   You want to check a CheckBox based on a value, but the value is a string like "yes" or "no" instead of a Boolean value
*   You have a file size in bytes but you wish to show it as bytes, kilobytes, megabytes or gigabytes based on how big it is

+++

These are some of the simple cases, but there are many more. For instance, you may want to check a checkbox based on a Boolean value, but you want it reversed, so that the CheckBox is checked if the value is false and not checked if the value is true. You can even use a converter to generate an image for an ImageSource, based on the value, like a green sign for true or a red sign for false - the possibilities are pretty much endless!

+++

For cases like this, you can use a value converter. These small classes, which implement the IValueConverter interface, will act like middlemen and translate a value between the source and the destination. So, in any situation where you need to transform a value before it reaches its destination or back to its source again, you likely need a converter.

+++

## Implementing a simple value converter

As mentioned, a WPF value converter needs to implement the IValueConverter interface, or alternatively, the IMultiValueConverter interface (more about that one later). Both interfaces just requires you to implement two methods: Convert() and ConvertBack(). As the name implies, these methods will be used to convert the value to the destination format and then back again.

+++

Let's implement a simple converter which takes a string as input and then returns a Boolean value, as well as the other way around. If you're new to WPF, and you likely are since you're reading this tutorial, then you might not know all of the concepts used in the example, but don't worry, they will all be explained after the code listings:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.DataBinding.ConverterSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:local="clr-namespace:WpfTutorialSamples.DataBinding"
        Title="ConverterSample" Height="140" Width="250">
        <Window.Resources>
                <local:YesNoToBooleanConverter x:Key="YesNoToBooleanConverter" />
        </Window.Resources>
        <StackPanel Margin="10">
                <TextBox Name="txtValue" />
                <WrapPanel Margin="0,10">
                        <TextBlock Text="Current value is: " />
                        <TextBlock Text="{Binding ElementName=txtValue, Path=Text, Converter={StaticResource YesNoToBooleanConverter}}"></TextBlock>
                </WrapPanel>
                <CheckBox IsChecked="{Binding ElementName=txtValue, Path=Text, Converter={StaticResource YesNoToBooleanConverter}}" Content="Yes" />
        </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Windows.Data;

namespace WpfTutorialSamples.DataBinding
{
        public partial class ConverterSample : Window
        {
                public ConverterSample()
                {
                        InitializeComponent();
                }
        }

        public class YesNoToBooleanConverter : IValueConverter
        {
                public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                        switch(value.ToString().ToLower())
                        {
                                case "yes":
                                case "oui":
                                        return true;
                                case "no":
                                case "non":
                                        return false;
                        }
                        return false;
                }

                public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                        if(value is bool)
                        {
                                if((bool)value == true)
                                        return "yes";
                                else
                                        return "no";
                        }
                        return "no";
                }
        }
}
```

+++

![Using an IValueConverter, here with an empty value](http://www.wpf-tutorial.com/chapters/data-binding/images/converter_simple_no_value.png "Using an IValueConverter, here with an empty value")

+++
 
![Using an IValueConverter, here with a value that converts to false](http://www.wpf-tutorial.com/chapters/data-binding/images/converter_simple_false.png "Using an IValueConverter, here with a value that converts to false") 

+++

![Using an IValueConverter, here with a value that converts to true](http://www.wpf-tutorial.com/chapters/data-binding/images/converter_simple.png "Using an IValueConverter, here with a value that converts to true")

+++


## Code-behind

So, let's start from the back and then work our way through the example. We have implemented a converter in the Code-behind file called YesNoToBooleanConverter. As advertised, it just implements the two required methods, called Convert() and ConvertBack(). The Convert() methods assumes that it receives a string as the input (the _value_ parameter) and then converts it to a Boolean true or false value, with a fallback value of false. For fun, I added the possibility to do this conversion from French words as well.

+++

The ConvertBack() method obviously does the opposite: It assumes an input value with a Boolean type and then returns the English word "yes" or "no" in return, with a fallback value of "no".

You may wonder about the additional parameters that these two methods take, but they're not needed in this example. We'll use them in one of the next chapters, where they will be explained.

+++

## XAML

In the XAML part of the program, we start off by declaring an instance of our converter as a resource for the window. We then have a TextBox, a couple of TextBlocks and a CheckBox control and this is where the interesting things are happening: We bind the value of the TextBox to the TextBlock and the CheckBox control and using the Converter property and our own converter reference, we juggle the values back and forth between a string and a Boolean value, depending on what's needed.

+++

If you try to run this example, you will be able to change the value in two places: By writing "yes" in the TextBox (or any other value, if you want false) or by checking the CheckBox. No matter what you do, the change will be reflected in the other control as well as in the TextBlock.

+++

## Summary

This was an example of a simple value converter, made a bit longer than needed for illustrational purposes. In the next chapter we'll look into a more advanced example, but before you go out and write your own converter, you might want to check if WPF already includes one for the purpose. As of writing, there are more than 20 built-in converters that you may take advantage of, but you need to know their name. I found the [this list](http://stackoverflow.com/questions/505397/built-in-wpf-ivalueconverters) which might come in handy for you.

---

# The StringFormat property

As we saw in the previous chapters, the way to manipulate the output of a binding before is shown is typically through the use of a converter. The cool thing about the converters is that they allow you to convert any data type into a completely different data type.

+++

However, for more simple usage scenarios, where you just want to change the way a certain value is shown and not necessarily convert it into a different type, the **StringFormat** property might very well be enough.

+++

Using the StringFormat property of a binding, you lose some of the flexibility you get when using a converter, but in return, it's much simpler to use and doesn't involve the creation of a new class in a new file.

The StringFormat property does exactly what the name implies: It formats the output string, simply by calling the String.Format method. Sometimes an example says more than a thousand words, so before I hit that word count, let's jump straight into an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.StringFormatSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="StringFormatSample" Height="150" Width="250"
                Name="wnd">
        <StackPanel Margin="10">
                <TextBlock Text="{Binding ElementName=wnd, Path=ActualWidth, StringFormat=Window width: {0:#,#.0}}" />
                <TextBlock Text="{Binding ElementName=wnd, Path=ActualHeight, StringFormat=Window height: {0:C}}" />
                <TextBlock Text="{Binding Source={x:Static system:DateTime.Now}, StringFormat=Date: {0:dddd, MMMM dd}}" />
                <TextBlock Text="{Binding Source={x:Static system:DateTime.Now}, StringFormat=Time: {0:HH:mm}}" />
        </StackPanel>
</Window>
```

+++

![Several data bindings using the StringFormat property to control the output](http://www.wpf-tutorial.com/chapters/data-binding/images/stringformat.png "Several data bindings using the StringFormat property to control the output")

+++

The first couple of TextBlock's gets their value by binding to the parent Window and getting its width and height. Through the StringFormat property, the values are formatted. For the width, we specify a custom formatting string and for the height, we ask it to use the currency format, just for fun. The value is saved as a double type, so we can use all the same format specifiers as if we had called double.ToString(). You can find in this [list](http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx).

+++

Also notice how I can include custom text in the StringFormat - this allows you to pre/post-fix the bound value with text as you please. When referencing the actual value inside the format string, we surround it by a set of curly braces, which includes two values: A reference to the value we want to format (value number 0, which is the first possible value) and the format string, separated by a colon.

+++

For the last two values, we simply bind to the current date (DateTime.Now) and the output it first as a date, in a specific format, and then as the time (hours and minutes), again using our own, pre-defined format. You can read more about DateTime formatting [here](http://msdn.microsoft.com/en-us/library/az4se3k1.aspx).

+++

## Formatting without extra text

Please be aware that if you specify a format string that doesn't include any custom text, which all of the examples above does, then you need to add an extra set of curly braces, when defining it in XAML. The reason is that WPF may otherwise confuse the syntax with the one used for Markup Extensions. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.StringFormatSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="StringFormatSample" Height="150" Width="250"
                Name="wnd">
        <WrapPanel Margin="10">
                <TextBlock Text="Width: " />
                <TextBlock Text="{Binding ElementName=wnd, Path=ActualWidth, StringFormat={}{0:#,#.0}}" />
        </WrapPanel>
</Window>
```

+++

## Using a specific Culture

If you need to output a bound value in accordance with a specific culture, that's no problem. The Binding will use the language specified for the parent element, or you can specify it directly for the binding, using the ConverterCulture property. Here's an example:

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.StringFormatCultureSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="StringFormatCultureSample" Height="120" Width="300">
        <StackPanel Margin="10">
                <TextBlock Text="{Binding Source={x:Static system:DateTime.Now}, ConverterCulture='de-DE', StringFormat=German date: {0:D}}" />
                <TextBlock Text="{Binding Source={x:Static system:DateTime.Now}, ConverterCulture='en-US', StringFormat=American date: {0:D}}" />
                <TextBlock Text="{Binding Source={x:Static system:DateTime.Now}, ConverterCulture='ja-JP', StringFormat=Japanese date: {0:D}}" />
        </StackPanel>
</Window>
```

+++

![Several data bindings using the StringFormat property, with a specific ConverterCulture, to control the output](http://www.wpf-tutorial.com/chapters/data-binding/images/stringformat_culture.png "Several data bindings using the StringFormat property, with a specific ConverterCulture, to control the output")

It's pretty simple: By combining the StringFormat property, which uses the D specifier (Long date pattern) and the ConverterCulture property, we can output the bound values in accordance with a specific culture. Pretty nifty!

+++

# Debugging data bindings

Since data bindings are evaluated at runtime, and no exceptions are thrown when they fail, a bad binding can sometimes be very hard to track down. These problems can occur in several different situations, but a common issue is when you try to bind to a property that doesn't exist, either because you remembered its name wrong or because you simply misspelled it. 

+++

Here's an example:

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataBindingDebuggingSample" Height="100" Width="200">
    <Grid Margin="10" Name="pnlMain">
                <TextBlock Text="{Binding NonExistingProperty, ElementName=pnlMain}" />
        </Grid>
</Window>
```

+++

## The Output window

The first place you will want to look is the Visual Studio Output window. It should be at the bottom of your Visual Studio window, or you can activate it by using the [Ctrl+Alt+O] shortcut. There will be loads of output from the debugger, but somewhere you should find a line like this, when running the above example:

```
_System.Windows.Data Error: 40 : BindingExpression path error: 'NonExistingProperty' property not found on 'object' ''Grid' (Name='pnlMain')'. BindingExpression:Path=NonExistingProperty; DataItem='Grid' (Name='pnlMain'); target element is 'TextBlock' (Name=''); target property is 'Text' (type 'String')_
```

+++

This might seem a bit overwhelming, mainly because no linebreaks are used in this long message, but the important part is this:

_'NonExistingProperty' property not found on 'object' ''Grid' (Name='pnlMain')'._

It tells you that you have tried to use a property called "NonExistingProperty" on an object of the type Grid, with the name pnlMain. That's actually pretty concise and should help you correct the name of the property or bind to the real object, if that's the problem.

+++

## Adjusting the trace level

The above example was easy to fix, because it was clear to WPF what we were trying to do and why it didn't work. Consider this next example though:

```XML
Download sample
<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataBindingDebuggingSample" Height="100" Width="200">
    <Grid Margin="10">
                <TextBlock Text="{Binding Title}" />
        </Grid>
</Window>
```

+++

I'm trying to bind to the property "Title", but on which object? As stated in the article on data contexts, WPF will use the DataContext property on the TextBlock here, which may be inherited down the control hierarchy, but in this example, I forgot to assign a data context. This basically means that I'm trying to get a property on a NULL object. WPF will gather that this might be a perfectly valid binding, but that the object just hasn't been initialized yet, and therefore it won't complain about it. If you run this example and look in the **Output** window, you won't see any binding errors.

+++

However, for the cases where this is not the behavior that you're expecting, there is a way to force WPF into telling you about all the binding problems it runs into. It can be done by setting the TraceLevel on the PresentationTraceSources object, which can be found in the System.Diagnostics namespace:

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:diag="clr-namespace:System.Diagnostics;assembly=WindowsBase"
        Title="DataBindingDebuggingSample" Height="100" Width="200">
    <Grid Margin="10">
                <TextBlock Text="{Binding Title, diag:PresentationTraceSources.TraceLevel=High}" />
        </Grid>
</Window>
```

+++

Notice that I have added a reference to the System.Diagnostics namespace in the top, and then used the property on the binding. WPF will now give you loads of information about this specific binding in the **Output** window:

```XML
System.Windows.Data Warning: 55 : Created BindingExpression (hash=2902278) for Binding (hash=52760599)
System.Windows.Data Warning: 57 :   Path: 'Title'
System.Windows.Data Warning: 59 : BindingExpression (hash=2902278): Default mode resolved to OneWay
System.Windows.Data Warning: 60 : BindingExpression (hash=2902278): Default update trigger resolved to PropertyChanged
System.Windows.Data Warning: 61 : BindingExpression (hash=2902278): Attach to System.Windows.Controls.TextBlock.Text (hash=18876224)
System.Windows.Data Warning: 66 : BindingExpression (hash=2902278): Resolving source
System.Windows.Data Warning: 69 : BindingExpression (hash=2902278): Found data context element: TextBlock (hash=18876224) (OK)
System.Windows.Data Warning: 70 : BindingExpression (hash=2902278): DataContext is null
System.Windows.Data Warning: 64 : BindingExpression (hash=2902278): Resolve source deferred
System.Windows.Data Warning: 66 : BindingExpression (hash=2902278): Resolving source
System.Windows.Data Warning: 69 : BindingExpression (hash=2902278): Found data context element: TextBlock (hash=18876224) (OK)
System.Windows.Data Warning: 70 : BindingExpression (hash=2902278): DataContext is null
System.Windows.Data Warning: 66 : BindingExpression (hash=2902278): Resolving source
System.Windows.Data Warning: 69 : BindingExpression (hash=2902278): Found data context element: TextBlock (hash=18876224) (OK)
System.Windows.Data Warning: 70 : BindingExpression (hash=2902278): DataContext is null
System.Windows.Data Warning: 66 : BindingExpression (hash=2902278): Resolving source
System.Windows.Data Warning: 69 : BindingExpression (hash=2902278): Found data context element: TextBlock (hash=18876224) (OK)
System.Windows.Data Warning: 70 : BindingExpression (hash=2902278): DataContext is null
System.Windows.Data Warning: 66 : BindingExpression (hash=2902278): Resolving source  (last chance)
System.Windows.Data Warning: 69 : BindingExpression (hash=2902278): Found data context element: TextBlock (hash=18876224) (OK)
System.Windows.Data Warning: 77 : BindingExpression (hash=2902278): Activate with root item <null>
System.Windows.Data Warning: 105 : BindingExpression (hash=2902278):   Item at level 0 is null - no accessor
System.Windows.Data Warning: 79 : BindingExpression (hash=2902278): TransferValue - got raw value {DependencyProperty.UnsetValue}
System.Windows.Data Warning: 87 : BindingExpression (hash=2902278): TransferValue - using fallback/default value ''
System.Windows.Data Warning: 88 : BindingExpression (hash=2902278): TransferValue - using final value ''
```

+++

By reading through the list, you can actually see the entire process that WPF goes through to try to find a proper value for your TextBlock control. Several times you will see it being unable to find a proper DataContext, and in the end, it uses the default _{DependencyProperty.UnsetValue}_ which translates into an empty string.

+++

## Using the real debugger

The above trick can be great for diagnosing a bad binding, but for some cases, it's easier and more pleasant to work with the real debugger. Bindings doesn't natively support this, since they are being handled deep inside of WPF, but using a Converter, like shown in a previous article, you can actually jump into this process and step through it. You don't really need a Converter that does anything useful, you just need a way into the binding process, and a dummy converter will get you there:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:self="clr-namespace:WpfTutorialSamples.DataBinding"
        Title="DataBindingDebuggingSample" Name="wnd" Height="100" Width="200">
        <Window.Resources>
                <self:DebugDummyConverter x:Key="DebugDummyConverter" />
        </Window.Resources>
    <Grid Margin="10">
                <TextBlock Text="{Binding Title, ElementName=wnd, Converter={StaticResource DebugDummyConverter}}" />
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Windows.Data;
using System.Diagnostics;

namespace WpfTutorialSamples.DataBinding
{
        public partial class DataBindingDebuggingSample : Window
        {
                public DataBindingDebuggingSample()
                {
                        InitializeComponent();
                }
        }

        public class DebugDummyConverter : IValueConverter
        {
                public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                        Debugger.Break();
                        return value;
                }

                public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                        Debugger.Break();
                        return value;
                }
        }
}
```

+++

In the Code-behind file, we define a DebugDummyConverter. In the Convert() and ConvertBack() methods, we call Debugger.Break(), which has the same effect as setting a breakpoint in Visual Studio, and then return the value that was given to us untouched.

+++

In the markup, we add a reference to our converter in the window resources and then we use it in our binding. In a real world application, you should define the converter in a file of its own and then add the reference to it in App.xaml, so that you may use it all over the application without having to create a new reference to it in each window, but for this example, the above should do just fine.

+++

If you run the example, you will see that the debugger breaks as soon as WPF tries to fetch the value for the title of the window. You can now inspect the values given to the Convert() method, or even change them before proceeding, using the standard debugging capabilities of Visual Studio.

If the debugger never breaks, it means that the converter is not used. This usually indicates that you have an invalid binding expression, which can be diagnosed and fixed using the methods described in the start of this article. The dummy-converter trick is only for testing valid binding expressions.